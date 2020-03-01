using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class DimensionalHive : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 120;

            npc.aiStyle = -1;

            NPCID.Sets.NeedsExpertScaling[npc.type] = true;

            npc.defense = 35;
            npc.lifeMax = 10000;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath5;

            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.buffImmune[24] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dimensional Hive");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 15000;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 20000;
                npc.defense = 65;
            }
        }
        public override void NPCLoot()
        {
            int essenceAmount = Main.rand.Next(5, 8);
            int width = npc.width / 2;
            int height = npc.height / 2;
            for (int i = 0; i < essenceAmount; i++)
            {
                Item item = Main.item[Item.NewItem((int)npc.Center.X + Main.rand.Next(-width, width), (int)npc.Center.Y + Main.rand.Next(-height, height), 2, 2, mod.ItemType("VoidEssence"))];
                Vector2 vel = item.Center - npc.Center;
                vel.Normalize();
                item.velocity = vel * 5f;
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5));
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (ModContent.GetInstance<Config>().lowDust)
            {
                float maxDist = 600;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];
                    if (VoidEvent.phase1NPCs.Contains(other.type) && Vector2.Distance(npc.Center, other.Center) < maxDist)
                    {
                        Texture2D texture = ModContent.GetTexture("ElementsAwoken/NPCs/VoidEventEnemies/Phase1/HiveBeam");

                        Vector2 position = npc.Center;
                        Vector2 mountedCenter = other.Center;
                        Rectangle? sourceRectangle = new Rectangle?();
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

                                Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                    }
                }
            }
            return true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];

            if (npc.localAI[0] == 0)
            {
                npc.ai[0] = npc.position.Y;
                npc.localAI[0]++;
            }
            npc.ai[1]++;
            npc.position.Y = npc.ai[0] + (float)Math.Sin(npc.ai[1] / 30) * 30;


            float maxDist = 600;
            int maxHealing = 5;
            int numHealing = 0;
            bool runCode = true;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC other = Main.npc[i];
                if (other.active)
                {
                    if (VoidEvent.phase1NPCs.Contains(other.type) && Vector2.Distance(npc.Center, other.Center) < maxDist && other.type != npc.type && other.type != mod.NPCType("VoidFly") && numHealing < maxHealing)
                    {
                        numHealing++;
                        int healamount = (int)(other.lifeMax * 0.05f);
                        if (other.life <= other.lifeMax - healamount && npc.ai[1] % 20 == 0) other.life += healamount;
                        if (!ModContent.GetInstance<Config>().lowDust)
                        {
                            for (int k = 0; k < 10; k++)
                            {
                                Dust d = Main.dust[Dust.NewDust(npc.Center + (other.Center - npc.Center) * Main.rand.NextFloat() - new Vector2(4, 4), 0, 0, 127)];
                                d.noGravity = true;
                                d.velocity *= 0.04f;
                                d.scale *= 0.8f;
                            }
                        }
                    }
                    if (other.whoAmI != npc.whoAmI && other.type == npc.type && Vector2.Distance(npc.Center, other.Center) < maxDist * 1.75 && npc.ai[1] < other.ai[1])
                    {
                        npc.active = false;
                        runCode = false; // so the dust isnt created when it vanishes
                    }
                }
            }
            if (runCode)
            {
                for (int i = 0; i < 120; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(npc.Center + offset - new Vector2(4, 4), 0, 0, 127)];
                    dust.noGravity = true;
                }
            }
        }
    }
}