using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CosmicalusRing : ModProjectile
    {
        public int shootTimer = 100;
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 46;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmicalus");

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            projectile.Center = new Vector2(player.Center.X, player.Center.Y - 60);
            if (player.direction == -1)
            {
                projectile.spriteDirection = -1;
            }
            else
            {
                projectile.spriteDirection = 1;
            }

            if (!modPlayer.cosmicalusArmor)
            {
                projectile.Kill();
            }

            shootTimer--;
            if (shootTimer <= 15 && shootTimer >= 0)
            {
                int maxdusts = 4;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = Main.rand.Next(30, 45);
                    float dustSpeed = 4.5f;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(projectile.Center + offset, 220, velocity, 0, default(Color), 1f);
                    vortex.noGravity = true;
                }
            }
            if (projectile.owner == Main.myPlayer)
            {
                float max = 500f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 12f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                        if (shootTimer <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 30);
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("PlanetarySpike"), 30, projectile.knockBack, projectile.owner);
                            shootTimer = 100;
                        }
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

            float scale = MathHelper.Clamp((float)Math.Sin((Main.time / 19.99f)) * 0.2f, 0f, 1f);
            SpriteEffects spriteEffects = projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float trailScale = (1f + scale) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, trailScale, spriteEffects, 0f);
            }

            Vector2 glowPos = projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            Color color1 = projectile.GetAlpha(lightColor) * 0.4f;


            spriteBatch.Draw(Main.projectileTexture[projectile.type], glowPos, null, color1, projectile.rotation, drawOrigin, 1f + scale, spriteEffects, 0f);
            return true;
        }
    }
}