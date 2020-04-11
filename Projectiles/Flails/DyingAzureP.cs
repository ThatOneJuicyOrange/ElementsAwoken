using System;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Flails
{
    public class DyingAzureP : ModProjectile
    {
        private int deathTimer = 400;
        private float withdrawSpeed = 8f; // speed it goes in

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
            ProjectileUtils.PushOtherEntities(projectile);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 127 }, projectile.damage, "melee");
            projectile.ai[0] = 1;
                return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 127 }, projectile.damage, "melee");
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
    }
}
