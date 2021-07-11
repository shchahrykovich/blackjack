using System;
using Blackjack.Domain.Players.Strategies;

namespace Blackjack.Domain.Players
{
    public class ComputerPlayer: Player
    {
        public ComputerPlayer(): base(new StandOn17Strategy())
        {
            
        }

        public ComputerPlayer(BaseGameStrategy strategy)
            : base(strategy)
        {
        }      
    }
}
