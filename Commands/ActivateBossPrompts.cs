using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class ActivateBossPrompts : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;
        public override string Command
            => "activateBossPrompts";

        public override string Usage
            => "/activateBossPrompts";

        public override string Description
            => "Sets all boss prompt counters to active. Wont turn it on if the boss prompt isnt available";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                MyWorld.desertPrompt = ElementsAwoken.bossPromptDelay;
                MyWorld.firePrompt = ElementsAwoken.bossPromptDelay;
                MyWorld.frostPrompt = ElementsAwoken.bossPromptDelay;
                MyWorld.skyPrompt = ElementsAwoken.bossPromptDelay;
                MyWorld.voidPrompt = ElementsAwoken.bossPromptDelay;
                MyWorld.waterPrompt = ElementsAwoken.bossPromptDelay;
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}