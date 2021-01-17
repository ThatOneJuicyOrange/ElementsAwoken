using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Erius
{
    public class SulfurWeb : ModProjectile
    {
        private bool webbed = false;
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 18;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 6000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfur Web");
        }
        public override void AI()
        {
            Projectile owner = Main.projectile[(int)projectile.ai[1]];
            projectile.ai[0]++;
            if (projectile.ai[0] < 58)
            {
                projectile.velocity.Y = 9;
            }
            else if (webbed)
            {
                if (MathHelper.Distance(owner.position.Y, projectile.position.Y) > 500 || projectile.ai[0] > 600)
                {
                    if (projectile.velocity.Y > -8) projectile.velocity.Y -= 0.09f;
                    if (projectile.Center.Y < owner.Center.Y)
                    {
                        projectile.Kill();
                        owner.Kill();
                    }
                }
                else projectile.velocity.Y *= 0.95f;
            }
            else if (projectile.velocity.Y > 0.05f)
            {
                projectile.velocity.Y *= 0.95f;
            }
            else if (projectile.ai[0] > 300)
            {
                if (projectile.velocity.Y > -6) projectile.velocity.Y -= 0.05f;
                if (projectile.Center.Y < owner.Center.Y)
                {
                    projectile.Kill();
                    owner.Kill();
                }
            }

            if (!webbed && MathHelper.Distance(owner.position.Y, projectile.position.Y) > 100)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (!player.dead && player.active && projectile.Hitbox.Intersects(player.Hitbox) && !player.GetModPlayer<MyPlayer>().acidWebbed)
                    {
                        player.AddBuff(ModContent.BuffType<Buffs.Debuffs.AcidWebbed>(), 300);
                        player.GetModPlayer<MyPlayer>().acidWebbedID = projectile.whoAmI;
                        webbed = true;
                        projectile.ai[0] = 300;
                        break;
                    }
                }
            }
            else
            {
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/NPCProj/Erius/SulfurWebChain");
            Projectile owner = Main.projectile[(int)projectile.ai[1]];

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = owner.Center;
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
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            if (webbed && projectile.ai[0] < 600) return false;
            return true;
        }
    }
}