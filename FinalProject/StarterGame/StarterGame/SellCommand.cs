using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class SellCommand : Command
    {
        public SellCommand() : base()
        {
            this.Name = "sell";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Sell(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nSell what?");
            }
            return false;
        }
    }
}
