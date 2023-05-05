using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    public class PickUpCommand : Command
    {
        public PickUpCommand() : base()
        {
            this.Name = "pickup";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.PickUp(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nPickUp what?");
            }
            return false;
        }
    }
}
