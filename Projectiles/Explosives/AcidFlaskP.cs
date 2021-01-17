using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Explosives
{
    internal class AcidFlaskP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.hostile = true;
            projectile.penetrate = -1;

            projectile.timeLeft = 300;

            drawOffsetX = 5;
            drawOriginOffsetY = 5;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    damage /= 5;
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3) return base.CanHitNPC(target);
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3) return base.CanHitPlayer(target);
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = 3;
            return false;
        }

        public override void AI()
        {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                projectile.damage = 100;
                projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                projectile.alpha = 255;
                // make hitbox big
                projectile.position = projectile.Center;
                projectile.width = 50;
                projectile.height = 50;
                projectile.Center = projectile.position;
                projectile.knockBack = 10f;
            }
            else
            {
                if (Main.rand.NextBool())
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, 0f, 0f, 100, default(Color), 1f)];
                    dust.position = projectile.Center + new Vector2(11, -11).RotatedBy((double)projectile.rotation, default(Vector2));
                    dust.scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
                    dust.fadeIn = 1f;
                    dust.noGravity = true;
                    dust.velocity *= 0.2f;
                    dust.noLight = true;
                }
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 5f)
            {
                projectile.ai[0] = 10f;
                // Roll speed dampening.
                if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.97f;

                    if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
                    {
                        projectile.velocity.X = 0f;
                        projectile.netUpdate = true;
                    }
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
            projectile.rotation += projectile.velocity.X * 0.1f;
            return;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Shatter, projectile.position);

            for (int i = 0; i < 10; i++)
            {
                Projectile.NewProjectile(projectile.Center, Main.rand.NextVector2Square(-2,2), ModContent.ProjectileType<AcidFlaskAcid>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
            }

            // reset size to normal width and height.
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 10;
            projectile.height = 10;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            ProjectileUtils.ExplosiveKillTiles(projectile, 3);
        }
    }
}
