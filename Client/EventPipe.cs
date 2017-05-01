using dkab.Client.Events;
using dkab.Protocol;
using dkab.Protocol.Game;
using System;
using System.Collections.Generic;

namespace dkab.Client
{
    /// <summary>Converts game packet to events. Batching delivery</summary>
    class EventPipe
    {
        private HashSet<string> presentedUnits;
        private HashSet<uint> presentedFauna;
        private HashSet<uint> presentedFlora;

        private Queue<string> events;

        /// <summary>Affect on how many bytes PopAll method returns</summary>
        public uint MaxResponseSize { get; set; }

        /// <param name = "maxResponseSize" > Arma 3 limited size up to 10kB</param>
        public EventPipe(uint maxResponseSize = 4000)
        {
            presentedUnits = new HashSet<string>();
            presentedFauna = new HashSet<uint>();
            presentedFlora = new HashSet<uint>();
            events = new Queue<string>();

            MaxResponseSize = maxResponseSize;
        }

        public void Push(InPacket packet)
        {
            #region Playable unit events
            if (packet is ReceiveDestination)
            {
                ReceiveDestination pos = packet as ReceiveDestination;

                if (presentedUnits.Contains(pos.Name))
                {
                    events.Enqueue(PacketConverter.ReceiveDestination(pos, ReceiveDestinationForm.MOVE));
                }
                else
                {
                    presentedUnits.Add(pos.Name);
                    events.Enqueue(PacketConverter.ReceiveDestination(pos, ReceiveDestinationForm.SPAWN));
                }
            }
            else if (packet is LeaveMessage)
            {
                LeaveMessage lev = packet as LeaveMessage;

                presentedUnits.Remove(lev.Name);
                events.Enqueue(lev.ToString());
            }
            #endregion
            #region Fauna events
            else if (packet is UpdateMonster)
            {
                UpdateMonster monster = packet as UpdateMonster;

                if (presentedFauna.Contains(monster.Id))
                {
                    events.Enqueue(PacketConverter.UpdateMonster(monster, UpdateMonsterForm.MOVE));
                }
                else
                {
                    presentedFauna.Add(monster.Id);
                    events.Enqueue(PacketConverter.UpdateMonster(monster, UpdateMonsterForm.SPAWN));
                }
            }
            #endregion
            #region Flora events
            else if (packet is UpdateHerb)
            {
                UpdateHerb herb = packet as UpdateHerb;

                if (presentedFauna.Contains(herb.Id))
                {
                    if (herb.State == 0)
                    {
                        events.Enqueue(PacketConverter.UpdateHerb(herb, UpdateHerbForm.REMOVE));
                    }
                    else
                    {
                        events.Enqueue(PacketConverter.UpdateHerb(herb, UpdateHerbForm.GROWTH));
                    }

                }
                else
                {
                    presentedFlora.Add(herb.Id);
                    events.Enqueue(PacketConverter.UpdateHerb(herb, UpdateHerbForm.SPAWN));
                }
            }
            #endregion
        }

        public Queue<string> Pop()
        {
            int currentSize = 0;
            Queue<string> result = new Queue<string>();

            if (events.Count == 0)
                return result;

            do
            {
                string e = events.Dequeue();
                currentSize += e.Length;

                result.Enqueue(e);

            } while (events.Count > 0 && currentSize + events.Peek().Length < MaxResponseSize);

            return result;
        }
    }
}
