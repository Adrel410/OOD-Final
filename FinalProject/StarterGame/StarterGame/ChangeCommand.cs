using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    public class ChangeCommand : Command
    {
        public ChangeCommand() : base()
        {
            this.Name = "change";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasThirdWord())
            {
                player.Change(this.ThirdWord);
            }
            else
            {
                player.OutputMessage("\nChange to what?");
            }
            return false;
        }
    }
}
