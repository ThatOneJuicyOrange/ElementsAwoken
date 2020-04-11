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

namespace ElementsAwoken.NPCs.Bosses.Infernace
{
    public class RockLarge : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 50;

            npc.lifeMax = 600;
            npc.defense = 20;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            NPCID.Sets.NeedsExpertScaling[npc.type] = true;

            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath43;

            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().cantElite = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rock");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1200;
            npc.defense = 30;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 3000;
                npc.defense = 40;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.localAI[0] == 0)
            {
                npc.frame.Y = Main.rand.Next(3) * frameHeight;
                npc.localAI[0]++;
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.spriteDirection = npc.direction;

            npc.rotation += npc.velocity.X * 0.1f;

            NPC parent = Main.npc[(int)npc.ai[1]];

            if (!parent.active || parent.type != ModContent.NPCType<HealingHearth>()) npc.active = false;


            float movespeed = 5f;
            if (Vector2.Distance(parent.Center, npc.Center) >= 120)
            {
                movespeed = 12f;
            }
            Vector2 toTarget = new Vector2(parent.Center.X - npc.Center.X, parent.Center.Y - npc.Center.Y);
            toTarget = new Vector2(parent.Center.X - npc.Center.X, parent.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(parent.Center, npc.Center) >= 70)
            {
                npc.velocity = toTarget * movespeed;
            }
            if (Main.rand.Next(200) == 0)
            {
                npc.velocity += new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)); // for some reason they bunch up in a line on the y axis
            }
            Vector2 diff = parent.position - parent.oldPosition;
            npc.position += diff;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (k != npc.whoAmI && (other.type == npc.type || other.type == mod.NPCType("RockSmall") || other.type == mod.ProjectileType("RockMedium"))
                     && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                {
                    const float pushAway = 0.05f;
                    if (npc.position.X < other.position.X)
                    {
                        npc.velocity.X -= pushAway;
                    }
                    else
                    {
                        npc.velocity.X += pushAway;
                    }
                    if (npc.position.Y < other.position.Y)
                    {
                        npc.velocity.Y -= pushAway;
                    }
                    else
                    {
                        npc.velocity.Y += pushAway;
                    }
                }
            }
        }
       
        public override bool CheckActive()
        {
            return false;
        }
    }
}
