using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Flails
{
    public class ManashardWhipP : ModProjectile
    {
        public int colldedNPCs = 0;
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            //projectile.aiStyle = 13;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manashard Whip");
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
                    if (num174 > 400f) // how long the chain is
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
                    float num175 = 20f;
                    if (num174 < 50f)
                    {
                        projectile.Kill();
                    }
                    num174 = num175 / num174;
                    num172 *= num174;
                    num173 *= num174;
                    projectile.velocity.X = num172;
                    projectile.velocity.Y = num173;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1f;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/Flails/ManashardWhipChain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
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
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            colldedNPCs++;
            if (colldedNPCs >= 2) projectile.ai[0] = 1;
            if (Main.rand.Next(3) == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
                int numberProjectiles = 2;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-9f,9f), Main.rand.NextFloat(-9f, 9f), mod.ProjectileType("Manashatter"), projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
                }
            }
        }
    }
}
