using System.Net.Sockets;

namespace dkab.Helpers
{
    class NetworkStreamOperations
    {
        private NetworkStream stream;
        public NetworkStreamOperations(NetworkStream stream)
        {
            this.stream = stream;
        }

        public byte[] Read(int len)
        {
            byte[] packet = new byte[len];

            int totalRead = stream.Read(packet, 0, len);

            while(totalRead < len)
            {
                totalRead += stream.Read(packet, totalRead, len - totalRead);
            }

            return packet;
        }
    }
}
