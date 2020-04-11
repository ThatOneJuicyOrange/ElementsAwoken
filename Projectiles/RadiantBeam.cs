using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles
{
    public class RadiantBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            //projectile.aiStyle = 48;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 4;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 170;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Beam");
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            int dustLength = 8;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.scale *= 1.5f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0 && Main.myPlayer == projectile.owner)
            {
                ProjectileUtils.OutwardsCircleDust(projectile, DustID.PinkFlame, 36, 6f, randomiseVel: true, dustScale: 2f, dustFadeIn: 2.4f);
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), ProjectileType<RadiantStarHoming>(), (int)(projectile.damage * 0.75f), projectile.knockBack, Main.myPlayer, 0f, projectile.whoAmI);
                    Main.PlaySound(SoundID.Item94, projectile.position);
                }
            }
        }
    }
}