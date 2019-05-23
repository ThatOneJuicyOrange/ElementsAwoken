using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.VoidEventItems
{
    public class VoidJelly : ModItem
    {
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.width = 20;
            item.height = 28;
            item.value = 2000;
            item.rare = 2;
            item.buffType = mod.BuffType("VoidJellyBuff");
            item.buffTime = 940;
            item.healLife = 100;
            item.potion = true;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Jelly");
            Tooltip.SetDefault("It looks like congealed blood...\n15% increased damage");
        }
    }
}
