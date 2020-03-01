using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.Minions;

namespace ElementsAwoken.Items.BossDrops.zVanilla.Awakened
{
    public class HellishFleshHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.rare = 3;
            item.value = Item.sellPrice(0, 2, 0, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flesh Drive");
            Tooltip.SetDefault("Summons 3 Hungry to steal the life off enemies");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.hellHeart = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<HungryMinion>()] < 3)
            {
                Projectile.NewProjectile(player.Center,Main.rand.NextVector2Square(2,2), ModContent.ProjectileType<HungryMinion>(), 30, 5f, player.whoAmI, 0);
            }
        }
    }
}
