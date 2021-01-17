using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class SkipQuest : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "SkipQuest";

        public override string Usage
            => "/SkipQuest identifier";

        public override string Description
            => "Skips an active quest";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                string text = "Specified quest not found";
                foreach (Quest k in QuestWorld.quests)
                {
                    if (k.identifier == args[0])
                    {
                        text = args[0] + " completed";
                        k.completed = true;
                    }
                }
               Main.NewText(text);
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}