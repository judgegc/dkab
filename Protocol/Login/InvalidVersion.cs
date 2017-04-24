using System.Text;

namespace dkab.Protocol.Login
{
    public class InvalidVersion: InPacket
    {
        public override bool TryParse(byte[] packet)
        {
            return Encoding.ASCII.GetString(packet).Equals("old_bich\n");
        }
    }
}
