using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.NPCProj.Permafrost
{
    public class HomingIce : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/NPCProj/Permafrost/IceRain"; } }

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 44;

            projectile.hostile = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Ice");
        }
        public override void AI()
        {
            projectile.rotation += 0.05f;

            Player P = Main.LocalPlayer;
            float goToX = P.Center.X - projectile.Center.X;
            float goToY = P.Center.Y - projectile.Center.Y;
            float speed = 0.2f;
            if (projectile.velocity.X <goToX)
            {
                projectile.velocity.X = projectile.velocity.X + speed;
                if (projectile.velocity.X < 0f &&goToX > 0f)
                {
                    projectile.velocity.X = projectile.velocity.X + speed;
                }
            }
            else if (projectile.velocity.X >goToX)
            {
                projectile.velocity.X = projectile.velocity.X - speed;
                if (projectile.velocity.X > 0f &&goToX < 0f)
                {
                    projectile.velocity.X = projectile.velocity.X - speed;
                }
            }
            if (projectile.velocity.Y < goToY)
            {
                projectile.velocity.Y = projectile.velocity.Y + speed;
                if (projectile.velocity.Y < 0f && goToY > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + speed;
                    return;
                }
            }
            else if (projectile.velocity.Y > goToY)
            {
                projectile.velocity.Y = projectile.velocity.Y - speed;
                if (projectile.velocity.Y > 0f && goToY < 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - speed;
                    return;
                }
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 120, false);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.Center);
            float speed = 5f;
            float numberProjectiles = MyWorld.awakenedMode ? 6 : 4;
            float rotation = MathHelper.ToRadians(360);
            float rotateExtra = MathHelper.ToRadians(Main.rand.Next(90));
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                perturbedSpeed.RotatedBy(rotateExtra);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<FrostExplosion>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            }
        }
    }
}