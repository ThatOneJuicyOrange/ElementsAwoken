using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.RadiantMaster
{
    public class RadiantMasterStar : ModProjectile
    {
        private float rotSpeed = 2;
        public float aiTimer = 0;
        private int dist = 0;
        private int orbitDur = 140;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(dist);
            writer.Write(rotSpeed);
            writer.Write(aiTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            dist = reader.ReadInt32();
            rotSpeed = reader.ReadSingle();
            aiTimer = reader.ReadSingle();
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.scale *= 1.1f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkle");
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[1]];
            Player player = Main.LocalPlayer;
            if (rotSpeed > 0 && aiTimer > 0) rotSpeed -= 2 / orbitDur;

           if (dist < 120) dist += 2;
            if (projectile.localAI[1] == 0) aiTimer += 1f;
            if (aiTimer == orbitDur)
            {
                Main.PlaySound(SoundID.Item9, projectile.position);
                double angle = Math.Atan2(player.position.Y - projectile.position.Y, player.position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 20f;
            }
            else if (aiTimer < orbitDur)
            {
                projectile.ai[0] += rotSpeed; // speed
                int distance = dist;
                double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
                projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
                projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;

                if (!parent.active) projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(projectile.Center, 0, 0, DustID.PinkFlame, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 vector11 = new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2));

            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 vector40 = projectile.Center - Main.screenPosition;
            vector40 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[projectile.type])) * projectile.scale / 2f;
            vector40 += vector11 * projectile.scale + new Vector2(0f, projectile.gfxOffY);
            float num147 = 1f / (float)projectile.oldPos.Length * 1.1f;
            int num148 = projectile.oldPos.Length - 1;
            if (aiTimer >= orbitDur)
            {
                while ((float)num148 >= 0f)
                {
                    float num149 = (float)(projectile.oldPos.Length - num148) / (float)projectile.oldPos.Length;
                    Color color35 = Color.White;
                    color35 *= 1f - num147 * (float)num148 / 1f;
                    color35.A = (byte)((float)color35.A * (1f - num149));
                    Main.spriteBatch.Draw(texture, vector40 + projectile.oldPos[num148] - projectile.position, null, color35, projectile.oldRot[num148], vector11, projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                    num148--;
                }
            }
            texture = Main.extraTexture[57];
            Main.spriteBatch.Draw(texture, vector40, null, Color.HotPink, 0f, texture.Size() / 2f, projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}