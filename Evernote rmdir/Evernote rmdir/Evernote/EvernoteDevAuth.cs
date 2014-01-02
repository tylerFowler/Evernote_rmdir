using Evernote.EDAM.Error;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.UserStore;
using System;
using System.Windows.Forms;
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
                throw new EvernoteException();
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
            try
            {
                Uri userStoreUrl = new Uri("https://" + evernoteHost + "/edam/user");
                TTransport userStoreTransport = new THttpClient(userStoreUrl);
                TProtocol userStoreProtocol = new TBinaryProtocol(userStoreTransport);
                userStore = new UserStore.Client(userStoreProtocol);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw new EvernoteException();
            }

            //make sure the API version we're using is ok
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
                throw new EvernoteException();
            }


            /* Try block for creating a new NoteStore Client
             * Possible outcomes with Evernote.EDAM.Error.EDAMUserException:
             *   AUTH_EXPIRED "authenticationToken" : token has expired
             *   BAD_DATA_FORMAT "authenticationToken" : token is malformed
             *   INVALID_AUTH "authenticationToken" : token signature is invalid
             */
            try
            {
                String noteStoreUrl = userStore.getNoteStoreUrl(authToken); //This isn't used with OAuth

                TTransport noteStoreTransport = new THttpClient(new Uri(noteStoreUrl));
                TProtocol noteStoreProtocol = new TBinaryProtocol(noteStoreTransport);
                noteStore = new NoteStore.Client(noteStoreProtocol);
            }
            catch (EDAMUserException e)
            {
                if (e.Parameter == "authenticationToken")
                {
                    String userErrorMessage = String.Empty;

                    switch (e.ErrorCode)
                    {
                        case EDAMErrorCode.AUTH_EXPIRED:
                            userErrorMessage = "Your developer token has expired";
                            break;
                        case EDAMErrorCode.BAD_DATA_FORMAT:
                            userErrorMessage = "Your developer token is incorrect or malformed";
                            break;
                        case EDAMErrorCode.INVALID_AUTH:
                            userErrorMessage = "Your developer token signature is invalid";
                            break;
                        default:
                            userErrorMessage = e.ToString();
                            break;
                    }

                    MessageBox.Show(userErrorMessage);
                }
                else
                    MessageBox.Show(e.ToString());

                throw new EvernoteException();
            }
            catch (EDAMSystemException e)
            {
                MessageBox.Show(e.ToString());
                throw new EvernoteException();
            }
        }
    }
}
