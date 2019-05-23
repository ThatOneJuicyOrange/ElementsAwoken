using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{
    public class MiniatureSandstorm : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = 54;
            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            projectile.alpha = 100;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 317;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miniature Sandstorm");
        }
        public override void AI()
        {
            bool flag64 = projectile.type == mod.ProjectileType("MiniatureSandstorm");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            player.AddBuff(mod.BuffType("MiniSandstormBuff"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.miniatureSandStorm = false;
                }
                if (modPlayer.miniatureSandStorm)
                {
                    projectile.timeLeft = 2;
                }
            }
        }
       
    }
}