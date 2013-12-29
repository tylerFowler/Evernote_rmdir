using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvernoteInterface;
using System.IO;
using FormSerialisation;
using Evernote.EDAM.Type;

namespace Evernote_rmdir
{
    public partial class Main : Form
    {
        //constants
        private const String STATUS_BAR_TEXT = "Number of completed Reminders to be deleted:";

        //primary interface into the Evernote service
        //use DevAuth for now, but later we'll use OAuth
        private EvernoteDevAuth evernote;

        //keeps track of values in the form
        private bool deleteAllReminders;
        private bool deleteRemindersBeforeCutoffDate;
        private int numCutoffDays;
        private bool excludeRemindersWithTag;
        private string tagToExclude;
        private List<Notebook> includedNotebooks;

        String settingsPath = Path.Combine(Environment.CurrentDirectory, @"\settings.xml");

        public Main()
        {
            InitializeComponent();

            try 
            { 
                evernote = EvernoteDevAuth.Instance; 
            }
            catch (Evernote.EDAM.Error.EDAMUserException) 
            { 
                //the Evernote Interface should give the user details on what happened
                Environment.Exit(0);
            }

            includedNotebooks = new List<Notebook>();

            if (chkbxDeleteAllReminders.Checked == true)
                ToggleDeleteAllReminders();

            RepopulateNotebooks();
        }

        /// <summary>
        /// Loads the settings file to restore the previous state (if there is one).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            //if there is a settings file from a previous session, use it
            //if (File.Exists(settingsPath))
                //FormSerialisor.Deserialise(this, settingsPath);
        }

        /// <summary>
        /// Gather all filtering information from the form and gather reminders to be deleted from Evernote.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckReminders_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPreviousUserCriteria();
                
                CollectAndValidateUserCriteria();

                lblStatusBar.Text = "Gathering information from your Evernote account...";

                //NOTE: I'm not sure that these values won't be null... could be an issue
                List<Reminder> reminders = evernote.GetCompletedRemindersMatchingCriteria(numCutoffDays, tagToExclude);

                //update the status bar
                lblStatusBar.Text = STATUS_BAR_TEXT + " " + reminders.Count;

                //enable to the run button
                btnRun.Enabled = true;
            }
            catch (ApplicationException)
            {
                return;
            }
        }

        private void ClearPreviousUserCriteria()
        {
            numCutoffDays = 0;
            tagToExclude = String.Empty;
            includedNotebooks.Clear();
        }

        /// <summary>
        /// Go through the form's controls, make sure everything is formatted correctly, and collect the criteria for the search.
        /// </summary>
        private void CollectAndValidateUserCriteria()
        {
            deleteAllReminders = chkbxDeleteAllReminders.Checked;
            //if delete all reminders is checked, the other two options must be set as false
            deleteRemindersBeforeCutoffDate = chkbxDeleteAllReminders.Checked ? false : chkbxDaysOffset.Checked;
            excludeRemindersWithTag = chkbxDeleteAllReminders.Checked ? false : chkbxExcludeTag.Checked;

            if (excludeRemindersWithTag) tagToExclude = txtbxExcludedTag.Text.Trim();


            //if the option to use a cutoff date is selected, make sure the value is a valid number
            if (deleteRemindersBeforeCutoffDate)
                if (!Int32.TryParse(txtbxNumDaysOffset.Text, out numCutoffDays))
                {
                    MessageBox.Show("You must enter a valid integer from 0 to 999 for the number of cutoff days.");
                    throw new ApplicationException();
                }


            //make sure that at least one notebook is selected to be included and add the checked notebooks to the list
            if (chkbxListNotebooks.CheckedItems.Count == 0)
            {
                MessageBox.Show("You must include at least one Notebook to pull reminders from.");
                throw new ApplicationException();
            }

            //don't want to add to any previous notebooks
            includedNotebooks.Clear();

            foreach (Object notebook in chkbxListNotebooks.CheckedItems)
                includedNotebooks.Add((Notebook)notebook);


            //if no options are selected, remind the user to check at least one option
            if (deleteAllReminders == false && deleteRemindersBeforeCutoffDate == false && excludeRemindersWithTag == false)
            {
                MessageBox.Show("You must select at least one option for filtering reminders.");
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Delete all queued up Reminders in the user's Evernote account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            //simply call the delete on the finalized list, maybe ask the user if he/she is sure?
            //might also have it to where it has a message saying it's done or something... 
            //make sure you catch everything Evernote might throw at you i.e. permissions!
        }

        private void Util_ShowFormValues()
        {
            MessageBox.Show("deleteAllReminders: " + deleteAllReminders + "\n"
                            + "deleteRemindersBeforeCutoffDate: " + deleteRemindersBeforeCutoffDate + "\n"
                            + "excludeRemindersWithTag: " + excludeRemindersWithTag + "\n"
                            + "tagToExclude: " + tagToExclude + "\n"
                            + "numCutoffDays: " + numCutoffDays
                            + "notebooks count: " + includedNotebooks.Count);
        }

        /// <summary>
        /// Find the state of the bottom two options (on or off) and flip them.
        /// </summary>
        private void ToggleDeleteAllReminders()
        {
            //whatever the state is, flip it
            bool toggle = !chkbxDaysOffset.Enabled;

            chkbxDaysOffset.Checked = false;
            chkbxDaysOffset.Enabled = toggle;
            txtbxNumDaysOffset.Enabled = toggle;
            lblDaysAgo.Enabled = toggle;

            chkbxExcludeTag.Checked = false;
            chkbxExcludeTag.Enabled = toggle;
            txtbxExcludedTag.Enabled = toggle;
        }

        /// <summary>
        /// Clears the checkbox list containing the user's notebooks and then repopulates the list.
        /// </summary>
        private void RepopulateNotebooks()
        {
            chkbxListNotebooks.Items.Clear();
            List<Notebook> notebooks = evernote.GetNotebooks();

            //add each notebook and make sure that it's state is initially set to be checked
            foreach (Notebook notebook in notebooks)
                chkbxListNotebooks.Items.Add(notebook, true);
        }

        /// <summary>
        /// When the 'Delete All Reminders' box is checked, toggle the values of the other options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkbxDeleteAllReminders_CheckedChanged(object sender, EventArgs e)
        {
            ToggleDeleteAllReminders();
        }

        private void txtbxNumDaysOffset_TextChanged(object sender, EventArgs e)
        {
            //let's not be illiterate
            if (txtbxNumDaysOffset.Text == "1") lblDaysAgo.Text = "day ago";
            else lblDaysAgo.Text = "days ago";
        }

        /// <summary>
        /// When the "Select All" checkbox is selected, check or uncheck all items in the list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool toggleState = chkbxSelectAll.Checked;

            for (int i = 0; i < chkbxListNotebooks.Items.Count; i++)
                chkbxListNotebooks.SetItemChecked(i, toggleState);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //save the state of the controls for later recall
            //FormSerialisor.Serialise(this, settingsPath);
        }
    }
}
