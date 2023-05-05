using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class BuyCommand : Command
    {
        public BuyCommand() : base()
        {
            this.Name = "buy";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Buy(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nBuy what?");
            }
            return false;
        }
    }
}
