using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using Evernote.EDAM.UserStore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvernoteInterface
{
    public abstract class IEvernote
    {
        protected UserStore.Client userStore;
        protected NoteStore.Client noteStore;

        protected String authToken;
        protected String evernoteHost;

        /// <summary>
        /// Gets all notebooks in a user's Evernote account.
        /// </summary>
        /// <returns>List of Notebook objects.</returns>
        public List<Notebook> GetNotebooks()
        {
            return noteStore.listNotebooks(authToken);
        }

        /// <summary>
        /// Gets all the completed reminders that match the criteria given as arguments (or all of them if no criteria is specified).
        /// </summary>
        /// <param name="numDays">The number of days to leave reminders before counting them in the search</param>
        /// <param name="tag">A tag used to exclude particular Reminders with said tag</param>
        /// <returns>A list of Reminder objects with the search results</returns>
        public List<Reminder> GetCompletedRemindersMatchingCriteria(int numDays = 0, String tag = null)
        {
            // Set up the Query to filter by the date reminders were completed and a certain tag if given one
            // and only get the attributes of the reminder we need (Guid, Notebook Guid, completion time)
            NoteFilter filter = new NoteFilter();

            if (numDays == 0) //matches all completed reminders
                filter.Words = "reminderDoneTime:*"; 
            else //matches all completed reminders marked as done before the number of days specified
                filter.Words = "reminderDoneTime:* -reminderDoneTime:day-" + numDays.ToString();

            if (tag != null && tag != String.Empty)
                filter.Words += " -tag:" + tag; 

            NotesMetadataResultSpec spec = new NotesMetadataResultSpec();
            spec.IncludeNotebookGuid = true;
            spec.IncludeAttributes = true;

            NotesMetadataList notes = noteStore.findNotesMetadata(authToken, filter, 0, Evernote.EDAM.Limits.Constants.EDAM_USER_NOTES_MAX, spec);

            // Now convert each note to a Reminder and add it to the list to be returned
            List<Reminder> result = new List<Reminder>();

            foreach (NoteMetadata note in notes.Notes)
            {
                Reminder convertedNote = ConvNoteToReminder(note);
                result.Add(convertedNote);
            }

            return result;
        }

        /// <summary>
        /// Converts a NoteMetadata representation of a reminder to our custom Reminder class.
        /// </summary>
        /// <param name="note">The note data to be converted</param>
        /// <returns>The converted note</returns>
        private Reminder ConvNoteToReminder(NoteMetadata note)
        {
            // Get the completed date and parse it as the colloquial DateTime object from the Unix Epoch date
            DateTime dateCompleted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateCompleted = dateCompleted.AddMilliseconds((Double)note.Attributes.ReminderDoneTime);

            return new Reminder(note.Guid, note.NotebookGuid, dateCompleted);
        }

        /// <summary>
        /// Goes through a List of Reminders and deletes each note the objects refer to.
        /// </summary>
        /// <param name="reminders">The list of Reminder objects who's note should be deleted</param>
        public void DeleteReminders(List<Reminder> reminders)
        {
            foreach (Reminder r in reminders)
                noteStore.deleteNote(authToken, r.GetReminderGuid());
        }
    }
}