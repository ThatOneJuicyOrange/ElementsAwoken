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
    public class HealingHearth : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 26;

            npc.lifeMax = 1000;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            NPCID.Sets.NeedsExpertScaling[npc.type] = true;

            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath58;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healing Hearth");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 4000;
            }
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.spriteDirection = npc.direction;

            npc.rotation = npc.velocity.X * 0.1f;

            NPC parent = Main.npc[(int)npc.ai[1]];
            npc.localAI[1]++;
            if (!parent.active) npc.active = false;
            else
            {
                if (parent.life < parent.lifeMax * 0.75f && Vector2.Distance(npc.Center, parent.Center) < 600 && npc.localAI[1] % 2 == 0) parent.life++;
            }
            Vector2 target = new Vector2(parent.Center.X, parent.Center.Y - 300);
            if (npc.Center.X > parent.Center.X) target.X = parent.Center.X + 200;
            else target.X = parent.Center.X - 100;
            Move(target, 0.3f);
            if (npc.localAI[0] == 0)
            {
                int numRocks = 3;
                if (Main.expertMode) numRocks = 5;
                if (MyWorld.awakenedMode) numRocks = 8;
                for (int i = 0; i < numRocks; i++)
                {
                    int type = mod.NPCType("RockSmall");
                    int choice = Main.rand.Next(3);
                    if (choice == 0) type = mod.NPCType("RockSmall");
                    else if (choice == 1) type = mod.NPCType("RockMedium");
                    else if (choice == 2) type = mod.NPCType("RockLarge");

                    NPC rock = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, type)];
                    rock.ai[1] = npc.whoAmI;
                    rock.velocity = new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
                }
                npc.localAI[0]++;
            }

            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.4f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }

        private void Move(Vector2 target, float speed)
        {
            int maxDist = 500;
            if (Vector2.Distance(target, npc.Center) >= maxDist)
            {
                float moveSpeed = 14f;
                Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            else
            {
                npc.spriteDirection = npc.direction;

                float gotoX = target.X - npc.Center.X;
                float gotoY = target.Y - npc.Center.Y;
                if (npc.velocity.X < gotoX)
                {
                    npc.velocity.X = npc.velocity.X + speed;
                    if (Vector2.Distance(target, npc.Center) >= maxDist / 2)
                    {
                        npc.velocity.X = npc.velocity.X + speed * 2;
                    }
                }
                else if (npc.velocity.X > gotoX)
                {
                    npc.velocity.X = npc.velocity.X - speed;
                    if (Vector2.Distance(target, npc.Center) >= maxDist / 2)
                    {
                        npc.velocity.X = npc.velocity.X - speed * 2;
                    }
                }
                if (npc.velocity.Y < gotoY)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    if (Vector2.Distance(target, npc.Center) >= maxDist / 2)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                    }
                    if (npc.velocity.Y < 0f && gotoY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        return;
                    }
                }
                else if (npc.velocity.Y > gotoY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    if (Vector2.Distance(target, npc.Center) >= maxDist / 2)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                    }
                    if (npc.velocity.Y > 0f && gotoY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        return;
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
