using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable
{
    public class Flareberries : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;

            item.useTurn = true;
            item.consumable = true;

            item.maxStack = 30;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 9;

            item.potion = true;
            item.healLife = 75;

            item.buffType = ModContent.BuffType<Buffs.PotionBuffs.DrakoniteSkinBuff>();
            item.buffTime = 900;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flareberry");
            Tooltip.SetDefault("Grants 15 seconds of full plateau lava immunity");
        }
    }
}
