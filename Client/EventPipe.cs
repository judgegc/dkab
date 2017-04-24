using dkab.Client.Events;
using dkab.Protocol;
using dkab.Protocol.Game;
using System;
using System.Collections.Generic;

namespace dkab.Client
{
    class EventPipe
    {
        private HashSet<string> presentedUnits;
        private HashSet<uint> presentedFauna;
        private HashSet<uint> presentedFlora;

        private bool clearEvents = false;
        private List<string> events;

        public EventPipe()
        {
            presentedUnits = new HashSet<string>();
            presentedFauna = new HashSet<uint>();
            presentedFlora = new HashSet<uint>();
            events = new List<string>();
        }

        private void ClearEvents()
        {
            if (clearEvents)
            {
                events.Clear();
                clearEvents = false;
            }
        }
        public void Push(InPacket packet)
        {
            ClearEvents();

            if (packet is ReceiveDestination)
            {
                ReceiveDestination pos = packet as ReceiveDestination;

                if(presentedUnits.Contains(pos.Name))
                {
                    events.Add(PacketConverter.ReceiveDestination(pos, ReceiveDestinationForm.MOVE));
                }
                else
                {
                    presentedUnits.Add(pos.Name);
                    events.Add(PacketConverter.ReceiveDestination(pos, ReceiveDestinationForm.SPAWN));
                }
            }
            else if(packet is LeaveMessage)
            {
                LeaveMessage lev = packet as LeaveMessage;

                presentedUnits.Remove(lev.Name);
                events.Add(lev.ToString());
            }
            else if(packet is UpdateMonster)
            {
                UpdateMonster monster = packet as UpdateMonster;
                Console.WriteLine(monster.ToString());

                if(presentedFauna.Contains(monster.Id))
                {
                    events.Add(PacketConverter.UpdateMonster(monster, UpdateMonsterForm.MOVE));
                }
                else
                {
                    presentedFauna.Add(monster.Id);
                    events.Add(PacketConverter.UpdateMonster(monster, UpdateMonsterForm.SPAWN));
                }
            }
            else if(packet is UpdateHerb)
            {
                UpdateHerb herb = packet as UpdateHerb;

                if (presentedFauna.Contains(herb.Id))
                {
                    if(herb.State == 0)
                    {
                        events.Add(PacketConverter.UpdateHerb(herb, UpdateHerbForm.REMOVE));
                    }
                    else
                    {
                        events.Add(PacketConverter.UpdateHerb(herb, UpdateHerbForm.RISE));
                    }
                    
                }
                else
                {
                    presentedFlora.Add(herb.Id);
                    events.Add(PacketConverter.UpdateHerb(herb, UpdateHerbForm.SPAWN));
                }
            }
        }

        public List<string> PopAll()
        {
            ClearEvents();
            clearEvents = true;
            return events;
        }

        public void Reset()
        {
            presentedUnits.Clear();
            presentedFauna.Clear();
            presentedFlora.Clear();

            events.Clear();
        }
    }
}
