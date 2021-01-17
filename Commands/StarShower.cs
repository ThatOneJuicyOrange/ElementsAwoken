using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class StarShower : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;
        public override string Command
            => "starShower";

        public override string Usage
            => "/starShower duration";

        public override string Description
            => "Begin or end the star shower and turn it night time";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                if (MyWorld.starShowerTime > 0)
                {
                    MyWorld.starShowerTime = 2;
                }
                else
                {
                    MyWorld.starShowerTime = int.Parse(args[0]);
                    Main.dayTime = false;
                    Main.time = 0;
                }
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}