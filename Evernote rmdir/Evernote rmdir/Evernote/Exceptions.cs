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
}