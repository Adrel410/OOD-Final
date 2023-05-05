using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld
{
    class ShopCommand : Command
    {
        public ShopCommand() : base()
        {
            this.Name = "shop";
        }

        override
        public bool Execute(Player player)
        {
            player.Shop();
            return false;
        }
    }
}
