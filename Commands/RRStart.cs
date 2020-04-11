using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class RRStart : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "radrain";

        public override string Usage
            => "/radrain duration";

        public override string Description
            => "Toggles the Radiant Rain";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                if (MyWorld.radiantRain)
                {
                    MyWorld.radiantRain = false;
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f; Main.NewText("Radiant Rain disabled");
                }
                else
                {
                    MyWorld.radiantRain = true;
                    Main.rainTime = int.Parse(args[0]);
                    Main.raining = true;
                    Main.maxRaining = Main.rand.NextFloat(0.2f, 0.8f);
                    Main.NewText("Radiant Rain enabled for " + int.Parse(args[0]) / 60 + " seconds");
                }
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); 
        }
    }
}