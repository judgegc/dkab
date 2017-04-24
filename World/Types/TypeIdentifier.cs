using System.Collections.Generic;

namespace dkab.World.Types
{
    class TypeIdentifier
    {
        public static Fauna ResolveFauna(string type)
        {
            return new Dictionary<string, Fauna>()
            {
                { "monster", Fauna.MONSTER}
            }[type];
        }

        public static Flora ResolveFlora(string type)
        {
            return new Dictionary<string, Flora>()
            {
                { "palm", Flora.PALM},
                { "cactus", Flora.CACTUS }
            }[type];
        }

    }
}
