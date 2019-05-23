using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialLightning : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;

            projectile.thrown = true;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;

            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            projectile.scale *= 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        }
        public override void AI()
        {
            //arc code
            int num3 = projectile.frameCounter;
            projectile.frameCounter = num3 + 1;
            Lighting.AddLight(projectile.Center, 0.3f, 0.45f, 0.5f);
            if (projectile.velocity == Vector2.Zero)
            {
                if (projectile.frameCounter >= projectile.extraUpdates * 2)
                {
                    projectile.frameCounter = 0;
                    bool flag36 = true;
                    for (int num855 = 1; num855 < projectile.oldPos.Length; num855 = num3 + 1)
                    {
                        if (projectile.oldPos[num855] != projectile.oldPos[0])
                        {
                            flag36 = false;
                        }
                        num3 = num855;
                    }
                    if (flag36)
                    {
                        projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.Next(projectile.extraUpdates) == 0)
                {
                    for (int num856 = 0; num856 < 2; num856 = num3 + 1)
                    {
                        float num857 = projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num858 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector96 = new Vector2((float)Math.Cos((double)num857) * num858, (float)Math.Sin((double)num857) * num858);
                        int num859 = Dust.NewDust(projectile.Center, 0, 0, 226, vector96.X, vector96.Y, 0, default(Color), 1f);
                        Main.dust[num859].noGravity = true;
                        Main.dust[num859].scale = 1.2f;
                        num3 = num856;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Vector2 value39 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                        int num860 = Dust.NewDust(projectile.Center + value39 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                        Dust dust = Main.dust[num860];
                        dust.velocity *= 0.5f;
                        Main.dust[num860].velocity.Y = -Math.Abs(Main.dust[num860].velocity.Y);
                        return;
                    }
                }
            }
            else if (projectile.frameCounter >= projectile.extraUpdates * 2)
            {
                projectile.frameCounter = 0;
                float num861 = projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)projectile.ai[1]);
                int num862 = 0;
                Vector2 vector97 = -Vector2.UnitY;
                Vector2 vector98;
                do
                {
                    int num863 = unifiedRandom.Next();
                    projectile.ai[1] = (float)num863;
                    num863 %= 100;
                    float f = (float)num863 / 100f * 6.28318548f;
                    vector98 = f.ToRotationVector2();
                    if (vector98.Y > 0f)
                    {
                        vector98.Y *= -1f;
                    }
                    bool flag37 = false;
                    if (vector98.Y > -0.02f)
                    {
                        flag37 = true;
                    }
                    if (vector98.X * (float)(projectile.extraUpdates + 1) * 2f * num861 + projectile.localAI[0] > 40f)
                    {
                        flag37 = true;
                    }
                    if (vector98.X * (float)(projectile.extraUpdates + 1) * 2f * num861 + projectile.localAI[0] < -40f)
                    {
                        flag37 = true;
                    }
                    if (!flag37)
                    {
                        goto IL_25086;
                    }
                    num3 = num862;
                    num862 = num3 + 1;
                }
                while (num3 < 100);
                projectile.velocity = Vector2.Zero;
                projectile.localAI[1] = 1f;
                goto IL_25092;
                IL_25086:
                vector97 = vector98;
                IL_25092:
                if (projectile.velocity != Vector2.Zero)
                {
                    projectile.localAI[0] += vector97.X * (float)(projectile.extraUpdates + 1) * 2f * num861;
                    projectile.velocity = vector97.RotatedBy((double)(projectile.ai[0] + 1.57079637f), default(Vector2)) * num861;
                    projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                    return;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)        
        {
            if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            projectile.damage = 0;
            projectile.velocity *= 0;
            target.AddBuff(BuffID.VortexDebuff, 120, false);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Color color = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            Vector2 end = projectile.position + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D drawTex = Main.extraTexture[33]; // the lightning texture
            projectile.GetAlpha(color);
            Vector2 scale16 = new Vector2(projectile.scale) / 2f;
            for (int num289 = 0; num289 < 3; num289++)
            {
                float num298 = (projectile.localAI[1] == -1f || projectile.localAI[1] == 1f) ? -0.2f : 0f;
                if (num289 == 0)
                {
                    scale16 = new Vector2(projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(115, 204, 219, 0) * 0.5f;
                }
                else if (num289 == 1)
                {
                    scale16 = new Vector2(projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                }
                else
                {
                    scale16 = new Vector2(projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                for (int num290 = projectile.oldPos.Length - 1; num290 > 0; num290--)
                {
                    if (!(projectile.oldPos[num290] == Vector2.Zero))
                    {
                        Vector2 start = projectile.oldPos[num290] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = projectile.oldPos[num290 - 1] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, drawTex, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (projectile.oldPos[0] != Vector2.Zero)
                {
                    Vector2 start2 = projectile.oldPos[0] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, drawTex, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                if (projectile.oldPos[i].X == 0f && projectile.oldPos[i].Y == 0f)
                {
                    break;
                }
                projHitbox.X = (int)projectile.oldPos[i].X;
                projHitbox.Y = (int)projectile.oldPos[i].Y;
                if (projHitbox.Intersects(targetHitbox))
                {
                    return true;
                }
            }
            return false;
        }       
    }
}
