using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    public class CrystallineCluster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.damage = 0;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item2;

            item.rare = 6;
            item.value = Item.sellPrice(0, 5, 0, 0);

            item.noMelee = true;

            item.shoot = mod.ProjectileType("PossessedHand");
            item.buffType = mod.BuffType("PossessedHandBuff");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Cluster");
            Tooltip.SetDefault("Summons a possessed hand");
        }


        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
