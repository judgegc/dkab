using System.Net.Sockets;
using System.Text;

namespace dkab.Protocol.Game
{
    class Ping : OutGamePacket
    {
        public Ping(NetworkStream s) : base(s)
        {
            data = Encoding.ASCII.GetBytes("ping");
        }
    }
}
