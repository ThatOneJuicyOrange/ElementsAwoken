using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Flails
{
    public class DyingAzureP : ModProjectile
    {
        public int deathTimer = 400;
        public float withdrawSpeed = 8f; // speed it goes in

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.melee = true;
            //projectile.aiStyle = 13;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dying Azure");
        }
        public override void AI()
        {
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.alpha == 0)
                {
                    if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
                    {
                        Main.player[projectile.owner].ChangeDir(1);
                    }
                    else
                    {
                        Main.player[projectile.owner].ChangeDir(-1);
                    }
                }
                //if (projectile.type == 481)
                {
                    if (projectile.ai[0] == 0f)
                    {
                        projectile.extraUpdates = 0;
                    }
                    else
                    {
                        projectile.extraUpdates = 1;
                    }
                }
                Vector2 vector15 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num172 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector15.X;
                float num173 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector15.Y;
                float num174 = (float)Math.Sqrt((double)(num172 * num172 + num173 * num173));
                if (projectile.ai[0] == 0f)
                {
                    if (num174 > 700f) // how long the chain is
                    {
                        projectile.ai[0] = 1f;
                    }
                    projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                    projectile.ai[1] = projectile.ai[1] += 1f;
                    if (projectile.ai[1] > 5f)
                    {
                        projectile.alpha = 0;
                    }
                    projectile.ai[1] = 8f;
                    if (projectile.ai[1] >= 10f)
                    {
                        projectile.ai[1] = 15f;
                        projectile.velocity.Y = projectile.velocity.Y += 0.3f;
                    }
                }
                else if (projectile.ai[0] == 1f)
                {
                    projectile.tileCollide = false;
                    projectile.rotation = (float)Math.Atan2((double)num173, (double)num172) - 1.57f;
                    if (num174 < 50f)
                    {
                        projectile.Kill();
                    }
                    num174 = withdrawSpeed / num174;
                    num172 *= num174;
                    num173 *= num174;
                    projectile.velocity.X = num172;
                    projectile.velocity.Y = num173;
                }
            }
            deathTimer--;
            if (deathTimer <= 0)
            {
                withdrawSpeed = 20f;
            }
            if (deathTimer > 50)
            {
                if (Main.rand.Next(18) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), mod.ProjectileType("ChaosFireball"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
            for (int k = 0; k < 200; k++)
            {
                Projectile other = Main.projectile[k];
                if (k != projectile.whoAmI && other.type == projectile.type && other.active && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    const float pushAway = 0.05f;
                    if (projectile.position.X < other.position.X)
                    {
                        projectile.velocity.X -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.X += pushAway;
                    }
                    if (projectile.position.Y < other.position.Y)
                    {
                        projectile.velocity.Y -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.Y += pushAway;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/Flails/DyingAzureChain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explosion();
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Explosion();
        }
        private void Explosion()
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}
