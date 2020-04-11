using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Events.RadiantRain.Enemies
{
    public class StarlightGlobule : ModNPC
    {
        private float timer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 26;

            npc.aiStyle = -1;
            npc.lifeMax = 7600;
            npc.damage = 100;
            npc.defense = 40;

            //animationType = NPCID.Skeleton;

            npc.noGravity = true;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.knockBackResist = 0.75f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Globule");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 15000;
            npc.damage = 150;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 20000;
                npc.damage = 200;
                npc.defense = 65;
            }
        }
        public override bool PreNPCLoot()
        {
            if (npc.scale < 1) return false;
            return base.PreNPCLoot();
        }
        public override void NPCLoot()
        {
            if (Main.rand.NextBool()) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Radia>());
            if (Main.rand.NextBool(30)) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<GlobuleCannon>());
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void AI()
        {
            npc.TargetClosest(false);
            Player P = Main.player[npc.target];
            if (npc.scale < 1)
            {
                npc.ai[0]++;
                if (npc.ai[0] > 420)
                {
                    for (int p = 1; p <= 3; p++)
                    {
                        float strength = p * 2f;
                        int numDusts = p * 10;
                        for (int i = 0; i < numDusts; i++)
                        {
                            Vector2 position = (Vector2.One * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                            Vector2 velocity = position - npc.Center;
                            int dust = Dust.NewDust(position + velocity, 0, 0, DustID.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].noLight = true;
                            Main.dust[dust].velocity = Vector2.Normalize(velocity) * strength;
                        }
                    }
                    npc.StrikeNPC(npc.lifeMax * 2, 0f, 0, false, false, false);
                    Main.PlaySound(SoundID.Item94, npc.position);
                    Projectile exp = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ProjectileType<ExplosionHostile>(), npc.damage / 2, 5f, Main.myPlayer, 0f, 0f)];
                    exp.width = 125;
                    exp.height = 125;
                    exp.Center = npc.Center;
                }
                if (npc.ai[0] > 300)
                {
                    npc.Center = new Vector2(npc.ai[1], npc.ai[2]) + Main.rand.NextVector2Square(-5, 5);
                    npc.velocity = Vector2.Zero;
                }
                else if (npc.ai[0] == 300)
                {
                    npc.ai[1] = npc.Center.X;
                    npc.ai[2] = npc.Center.Y;
                }
            }
            if (npc.ai[0] < 300)
            {
                npc.rotation = npc.velocity.X * 0.075f;

                float moveSpeed = 0.02f;
                Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                if ((toTarget.X > 0 && npc.velocity.X < 5) || (toTarget.X < 0 && npc.velocity.X > -5)) npc.velocity.X += toTarget.X * moveSpeed;
                if ((toTarget.Y > 0 && npc.velocity.Y < 5) || (toTarget.Y < 0 && npc.velocity.Y > -5)) npc.velocity.Y += toTarget.Y * moveSpeed;

                float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
                if ((toTarget.X > 0 && npc.velocity.X < 0) || (toTarget.X < 0 && npc.velocity.X > 0)) npc.velocity.X *= slowSpeed;
                if ((toTarget.Y > 0 && npc.velocity.Y < 0) || (toTarget.Y < 0 && npc.velocity.Y > 0)) npc.velocity.Y *= slowSpeed;



                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.75f;
                    if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                    {
                        npc.velocity.X = 2f;
                    }
                    if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                    {
                        npc.velocity.X = -2f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.oldVelocity.Y * -0.75f;
                    if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                    {
                        npc.velocity.Y = 1f;
                    }
                    if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                    {
                        npc.velocity.Y = -1f;
                    }
                }

                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (k != npc.whoAmI && other.type == npc.type && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                    {
                        const float pushAway = 0.05f;
                        if (npc.position.X < other.position.X) npc.velocity.X -= pushAway;
                        else npc.velocity.X += pushAway;
                        if (npc.position.Y < other.position.Y) npc.velocity.Y -= pushAway;
                        else npc.velocity.Y += pushAway;
                    }
                }
                FallThroughPlatforms();
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < 0 && npc.scale == 1f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int n = 0; n < 3; n++)
                {
                    NPC smol = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y,npc.type)];
                    smol.scale = 0.5f;
                    smol.lifeMax = (int)(smol.lifeMax * 0.5f);
                    smol.life = smol.lifeMax;
                }
            }
        }
        private void FallThroughPlatforms()
        {
            Player P = Main.player[npc.target];
            Vector2 platform = npc.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.type] && npc.Bottom.Y < P.Bottom.Y && platformTile.active()) npc.position.Y += 0.3f;
        }
    }
}
