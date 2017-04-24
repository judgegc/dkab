using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace dkab.Protocol.Game
{
    class SetDestination : OutGamePacket
    {
        public SetDestination(NetworkStream s, uint clientId, float x, float y) : base(s)
        {
            data = Encoding.ASCII.GetBytes(string.Format("pos  {0} {1} {2}", clientId, x.ToString("0.00", CultureInfo.InvariantCulture), y.ToString("0.00", CultureInfo.InvariantCulture)));
        }
    }
}
