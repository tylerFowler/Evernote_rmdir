using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evernote.EDAM.Type;

namespace EvernoteInterface
{
    public class Reminder
    {
        private String guid;
        private String notebookGuid;
        private DateTime reminderCompleteDate; //corresponds to the reminderDoneTime attribute of a note

        public Reminder(String guid, String notebookGuid, DateTime timeCompleted)
        {
            this.guid = guid;
            this.notebookGuid = notebookGuid;
            this.reminderCompleteDate = timeCompleted;
        }

        public String GetNotebookGuid() { return notebookGuid; }

        public String GetReminderGuid() { return guid; }

        /// <summary>
        /// Checks to see if the given date is before the complete date of the Reminder object.
        /// </summary>
        /// <param name="date">The date to compare against</param>
        /// <returns>True if the given date is before the Reminder's complete date, false if not</returns>
        public bool WasCompletedBeforeDate(DateTime date)
        {
            return date < reminderCompleteDate;
        }
    }
}
