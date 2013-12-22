using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteInterface
{
    public class EvernoteOAuth : IEvernote
    {
        //This one will use the OAuth stuff to authorize. Implement last.
        private static EvernoteOAuth evernoteOAuth;

        public static EvernoteOAuth Instance
        {
            get
            {
                if (evernoteOAuth == null)
                    evernoteOAuth = new EvernoteOAuth();

                return evernoteOAuth;
            }
        }

        private EvernoteOAuth() {}

        public EvernoteOAuth GetInstance() { return evernoteOAuth; }
    }
}
