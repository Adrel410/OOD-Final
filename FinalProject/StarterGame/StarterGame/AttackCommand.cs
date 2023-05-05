using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    public class AttackCommand : Command
    {
        public AttackCommand() : base()
        {
            this.Name = "attack";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Attack(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nAttack what?");
            }
            return false;
        }
    }
}
