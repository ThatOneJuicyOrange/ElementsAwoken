using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousFirePortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 126;
            projectile.height = 126;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hostile = true;
            projectile.alpha = 255;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Portal");
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Color color = projectile.GetAlpha(lightColor);
            Texture2D headTexture = mod.GetTexture("Projectiles/NPCProj/Obsidious/ObsidiousFirePortalCenter");
            Vector2 drawOrigin = new Vector2(headTexture.Width * 0.5f, headTexture.Height * 0.5f);
            sb.Draw(headTexture, new Vector2(projectile.Center.X, projectile.Center.Y) - Main.screenPosition - drawOrigin, null, color, 0f, Vector2.Zero, projectile.scale, SpriteEffects.None, 0f);
            return true;
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] < 1)
            {
                projectile.scale = 0.01f;
            }
                if (projectile.localAI[0] <= 60)
            {
                projectile.alpha -= 15;
                projectile.scale += 0.05f;
            }
            if (projectile.localAI[0] >= 360)
            {
                projectile.alpha += 15;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
            if (projectile.scale > 1f)
            {
                projectile.scale = 1f;
            }
            projectile.rotation += 0.15f;

            int maxdusts = 10;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 75;
                float dustSpeed = 6;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(projectile.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                vortex.noGravity = true;
            }
            Player player = Main.player[Main.myPlayer];
            Vector2 playerPos = new Vector2(Main.player[Main.myPlayer].Center.X, Main.player[Main.myPlayer].Center.Y);
            projectile.localAI[1]--;
            if (projectile.localAI[0] >= 60 && projectile.localAI[0] <= 360)
            {
                if (projectile.localAI[1] <= 0)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 shootVel = playerPos - projectile.Center;

                        Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                        int type = mod.ProjectileType("SolarFragmentProj");
                        float Speed = 12f;
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int damage = projectile.damage / 3;
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                        Projectile.NewProjectile(vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        projectile.localAI[1] = Main.rand.Next(8, 24);
                    }
                }
            }
        }

    }
}
