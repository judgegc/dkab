using dkab.World.Types;
using System.Globalization;
using System.Text;

namespace dkab.Protocol.Game
{
    class UpdateMonster : InPacket
    {
        public uint     Id { get; private set; }
        public Fauna    Type { get; private set; }
        public int      State { get; private set; }
        public float    X { get; private set; }
        public float    Y { get; private set; }
        public int      EmoState { get; private set; }

        public override bool TryParse(byte[] packet)
        {
            string[] disPacket = Encoding.ASCII.GetString(packet).Split();

            if (!(disPacket.Length == 7 && disPacket[0].Equals("updmonstr")))
                return false;

            Id = uint.Parse(disPacket[1]);
            Type = TypeIdentifier.ResolveFauna(disPacket[2]);
            State = int.Parse(disPacket[3]);
            X = float.Parse(disPacket[4], CultureInfo.InvariantCulture);
            Y = float.Parse(disPacket[5], CultureInfo.InvariantCulture);
            EmoState = int.Parse(disPacket[6]);

            return true;
        }

        public override string ToString()
        {
            return string.Format("updmonstr {0} {1} {2} {3} {4} {5}", Id, (int)Type, State, X, Y, EmoState);
        }
    }
}
