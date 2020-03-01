using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.BossDrops.zVanilla.Awakened
{
    public class SlimeBooster : ModItem
    {
        private int timer = 0;
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
            DisplayName.SetDefault("Slime Booster");
            Tooltip.SetDefault("Walking leaves a acidic slimy trail\nGreatly increased jump speed\nWhen the player is hit, they shoot out slime spikes");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            timer++;
               MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.slimeBooster = true;
            player.jumpSpeedBoost += 1.75f;
            if (player.velocity.X != 0 && player.velocity.Y == 0)
            {
                int mod = (int)MathHelper.Lerp(5, 1, MathHelper.Clamp(Math.Abs(player.velocity.X) / 20f, 0, 1));
                int timeleft = (int)MathHelper.Lerp(60, 20, MathHelper.Clamp(Math.Abs(player.velocity.X) / 20f, 0, 1));
                if (timer  % mod == 0 &&  Main.myPlayer == player.whoAmI)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(player.Bottom.X, player.Bottom.Y - 2, 0f, 0f, ModContent.ProjectileType<SlimeBoosterTrail>(), 10, 0f, Main.myPlayer)];
                    proj.timeLeft = timeleft;
                }
            }
        }
    }
}
