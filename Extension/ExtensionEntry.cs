using dkab.Client;
using System.Threading;

namespace dkab.Extension
{
    class ExtensionEntry
    {
        private Thread thread;
        public GameClient GameClient { get; private set; }

        private static ExtensionEntry instance;
        public static ExtensionEntry Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExtensionEntry();
                }
                return instance;
            }
        }

        public ExtensionEntry()
        {
            
        }
        public void Connect(string username, string password)
        {
            GameClient = new GameClient();
            thread = new Thread(() =>
            {              
                bool suc = GameClient.Login(username, password);
            });
            thread.Start();
        }
    }
}
