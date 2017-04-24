using System.Text;

namespace dkab.Protocol.Login
{
    class ReplyHelloMessage : InPacket
    {
        public override bool TryParse(byte[] packet)
        {
            return Encoding.ASCII.GetString(packet).Equals("hello\n");
        }
    }
}
