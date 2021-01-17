using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class ResetAbilities : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "resetA";

        public override string Usage
            => "/resetA";

        public override string Description
            => "Set ability booleans to false";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                modPlayer.canSandstormA = false;
                modPlayer.canTimeA = false;
                modPlayer.canRainA = false;
                modPlayer.canWispA = false;
                Main.NewText("Abilities Removed");
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}