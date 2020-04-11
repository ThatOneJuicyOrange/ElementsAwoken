using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Commands
{
    public class Wisp : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "wispForm";

        public override string Usage
            => "/wispForm";

        public override string Description
            => "Turn the player into a wisp";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (!modPlayer.forceWisp)
                {
                    modPlayer.forceWisp = true;

                }
                else
                {
                    player.QuickMount();
                    modPlayer.forceWisp = false;
                }
            }
            else
            {
                Main.NewText("You are not in debug Mode");
            }
        }
    }
}