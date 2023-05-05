using System.Collections;
using System.Collections.Generic;

namespace HellWorld
{
    public class GoCommand : Command
    {
        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WaltTo(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nGo Where?");
            }
            return false;
        }
    }
}
