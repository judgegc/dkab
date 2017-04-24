using dkab.Protocol.Game;

namespace dkab.Client.Events
{
    class PacketConverter
    {
        public static string ReceiveDestination(ReceiveDestination packet, ReceiveDestinationForm to)
        {
            string[] args = packet.ToString().Split();
            switch(to)
            {
                case ReceiveDestinationForm.SPAWN:
                    args[0] = "spawn";
                    break;
                case ReceiveDestinationForm.MOVE:
                    args[0] = "move";
                    break;
            }
            return string.Join(" ", args);
        }

        public static string UpdateMonster(UpdateMonster packet, UpdateMonsterForm to)
        {
            string[] args = packet.ToString().Split();
            switch (to)
            {
                case UpdateMonsterForm.SPAWN:
                    args[0] = "fa_spawn";
                    break;
                case UpdateMonsterForm.MOVE:
                    args[0] = "fa_move";
                    break;
            }
            return string.Join(" ", args);
        }

        public static string UpdateHerb(UpdateHerb packet, UpdateHerbForm to)
        {
            string[] args = packet.ToString().Split();
            switch (to)
            {
                case UpdateHerbForm.SPAWN:
                    args[0] = "fl_spawn";
                    break;
                case UpdateHerbForm.GROWTH:
                    args[0] = "fl_growth";
                    break;
                case UpdateHerbForm.REMOVE:
                    args[0] = "fl_remove";
                    break;
            }
            return string.Join(" ", args);
        }
    }
}
