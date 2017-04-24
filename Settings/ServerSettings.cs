using System.Net;

namespace dkab.Settings
{
    class ServerSettings
    {
        public IPEndPoint GameServer { get; } = new IPEndPoint(IPAddress.Parse("194.87.237.144"), 6655);
        public IPEndPoint LoginServer { get; } = new IPEndPoint(IPAddress.Parse("194.87.237.144"), 6656);
        public IPEndPoint ChatServer { get; } = new IPEndPoint(IPAddress.Parse("194.87.237.144"), 6657);

        private static ServerSettings instance;

        public static ServerSettings Items
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerSettings();
                }
                return instance;
            }
        }
    }
}
