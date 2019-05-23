using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{
    public class Bubble : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0.2f;
            projectile.timeLeft = 18000;
            projectile.alpha = 100;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = 1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 388;
            projectile.aiStyle = 66;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.04f;
            bool flag64 = projectile.type == mod.ProjectileType("Bubble");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            player.AddBuff(mod.BuffType("BubbleBuff"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.bubble = false;
                }
                if (modPlayer.bubble)
                {
                    projectile.timeLeft = 2;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}