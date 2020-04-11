using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles
{
    public class ShotstormDartStuck : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/ShotstormDart"; } }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.timeLeft = 300;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shotstorm Dart");
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)projectile.ai[0]];
                if (stick.active)
                {
                    projectile.Center = stick.Center - projectile.velocity * 2f;
                    projectile.gfxOffY = stick.gfxOffY;

                    projectile.alpha += 3;
                    if (projectile.alpha >= 255) projectile.Kill();
                }
                else projectile.Kill();
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
             return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = target.whoAmI;
            projectile.ai[1] = 1;
            projectile.velocity = (target.Center - projectile.Center) * 0.75f;
            projectile.netUpdate = true;
            target.AddBuff(BuffType<FastPoison>(), 60);
        }
    }
}