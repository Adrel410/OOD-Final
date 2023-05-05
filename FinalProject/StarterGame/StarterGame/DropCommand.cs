using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class DropCommand : Command
    {
        public DropCommand() : base()
        {
            this.Name = "drop";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Drop(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nDrop what?");
            }
            return false;
        }
    }
}
