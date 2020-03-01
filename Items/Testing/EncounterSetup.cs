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
                if (mode > 3)
                {
                    mode = 0;
                }
                Main.NewText(mode, Color.White);
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
                ElementsAwoken.encounter = mode;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.encounterSetup = false;
               /* if (Main.netMode == NetmodeID.Server)
                {
                        NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }*/
            }
            return true;
        }
    }
}
