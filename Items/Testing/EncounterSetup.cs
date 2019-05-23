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
    public class EncounterSetup : ModItem
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
            DisplayName.SetDefault("Encounter Setup");
            Tooltip.SetDefault("he comes");
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
                if (mode > 2)
                {
                    mode = 0;
                }
                string text = "";
                switch (mode)
                {
                    case 0:
                        text = "1";
                        break;
                    case 1:
                        text = "2";
                        break;
                    case 2:
                        text = "3";
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
                if (mode == 0)
                {
                    MyWorld.encounter1 = true;
                    MyWorld.encounter2 = false;
                    MyWorld.encounter3 = false;
                }
                else if (mode == 1)
                {
                    MyWorld.encounter1 = false;
                    MyWorld.encounter2 = true;
                    MyWorld.encounter3 = false;
                }
                else
                {
                    MyWorld.encounter1 = false;
                    MyWorld.encounter2 = false;
                    MyWorld.encounter3 = true;
                }
                MyWorld.encounterTimer = 3600;
                MyWorld.encounterSetup = false;
            }
            return true;
        }
    }
}
