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
    public class DownedReset : ModItem
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
            item.GetGlobalItem<EATooltip>().testing = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Downed Reset");
            Tooltip.SetDefault("oof");
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
                if (mode >= 4)
                {
                    mode = 0;
                }
                string text = "";
                switch (mode)
                {
                    case 0:
                        text = "Regular Off";
                        break;
                    case 1:
                        text = "Elemental Off";
                        break;
                    case 2:
                        text = "Regular On";
                        break;
                    case 3:
                        text = "Elemental On";
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
                        MyWorld.downedToySlime = false;
                        MyWorld.downedScourgeFighter = false;
                        MyWorld.downedObsidious = false;
                        MyWorld.downedEye = false;
                        MyWorld.downedAncientWyrm = false;
                        MyWorld.downedGuardian = false;
                        MyWorld.downedVoidEvent = false;
                        MyWorld.downedShadeWyrm = false;
                        MyWorld.downedVolcanox = false;
                        MyWorld.downedAzana = false;
                        MyWorld.sparedAzana = false;
                        MyWorld.downedAncients = false;
                        MyWorld.downedCosmicObserver = false;
                        break;
                    case 1:
                        MyWorld.downedWasteland = false;
                        MyWorld.downedInfernace = false;
                        MyWorld.downedRegaroth = false;
                        MyWorld.downedPermafrost = false;
                        MyWorld.downedAqueous = false;
                        MyWorld.downedVoidLeviathan = false;
                        break;
                    case 2:
                        MyWorld.downedToySlime = true;
                        MyWorld.downedScourgeFighter = true;
                        MyWorld.downedObsidious = true;
                        MyWorld.downedEye = true;
                        MyWorld.downedAncientWyrm = true;
                        MyWorld.downedGuardian = true;
                        MyWorld.downedVoidEvent = true;
                        MyWorld.downedShadeWyrm = true;
                        MyWorld.downedVolcanox = true;
                        MyWorld.downedAzana = true;
                        MyWorld.sparedAzana = true;
                        MyWorld.downedAncients = true;
                        MyWorld.downedCosmicObserver = true;
                        break;
                    case 3:
                        MyWorld.downedWasteland = true;
                        MyWorld.downedInfernace = true;
                        MyWorld.downedRegaroth = true;
                        MyWorld.downedPermafrost = true;
                        MyWorld.downedAqueous = true;
                        MyWorld.downedVoidLeviathan = true;
                        break;
                }
            }     
            return true;
        }
    }
}
