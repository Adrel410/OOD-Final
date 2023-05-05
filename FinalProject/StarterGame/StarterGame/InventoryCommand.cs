using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    public class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
            this.Name = "inventory";
        }

        override
        public bool Execute(Player player)
        {
            player.Inventory();
            return false;
        }
    }
}
