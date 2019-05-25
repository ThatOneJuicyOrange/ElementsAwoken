using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Consumable
{
    public class ElementalCapsule : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.expert = true;
            item.consumable = true;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item119;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Capsule");
            Tooltip.SetDefault("Forces bend around this capsule...\nOnly use if you are up for a challenge\nActivates [c/f442aa:Awakened Mode] and sanity");
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.expertMode)
            {
                Main.NewText("The forces of the world get twisted beyond imagination...", Color.DeepPink);
                MyWorld.awakenedMode = true;
                item.stack--;
                return true;
            }
            return false;
        }
    }
}
