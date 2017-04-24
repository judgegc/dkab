using System.Text;

namespace dkab.Protocol.Login
{
    class InvalidCredentials : InPacket
    {
        public override bool TryParse(byte[] packet)
        {
            return Encoding.ASCII.GetString(packet).Equals("ERR_LOGIN\n");
        }
    }
}
