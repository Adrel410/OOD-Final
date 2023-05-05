using System.Collections;
using System.Collections.Generic;

namespace HellWorld
{
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
