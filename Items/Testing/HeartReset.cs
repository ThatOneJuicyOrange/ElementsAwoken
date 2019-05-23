using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ElementsAwoken.Structures;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Testing
{
    public class HeartReset : ModItem
    {
        int mode = 0;

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart Reset");
            Tooltip.SetDefault("you cannot give up just yet");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                mode++;
                if (mode >= 5)
                {
                    mode = 0;
                }
                string text = "";
                switch (mode)
                {
                    case 0:
                        text = "Chaos heart remover";
                        break;
                    case 1:
                        text = "Void heart remover";
                        break;
                    case 2:
                        text = "Life fruit remover";
                        break;
                    case 3:
                        text = "Life crystal remover";
                        break;
                    default:
                        return base.CanUseItem(player);
                }
                Main.NewText(text, Color.White.R, Color.White.G, Color.White.B);
            }
            else
            {
                return true;
            }
            return base.CanUseItem(player);
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                switch (mode)
                {
                    case 0:
                        player.GetModPlayer<MyPlayer>().chaosHeartsUsed = 0;
                        break;
                    case 1:
                        player.GetModPlayer<MyPlayer>().voidHeartsUsed = 0;
                        player.GetModPlayer<MyPlayer>().chaosHeartsUsed = 0;
                        break;
                    case 2:
                        player.statLifeMax = 400;
                        player.GetModPlayer<MyPlayer>().voidHeartsUsed = 0;
                        player.GetModPlayer<MyPlayer>().chaosHeartsUsed = 0;
                        break;
                    case 3:
                        player.statLifeMax = 100;
                        player.GetModPlayer<MyPlayer>().voidHeartsUsed = 0;
                        player.GetModPlayer<MyPlayer>().chaosHeartsUsed = 0;
                        break;
                    default:
                        return base.CanUseItem(player);
                }
            }
            return true;
        }
    }
}
