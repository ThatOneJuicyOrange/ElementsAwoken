using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CombatDrillHead : ModProjectile
    {
        private float withdrawSpeed = 8f; // speed it goes in
        private int deathTimer = 400;

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.penetrate = 4;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat Drill");
        }
        public override void Kill(int timeLeft)
        {
            Main.projectile[(int)projectile.localAI[0]].Kill();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.ai[0] != 1)
            {
                Player player = Main.player[projectile.owner];
                Main.PlaySound(SoundID.Item10, projectile.position);
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = 0; j <= 1; j++)
                    {
                        Vector2 pos = projectile.Center + new Vector2(16 * i, -14 - 8 * j).RotatedBy((double)projectile.rotation, default(Vector2));
                        Point killPos = pos.ToTileCoordinates();
                        Tile t = Framing.GetTileSafely(killPos.X, killPos.Y);
                        if (t.type != TileID.Trees)player.PickTile(killPos.X, killPos.Y, player.HeldItem.pick);
                    }

                }
            }
            projectile.ai[0] = 1;
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            player.heldProj = projectile.whoAmI;
            player.itemTime = 15;
            player.itemAnimation = 15;
            if (player.dead)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.alpha == 0)
                {
                    if (projectile.position.X + (float)(projectile.width / 2) > player.position.X + (float)(player.width / 2))
                    {
                        player.ChangeDir(1);
                    }
                    else
                    {
                        player.ChangeDir(-1);
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
                float num172 = player.position.X + (float)(player.width / 2) - vector15.X;
                float num173 = player.position.Y + (float)(player.height / 2) - vector15.Y;
                float num174 = (float)Math.Sqrt((double)(num172 * num172 + num173 * num173));
                if (projectile.ai[0] == 0f)
                {
                    if (num174 > 150f) // how long the chain is
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
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.damage = 0;
                projectile.ai[0] = 1;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];

            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/CombatDrillChain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = player.MountedCenter;
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
