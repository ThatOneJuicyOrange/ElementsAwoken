using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class SetEncounter : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "setEncounter";

        public override string Usage
            => "/setEncounter num";

        public override string Description
            => "Change the current encounter";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                ElementsAwoken.encounter = int.Parse(args[0]);
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.encounterSetup = false;
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}