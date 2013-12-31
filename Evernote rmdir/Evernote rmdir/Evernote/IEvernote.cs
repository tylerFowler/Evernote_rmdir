using Evernote.EDAM.Error;
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
            try
            {
                return noteStore.listNotebooks(authToken);
            }
            catch (EDAMUserException e)
            {
                MessageBox.Show(e.ToString());
                throw new EvernoteException();
            }
            catch (EDAMSystemException e)
            {
                if (e.ErrorCode == EDAMErrorCode.RATE_LIMIT_REACHED)
                    HandleRateLimitExceeded(e.RateLimitDuration);
                else
                    MessageBox.Show(e.ToString());
                    
                throw new EvernoteException();
            }
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

            if (String.IsNullOrEmpty(tag))
                filter.Words += " -tag:" + tag; 

            NotesMetadataResultSpec spec = new NotesMetadataResultSpec();
            spec.IncludeNotebookGuid = true;
            spec.IncludeAttributes = true;

            try
            {
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
            catch (EDAMUserException e)
            {
                if (e.ErrorCode == EDAMErrorCode.BAD_DATA_FORMAT)
                {
                    HandleBadQueryFormat(e);
                }
                else
                {
                    MessageBox.Show(e.ToString());
                }

                throw new EvernoteException();
            }
            catch (EDAMNotFoundException e)
            {
                MessageBox.Show(e.ToString());
                throw new EvernoteException();
            }
            catch (EDAMSystemException e)
            {
                if (e.ErrorCode == EDAMErrorCode.RATE_LIMIT_REACHED)
                    HandleRateLimitExceeded(e.RateLimitDuration);
                else
                    MessageBox.Show(e.ToString());
                    
                throw new EvernoteException();
            }
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
            //TODO: implement logging here, as this is where everything from the report will come from

            //NOTE: for this we don't necessarily want to throw an EvernoteException exception, these will actually be handled gracefully.
            //      For PERMISSION_DENIED: we just want to inform the user that they don't have permission, maybe prompt user to reenter credentials? 
            //                             Could be something we want to call in OAuth... which would be a problem for us.
            //      For DATA_CONFLICT: just skip this note, add it to the log if I go that route

            /* Try block for deleting the selected notes
             * Possible outcomes with Evernote.EDAM.Error.EDAMUserException:
             *   PERMISSION_DENIED "Note" : user doesn't have permission to update/delete notes
             *   DATA_CONFLICT "Note.guid" : the note has already been deleted
             * Possible outcomes with Evernote.EDAM.Error.EDAMNotFoundException:
             *   "Note.guid" : note not found by GUID
             */
            foreach (Reminder r in reminders)
            {
                try
                {
                    noteStore.deleteNote(authToken, r.GetReminderGuid());
                }
                catch (EDAMUserException e)
                {
                    if (e.ErrorCode == EDAMErrorCode.PERMISSION_DENIED && e.Parameter == "Note")
                    {
                        MessageBox.Show("You don't have permission to update or delete notes on this Evernote account");
                        throw new EvernoteException();
                    }
                    else if (e.ErrorCode == EDAMErrorCode.DATA_CONFLICT && e.Parameter == "Note.guid")
                    {
                        //the note's already been deleted so just skip this one, add it to the log if necessary
                        continue;
                    }
                    else
                        continue; //if there's some other problem just skip this note
                }
                catch (EDAMNotFoundException e)
                {
                    if (e.ToString().Contains("Note.guid"))
                    {
                        //if we're here, it means that the note wasn't found and we should skip it and log it in the report
                        continue;
                    }
                    else
                        continue; //if there's some other problem just skip this note
                }
                catch (EDAMSystemException e)
                {
                    if (e.ErrorCode == EDAMErrorCode.RATE_LIMIT_REACHED)
                        HandleRateLimitExceeded(e.RateLimitDuration);
                    else
                        MessageBox.Show(e.ToString());

                    //we actually want to stop here because we don't want to continue making more calls
                    throw new EvernoteException();
                }
            }

            //here is where we're gonna wanna return information for the report
        }

        private static void HandleBadQueryFormat(EDAMUserException e)
        {
            String userErrorMessage = String.Empty;

            switch (e.Parameter)
            {
                case "offset":
                    userErrorMessage = "Offset not between 0 and EDAM_USER_NOTES_MAX";
                    break;
                case "maxNotes":
                    userErrorMessage = "Max number of notes to return isn't between 0 and EDAM_USER_NOTES_MAX";
                    break;
                case "NoteFilter.notebookGuid":
                    userErrorMessage = "Notebook GUID is malformed";
                    break;
                case "NoteFilter.tagGuids":
                    userErrorMessage = "Tags aren't formatted correctly, only select one tag name to be used";
                    break;
                case "NoteFilter.words":
                    userErrorMessage = "Query string is too long, try using a shorter tag?";
                    break;
                default:
                    userErrorMessage = e.ToString();
                    break;
            }

            MessageBox.Show(userErrorMessage);
        }

        protected void HandleRateLimitExceeded(int rateLimitDuration)
        {
            //convert seconds to minutes
            double minutes = rateLimitDuration / 60.0;
            MessageBox.Show("Rate limit reached. Try again in " + minutes.ToString("F") + " minute(s).");
        }
    }
}