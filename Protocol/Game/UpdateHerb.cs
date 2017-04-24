using dkab.World.Types;
using System.Globalization;
using System.Text;

namespace dkab.Protocol.Game
{
    class UpdateHerb : InPacket
    {
        public uint Id { get; private set; }
        public Flora Type { get; private set; }
        public int State { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }

        public override bool TryParse(byte[] packet)
        {
            string[] disPacket = Encoding.ASCII.GetString(packet).Split();

            if (!(disPacket.Length == 6 && disPacket[0].Equals("updherb")))
            {
                return false;
            }

            Id = uint.Parse(disPacket[1]);
            Type = TypeIdentifier.ResolveFlora(disPacket[2]);
            State = int.Parse(disPacket[3]);
            X = float.Parse(disPacket[4], CultureInfo.InvariantCulture);
            Y = float.Parse(disPacket[5], CultureInfo.InvariantCulture);

            return true;
        }

        public override string ToString()
        {
            return string.Format("updherb {0} {1} {2} {3} {4}", Id, (int)Type, State, X, Y);
        }
    }
}
