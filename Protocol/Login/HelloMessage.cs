using System.Net.Sockets;
using System.Text;

namespace dkab.Protocol.Login
{
    class HelloMessage : OutPacket
    {
        public HelloMessage(NetworkStream s) : base(s)
        {
            data = Encoding.ASCII.GetBytes("hello 8.3\r\n");
        }
    }
}
