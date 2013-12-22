using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Evernote.EDAM.Type;
using Evernote.EDAM.UserStore;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Error;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;


namespace EvernoteInterface
{
    public class EvernoteDevAuth : IEvernote
    {
        private static EvernoteDevAuth devAuth; //singleton design pattern, don't want to authorize twice!

        public static EvernoteDevAuth Instance
        {
            get
            {
                if (devAuth == null)
                    devAuth = new EvernoteDevAuth();

                return devAuth;
            }
        }

        private EvernoteDevAuth()
        {
            evernoteHost = "sandbox.evernote.com";
            authToken = "Enter your developer token here"; //note that you'll need to enter this yourself

            if (CheckIfValidAuthToken() == false)
            {
                MessageBox.Show("You forgot to enter your developer token!");
                throw new Evernote.EDAM.Error.EDAMUserException();
            }

            Authorize();
        }

        /// <summary>
        /// Checks to see if the developer token is still the default value.
        /// </summary>
        /// <returns>True if it's a valid token or false if not.</returns>
        private bool CheckIfValidAuthToken()
        {
            if (authToken != "Enter your developer token here" && authToken != String.Empty)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Performs the authorization to Evernote, initializes the User Store and Note Store.
        /// </summary>
        private void Authorize()
        {
            Uri userStoreUrl = new Uri("https://" + evernoteHost + "/edam/user");
            TTransport userStoreTransport = new THttpClient(userStoreUrl);
            TProtocol userStoreProtocol = new TBinaryProtocol(userStoreTransport);
            userStore = new UserStore.Client(userStoreProtocol);

            //here to a try block to see if the version is ok or not.
            try
            {
                bool versionOK = userStore.checkVersion("Evernote Evernote rmdir (C#)",
                                    Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MAJOR,
                                    Evernote.EDAM.UserStore.Constants.EDAM_VERSION_MINOR);
                if (!versionOK)
                    throw new EvernoteAPIVersionError("Your Evernote API version is too low.");
            }
            catch (EvernoteAPIVersionError e)
            {
                MessageBox.Show(e.ToString());
                return;
            }

            String noteStoreUrl = userStore.getNoteStoreUrl(authToken); //This isn't used with OAuth

            TTransport noteStoreTransport = new THttpClient(new Uri(noteStoreUrl));
            TProtocol noteStoreProtocol = new TBinaryProtocol(noteStoreTransport);
            noteStore = new NoteStore.Client(noteStoreProtocol);
        }
    }
}
