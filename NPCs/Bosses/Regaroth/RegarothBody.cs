using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Regaroth
{
    public class RegarothBody : ModNPC
    {
        public int projectileBaseDamage = 50;
        public override void SetDefaults()
        {
            npc.width = 52;
            npc.height = 88;

            npc.damage = 35;
            npc.defense = 35;
            npc.lifeMax = 100000;
            npc.knockBackResist = 0.0f;

            npc.scale = 1.1f;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regaroth");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void FindFrame(int frameHeight)
        {
            if (Main.npc[(int)npc.ai[3]].life < Main.npc[(int)npc.ai[3]].lifeMax / 2)
            {
                npc.frame.Y = frameHeight * 1;
            }
            else
            {
                npc.frame.Y = 0;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 45;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 60;
                npc.defense = 45;
            }
        }
        public override bool PreAI()
        {
            bool expertMode = Main.expertMode;
            Player P = Main.player[npc.target];
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead)
                npc.timeLeft = 50;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            // shoot code
            if (npc.localAI[0] == 0)
            {
                npc.localAI[0]++;
            }
            npc.ai[2]++;
            npc.ai[0]--;
            if (npc.ai[2] > 1150f)
            {
                npc.ai[2] = 0f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                float rotation = (float)Math.Atan2(npc.Center.Y - (P.position.Y + (P.height * 0.5f)), npc.Center.X - (P.position.X + (P.width * 0.5f)));
                if (npc.ai[2] >= 750)
                {
                    if (Main.rand.Next(250) == 0)
                    {
                        float Speed = 10f;
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                        int num54 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("RegarothBolt"), projectileBaseDamage, 0f, 0);
                    }
                }
                if (npc.ai[2] <= 750)
                {
                    if (Main.rand.Next(4000) == 0)
                    {
                        float Speed = 4f;
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                        int num54 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("RegarothPortal"), projectileBaseDamage, 0f, 0);
                    }
                }
                if (Main.rand.Next(1500) == 0)
                {
                    float Speed = 4f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                    int num54 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("RegarothBomb"), projectileBaseDamage + 20, 0f, 0);
                }
                if (npc.ai[0] <= 0)
                {
                    if (Main.rand.Next(20) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("RegarothMinionHead"));
                    }
                    npc.ai[0] = 500f;
                }
            }
            //gore
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RegarothBody"), 1.1f);
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 50;
                npc.height = 50;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                npc.velocity = Vector2.Zero;
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;

            }

            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;       //this make that the npc does not have a health bar
        }
    }
}
