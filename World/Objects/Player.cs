using dkab.Extension;
using dkab.Protocol.Game;

namespace dkab.World.Objects
{
    class Player: Unit
    {
        public uint Id { get; private set; }
        public string Name { get; private set; }

        public Player(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
