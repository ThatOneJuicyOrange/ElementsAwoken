using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class FastTileUpdate : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;
        public override string Command
            => "fastTileUpdate";

        public override string Usage
            => "/fastTileUpdate duration";

        public override string Description
            => "Force mod tiles to random update every frame. May be buggy";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                MyWorld.fastRandUpdate = int.Parse(args[0]);
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}