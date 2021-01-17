using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable
{
    public class SpoiledFlareberries : ModItem
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

            //item.buffType = mod.BuffType("CalamityPotionBuff");
            //item.buffTime = 10800;
        }
        public override bool UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.flareberryDrugged = modPlayer.flareberryDruggedDuration;
            player.AddBuff(ModContent.BuffType<Buffs.Debuffs.Drugged>(), modPlayer.flareberryDruggedDuration);
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spoiled Flareberry");
            Tooltip.SetDefault("The void has infested the berries inducing extremely unwanted effects\nOr maybe wanted...");
        }
    }
}
