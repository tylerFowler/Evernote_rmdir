using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteInterface
{
    /// <summary>
    /// Error that occurs when the Evernote API being used is out of date.
    /// </summary>
    [Serializable()]
    public class EvernoteAPIVersionError : System.Exception
    {
        public EvernoteAPIVersionError() : base() { }
        public EvernoteAPIVersionError(string message) : base(message) { }
        public EvernoteAPIVersionError(string message, Exception inner) : base(message, inner) { }

    }

    /// <summary>
    /// Generic error to send to the main application signifying something has gone wrong on the Evernote side of things.
    /// Note: because this is used to send to the main application, it's important that any information that needs
    ///       to be sent to the user is sent in Evernote Interface.
    /// </summary>
    [Serializable()]
    public class EvernoteException : System.Exception
    {
        public EvernoteException() : base() { }
    }
}