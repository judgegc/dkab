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
            thread = new Thread(() =>
            {
                GameClient = new GameClient();
                bool suc = GameClient.Login("judgeGC", "LoadLibrary");
            });
            thread.Start();
        }
    }
}
