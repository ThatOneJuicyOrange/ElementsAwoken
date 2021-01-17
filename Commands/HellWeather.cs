using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class HellWeather : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "hellWeather";

        public override string Usage
            => "/hellWeather num duration";

        public override string Description
            => "Change the current Volcanic Plateau weather";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                MyWorld.plateauWeatherTime = int.Parse(args[1]);
                MyWorld.plateauWeather = int.Parse(args[0]);
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}