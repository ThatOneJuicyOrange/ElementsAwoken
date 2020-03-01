using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.BossDrops.zVanilla.Awakened
{
    public class BleedingHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.rare = 1;
            item.value = Item.sellPrice(0, 2, 0, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bleeding Heart");
            Tooltip.SetDefault("Releases homing projectiles when dashing\nKilling 5 enemies in 10 seconds grants the player, 'Bloodbath' which increases damage by 20%");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.rand.NextBool(4))
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 5, 0, 0, 0, default(Color))];
                dust.velocity.X *= 0.2f;
                dust.velocity.Y = 0;
            }

            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.bleedingHeart = true;

        }
    }
}
