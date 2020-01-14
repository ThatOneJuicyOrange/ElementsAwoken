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
    public class VolcanoxTentacle : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 40;

            npc.lifeMax = 15000;
            npc.damage = 90;
            npc.defense = 25;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 1f;
            npc.netAlways = true;

            // all EA modded buffs (unless i forget to add new ones)
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("AncientDecay")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("DragonFire")] = true;
            npc.buffImmune[mod.BuffType("Discord")] = true;
            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanox Tentacle");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 120;
            npc.lifeMax = 25000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 35000;
                npc.damage = 150;
                npc.defense = 30;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
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
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.localAI[0] -= 1f;
                if (npc.localAI[0] <= 0f)
                {
                    npc.localAI[0] = (float)Main.rand.Next(120, 480);
                    npc.ai[0] = (float)Main.rand.Next(-100, 101);
                    npc.ai[1] = (float)Main.rand.Next(-100, 101);
                    npc.netUpdate = true;
                }
            }
            npc.TargetClosest(true);
            float speed = 0.6f;
            float num780 = 200f;
            if ((double)parent.life < (double)parent.lifeMax * 0.25)
            {
                num780 += 100f;
            }
            if ((double)parent.life < (double)parent.lifeMax * 0.1)
            {
                num780 += 100f;
            }
            if (Main.expertMode)
            {
                float num781 = 1f - (float)npc.life / (float)npc.lifeMax;
                num780 += num781 * 300f;
                speed += 0.3f;
            }
            if (!parent.active || mod.NPCType("Volcanox") < 0)
            {
                npc.active = false;
                return;
            }
            float targetX = parent.position.X + (float)(parent.width / 2);
            float targetY = parent.position.Y + (float)(parent.height / 2);
            Vector2 vector97 = new Vector2(targetX, targetY);
            float num784 = targetX + npc.ai[0];
            float num785 = targetY + npc.ai[1];
            float num786 = num784 - vector97.X;
            float num787 = num785 - vector97.Y;
            float num788 = (float)Math.Sqrt((double)(num786 * num786 + num787 * num787));
            num788 = num780 / num788;
            num786 *= num788;
            num787 *= num788;
            if (npc.position.X < targetX + num786)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && num786 > 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            else if (npc.position.X > targetX + num786)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && num786 < 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            if (npc.position.Y < targetY + num787)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && num787 > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
            }
            else if (npc.position.Y > targetY + num787)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && num787 < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
            }
            float maxSpeed = 12f;
            if (npc.velocity.X > maxSpeed)
            {
                npc.velocity.X = maxSpeed;
            }
            if (npc.velocity.X < -maxSpeed)
            {
                npc.velocity.X = -maxSpeed;
            }
            if (npc.velocity.Y > maxSpeed)
            {
                npc.velocity.Y = maxSpeed;
            }
            if (npc.velocity.Y < -maxSpeed)
            {
                npc.velocity.Y = -maxSpeed;
            }
            if (num786 > 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = (float)Math.Atan2((double)num787, (double)num786);
            }
            if (num786 < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = (float)Math.Atan2((double)num787, (double)num786) + 3.14f;
                return;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("ElementsAwoken/NPCs/Bosses/Volcanox/VolcanoxChain2");
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
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 0.9f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
