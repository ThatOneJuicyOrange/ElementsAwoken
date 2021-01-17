using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Held.Drills
{
    public class MatterManipulator : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.9f);
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.4f, 0.2f, 0.9f);

            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 60f)
            {
                projectile.localAI[0] = 0f;
            }
            if (Vector2.Distance(vector, projectile.Center) >= 5f)
            {
                float num8 = projectile.localAI[0] / 60f;
                if (num8 > 0.5f)
                {
                    num8 = 1f - num8;
                }
                Vector3 arg_548_0 = new Vector3(0f, 1f, 0.7f);
                Vector3 value3 = new Vector3(0f, 0.7f, 1f);
                Vector3 vector6 = Vector3.Lerp(arg_548_0, value3, 1f - num8 * 2f) * 0.5f;
                if (Vector2.Distance(vector, projectile.Center) >= 30f)
                {
                    Vector2 vector7 = projectile.Center - vector;
                    vector7.Normalize();
                    vector7 *= Vector2.Distance(vector, projectile.Center) - 30f;
                    DelegateMethods.v3_1 = vector6 * 0.8f;
                    Utils.PlotTileLine(projectile.Center - vector7, projectile.Center, 8f, new Utils.PerLinePoint(DelegateMethods.CastLightOpen));
                }
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, vector6.X, vector6.Y, vector6.Z);
            }
            if (Main.myPlayer == projectile.owner)
            {
                if (projectile.localAI[1] > 0f)
                {
                    projectile.localAI[1] -= 1f;
                }
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
                else if (projectile.localAI[1] == 0f)
                {
                    Vector2 vector8 = vector;
                    Vector2 vector9 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector8;
                    if (player.gravDir == -1f)
                    {
                        vector9.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector8.Y;
                    }
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].active())
                    {
                        vector9 = new Vector2((float)Player.tileTargetX, (float)Player.tileTargetY) * 16f + Vector2.One * 8f - vector8;
                        projectile.localAI[1] = 2f;
                    }
                    vector9 = Vector2.Lerp(vector9, projectile.velocity, 0.7f);
                    if (float.IsNaN(vector9.X) || float.IsNaN(vector9.Y))
                    {
                        vector9 = -Vector2.UnitY;
                    }
                    float num9 = 30f;
                    if (vector9.Length() < num9)
                    {
                        vector9 = Vector2.Normalize(vector9) * num9;
                    }
                    int tileBoost = player.inventory[player.selectedItem].tileBoost;
                    int num10 = -Player.tileRangeX - tileBoost + 1;
                    int num11 = Player.tileRangeX + tileBoost - 1;
                    int num12 = -Player.tileRangeY - tileBoost;
                    int num13 = Player.tileRangeY + tileBoost - 1;
                    int num14 = 12;
                    bool flag2 = false;
                    if (vector9.X < (float)(num10 * 16 - num14))
                    {
                        flag2 = true;
                    }
                    if (vector9.Y < (float)(num12 * 16 - num14))
                    {
                        flag2 = true;
                    }
                    if (vector9.X > (float)(num11 * 16 + num14))
                    {
                        flag2 = true;
                    }
                    if (vector9.Y > (float)(num13 * 16 + num14))
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        Vector2 vector10 = Vector2.Normalize(vector9);
                        float num15 = -1f;
                        if (vector10.X < 0f && ((float)(num10 * 16 - num14) / vector10.X < num15 || num15 == -1f))
                        {
                            num15 = (float)(num10 * 16 - num14) / vector10.X;
                        }
                        if (vector10.X > 0f && ((float)(num11 * 16 + num14) / vector10.X < num15 || num15 == -1f))
                        {
                            num15 = (float)(num11 * 16 + num14) / vector10.X;
                        }
                        if (vector10.Y < 0f && ((float)(num12 * 16 - num14) / vector10.Y < num15 || num15 == -1f))
                        {
                            num15 = (float)(num12 * 16 - num14) / vector10.Y;
                        }
                        if (vector10.Y > 0f && ((float)(num13 * 16 + num14) / vector10.Y < num15 || num15 == -1f))
                        {
                            num15 = (float)(num13 * 16 + num14) / vector10.Y;
                        }
                        vector9 = vector10 * num15;
                    }
                    if (vector9.X != projectile.velocity.X || vector9.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector9;
                }
            }

            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipHorizontally;

            Vector2 vector63 = projectile.position + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Vector2 vector64 = Main.player[projectile.owner].RotatedRelativePoint(player.MountedCenter, true) + Vector2.UnitY * Main.player[projectile.owner].gfxOffY;
            Vector2 vector65 = vector63 + Main.screenPosition - vector64;
            Vector2 value42 = Vector2.Normalize(vector65);
            float num296 = 2f;

            // actual drill
            Item held = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem];
            Texture2D texture2D38 = Main.itemTexture[held.type];
            Vector2 drillPos = vector64 + value42 * num296;
            Color color25 = Lighting.GetColor((int)drillPos.X / 16,(int)drillPos.Y / 16);
            drillPos -= Main.screenPosition;
            Main.spriteBatch.Draw(texture2D38, drillPos, null, color25, projectile.rotation + 1.57079637f + ((spriteEffects == SpriteEffects.None) ? 3.14159274f : 0f), new Vector2((float)((spriteEffects == SpriteEffects.None) ? 0 : texture2D38.Width), (float)texture2D38.Height / 2f) + Vector2.UnitY * 1f, held.scale, spriteEffects, 0f);


            // chain
            Vector2 thing = projectile.velocity;
            thing.Normalize();
            thing *= 26f;
            Vector2 yAdd = new Vector2(0, 0);
            if (player.direction == 1)
            {
                yAdd.Y = 6;
            }
            else
            {
                yAdd.Y = -6;
            }

            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/Held/Drills/ManipulatorChain");
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
            Color color = new Color(100, 100, 100, 0);
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
                    vector2_4 = mountedCenter - position + thing.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2)) - yAdd;
                    //Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    for (int k = 0; k < 7; k++)
                    {
                        Vector2 newPos = position + new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                        Main.spriteBatch.Draw(texture, newPos - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            // tip
            Texture2D tipTex = Main.projectileTexture[projectile.type];
            for (int k = 0; k < 7; k++)
            {
                Vector2 newPos = projectile.Center + new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                Main.spriteBatch.Draw(tipTex, newPos - Main.screenPosition, sourceRectangle, color, rotation, tipTex.Size() / 2, 1f, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}