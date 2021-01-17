using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.NPCs
{
    public class TestNPC : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 34;

            npc.aiStyle = -1;

            npc.damage = 24;
            npc.defense = 6;
            npc.lifeMax = 32;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testi");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y != 0)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.velocity.X != 0)
            {
                npc.frameCounter += Math.Abs(npc.velocity.X) * 2;
                if (npc.frameCounter > 6)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 1)  npc.frame.Y = 0;
            }
            else npc.frame.Y = 0;
        }
        public override void AI()
        {
            npc.ai[0] += 0.008f;
            if (npc.velocity.Y == 0)
            {
                npc.velocity.X *= 0.96f;
                npc.rotation = 0;
            }
            else npc.rotation += npc.velocity.Y * 0.02f;
            if (npc.ai[1] == 1)
            {
                npc.Center = Main.MouseWorld;
                npc.velocity = Vector2.Zero; if (Main.mouseRight && Main.mouseRightRelease)
                {
                    npc.ai[1] = 2;
                    Vector2 mouseMove = new Vector2(Main.mouseX - Main.lastMouseX, Main.mouseY - Main.lastMouseY);
                    float len = mouseMove.Length();
                    if (mouseMove != Vector2.Zero)mouseMove.Normalize();
                    npc.velocity = mouseMove * len * 0.3f;
                }
            }
            if (npc.ai[1] == 0 && npc.getRect().Contains(Main.MouseWorld.ToPoint()) && Main.mouseRight && Main.mouseRightRelease)
            {
                npc.ai[1] = 1;
            }
            if (npc.ai[1] == 2) npc.ai[1] = 0; // to stop it picking up instantly
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            {
                Texture2D texture = ModContent.GetTexture("ElementsAwoken/Extra/Aurora");
                float scale = 0.2f;
                float y = 20f * scale;
                float x = 200f * scale;
                float num = 20;
                float num1 = MyWorld.generalTimer / 500f;

                for (int l = 0; l < num; l++)
                {
                    float distance = 360f / num;
                    Vector2 value28 = (Vector2.UnitY.RotatedBy((double)npc.ai[0] + (float)l * distance, default(Vector2)) * new Vector2(x, y)).RotatedBy(MathHelper.ToRadians(10));
                    Vector2 pos = npc.Center;
                    pos += value28;

                    Color color = Color.Lerp(new Color(107, 232, 171, 0), new Color(155, 103, 214, 0), Math.Abs(pos.X - npc.Center.X - x) / (x * 2));
                    float num8 = (float)Math.Sin((double)(num1 * 6.28318548f + 1.57079637f + (float)l / 2f));
                    float rotation = 1.57079637f + num8 * 0.7853982f * -0.3f + 3.14159274f * (float)l;

                    spriteBatch.Draw(texture, pos - Main.screenPosition + new Vector2(0, npc.gfxOffY), null, color, rotation, texture.Size() / 2, Vector2.One * scale, SpriteEffects.None, 0f);
                }
                Lighting.AddLight(npc.Center + new Vector2(-x, 0), 0.6f, 0.4f, 0.8f);
                Lighting.AddLight(npc.Center + new Vector2(x, 0), 0.4f, 0.9f, 0.7f);
            }
            {
                spriteBatch.End();
                spriteBatch.Begin(default, BlendState.Additive);

                Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/LightBeam");
                Vector2 pos = npc.Center - new Vector2(200, 200) - Main.screenPosition;
                Vector2 origin = new Vector2(tex.Width / 2, 0);
                Color color = new Color(190, 190, 150);
                float rot = -3.14f / 4;
                float scale = 2f;
                float spread = 0.6f;
                float speedScale = 180 / spread;
                int numToDraw = (int)(9 * spread);
                for (int k = 0; k <= numToDraw; k++)
                {
                    spriteBatch.Draw(tex, pos, null, color * 0.5f, rot + (float)Math.Sin(MyWorld.generalTimer / speedScale + k) * spread, origin, scale, 0, 0);                         // small beams- medium opacity
                    spriteBatch.Draw(tex, pos, null, color * 0.65f, rot + (float)Math.Sin(MyWorld.generalTimer / speedScale + k + 0.7f) * spread * 0.5f, origin, scale * 1.2f, 0, 0);   // medium beams - strong opacity
                    spriteBatch.Draw(tex, pos, null, color * 0.35f, rot + (float)Math.Sin(MyWorld.generalTimer / speedScale + k + 0.5f) * spread, origin, scale * 1.4f, 0, 0);          // large beams - faint opacity
                }

                spriteBatch.End();
                spriteBatch.Begin();
            }


            return true;
        }
    }
}
