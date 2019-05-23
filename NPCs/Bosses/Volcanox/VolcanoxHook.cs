using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Volcanox
{
    public class VolcanoxHook : ModNPC
    {
        public override void SetDefaults()
        {
            npc.lifeMax = 10000;
            npc.damage = 60;
            npc.defense = 25;
            npc.knockBackResist = 0f;

            npc.width = 38;
            npc.height = 56;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 1f;

            npc.immortal = true;
            npc.dontTakeDamage = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanox Hook");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X == 0 || npc.velocity.Y == 0)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            if (npc.velocity.X > 0 || npc.velocity.Y > 0 || npc.velocity.X < 0 || npc.velocity.Y < 0)
            {
                npc.frame.Y = 1 * frameHeight;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            npc.immortal = true;
            bool flag48 = false;
            bool flag49 = false;
            if (!NPC.AnyNPCs(mod.NPCType("Volcanox")))
            {
                npc.active = false;
            }
            NPC parent = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == mod.NPCType("Volcanox"))
                {
                    parent = Main.npc[i];
                    break;
                }
            }
            if (Main.player[parent.target].dead)
            {
                flag49 = true;
            }
            if (((mod.NPCType("Volcanox") != -1 && !Main.player[parent.target].ZoneUnderworldHeight) || flag49))
            {
                npc.localAI[0] -= 4f;
                flag48 = true;
            }
            if (Main.netMode == 1)
            {
                if (npc.ai[0] == 0f)
                {
                    npc.ai[0] = (float)((int)(npc.Center.X / 16f));
                }
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)((int)(npc.Center.X / 16f));
                }
            }
            if (Main.netMode != 1)
            {
                if (npc.ai[0] == 0f || npc.ai[1] == 0f)
                {
                    npc.localAI[0] = 0f;
                }
                npc.localAI[0] -= 1f;
                if (parent.life < parent.lifeMax / 2)
                {
                    npc.localAI[0] -= 2f;
                }
                if (parent.life < parent.lifeMax / 4)
                {
                    npc.localAI[0] -= 2f;
                }
                if (flag48)
                {
                    npc.localAI[0] -= 6f;
                }
                if (!flag49 && npc.localAI[0] <= 0f && npc.ai[0] != 0f)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (i != npc.whoAmI && Main.npc[i].active && Main.npc[i].type == npc.type && (Main.npc[i].velocity.X != 0f || Main.npc[i].velocity.Y != 0f))
                        {
                            npc.localAI[0] = (float)Main.rand.Next(60, 300);
                        }
                    }
                }
                if (npc.localAI[0] <= 0f)
                {
                    npc.localAI[0] = (float)Main.rand.Next(100, 300);
                    bool flag50 = false;
                    int num764 = 0;
                    while (!flag50 && num764 <= 1000)
                    {
                        num764++;
                        int num765 = (int)(Main.player[parent.target].Center.X / 16f);
                        int num766 = (int)(Main.player[parent.target].Center.Y / 16f);
                        if (npc.ai[0] == 0f)
                        {
                            num765 = (int)((Main.player[parent.target].Center.X + parent.Center.X) / 32f);
                            num766 = (int)((Main.player[parent.target].Center.Y + parent.Center.Y) / 32f);
                        }
                        if (flag49)
                        {
                            num765 = (int)parent.position.X / 16;
                            num766 = (int)(parent.position.Y + 400f) / 16;
                        }
                        int num767 = 20;
                        num767 += (int)(100f * ((float)num764 / 1000f));
                        int num768 = num765 + Main.rand.Next(-num767, num767 + 1);
                        int num769 = num766 + Main.rand.Next(-num767, num767 + 1);
                        if (parent.life < parent.lifeMax / 2 && Main.rand.Next(6) == 0)
                        {
                            npc.TargetClosest(true);
                            int num770 = (int)(Main.player[npc.target].Center.X / 16f);
                            int num771 = (int)(Main.player[npc.target].Center.Y / 16f);
                            if (Main.tile[num770, num771].wall > 0)
                            {
                                num768 = num770;
                                num769 = num771;
                            }
                        }
                        try
                        {
                            if (WorldGen.SolidTile(num768, num769) || (Main.tile[num768, num769].wall > 0 && (num764 > 500 || parent.life < parent.lifeMax / 2)))
                            {
                                flag50 = true;
                                npc.ai[0] = (float)num768;
                                npc.ai[1] = (float)num769;
                                npc.netUpdate = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (npc.ai[0] > 0f && npc.ai[1] > 0f)
            {
                float num772 = 10f;
                if (parent.life < parent.lifeMax / 2)
                {
                    num772 = 14f;
                }
                if (parent.life < parent.lifeMax / 4)
                {
                    num772 = 16f;
                }
                if (Main.expertMode)
                {
                    num772 += 1f;
                }
                if (Main.expertMode && parent.life < parent.lifeMax / 2)
                {
                    num772 += 1f;
                }
                if (flag48)
                {
                    num772 *= 2f;
                }
                if (flag49)
                {
                    num772 *= 2f;
                }
                Vector2 vector95 = new Vector2(npc.Center.X, npc.Center.Y);
                float num773 = npc.ai[0] * 16f - 8f - vector95.X;
                float num774 = npc.ai[1] * 16f - 8f - vector95.Y;
                float num775 = (float)Math.Sqrt((double)(num773 * num773 + num774 * num774));
                if (num775 < 12f + num772)
                {
                    npc.velocity.X = num773;
                    npc.velocity.Y = num774;
                }
                else
                {
                    num775 = num772 / num775;
                    npc.velocity.X = num773 * num775;
                    npc.velocity.Y = num774 * num775;
                }
                Vector2 vector96 = new Vector2(npc.Center.X, npc.Center.Y);
                float num776 = parent.Center.X - vector96.X;
                float num777 = parent.Center.Y - vector96.Y;
                npc.rotation = (float)Math.Atan2((double)num777, (double)num776) - 1.57f;
                return;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModLoader.GetTexture("ElementsAwoken/NPCs/Bosses/Volcanox/VolcanoxChain");
            NPC parent = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == mod.NPCType("Volcanox"))
                {
                    parent = Main.npc[i];
                    break;
                }
            }
            Vector2 position = npc.Center;
            Vector2 mountedCenter = parent.Center;
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
                    color2 = npc.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 70;
            npc.lifeMax = 40000;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
