using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.NPCs
{
    public class AwakenedModeNPC : GlobalNPC
    {
        public int elite = 0;
        public bool cantElite = false;
        public bool dontExtraScale = false;
        public bool hasAssignedElite = false;
        public override void ResetEffects(NPC npc)
        {
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        /*public bool CalamityModRevengeance
        {
            get { return CalamityMod.CalamityWorld.revenge; }
        }*/ // gimme the fckn windows.dll pls 
        public override void SetDefaults(NPC npc)
        {
            if (MyWorld.awakenedMode)
            {
                if (NPC.CountNPCS(npc.type) > 5 && !cantElite && !npc.friendly && npc.damage != 0 && !npc.townNPC)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        elite = Main.rand.Next(1, 5);
                    }
                }

            }
            
        }
        public override void PostAI(NPC npc)
        {
            string basename = "";
            if (npc.GivenName == "") basename = npc.TypeName;
            else basename = npc.GivenName;

            if (npc.realLife != -1)
            {
                elite = 0; // so worms cant
            }

            if (elite > 0 && !hasAssignedElite)
            {
                switch (elite)
                {
                    case 1:
                        npc.GivenName = "Blazing " + basename;
                        break;
                    case 2:
                        npc.GivenName = "Frozen " + basename;
                        npc.color = new Color(175, 247, 240);
                        break;
                    case 3:
                        npc.GivenName = "Electrifying " + basename;
                        break;
                    case 4:
                        npc.GivenName = "Shielded " + basename;
                        break;
                    case 5:
                        npc.GivenName = "Poisonous " + basename;
                        break;
                    default: break;
                }
                hasAssignedElite = true;
            }
        }
        public override void AI(NPC npc)
        {
            if (elite > 0)
            {
                switch (elite)
                {
                    case 1:
                        if (Main.rand.Next(2) == 0)
                        {
                            Dust fire = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * -0.5f, npc.velocity.X * -0.5f, 100, default(Color), 2f)];
                            fire.noGravity = true;
                            fire.scale = 1f + Main.rand.Next(10) * 0.1f;
                            fire.fadeIn = 2.5f;
                        }
                        break;
                    case 2:
                        if (Main.rand.Next(3) == 0)
                        { 
                            Dust frost = Main.dust[Dust.NewDust(npc.BottomLeft + npc.velocity, npc.width, 4, 135, npc.velocity.X * -0.5f, npc.velocity.X * -0.5f, 100, default(Color), 2f)];
                            frost.noGravity = true;
                            frost.scale = 1f + Main.rand.Next(10) * 0.1f;
                            frost.fadeIn = 1.5f;
                        }
                        break;
                    case 3:
                        Dust electricity = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 0.5f)];
                        electricity.velocity *= 1.6f;
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default: break;
                }
            }
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (elite == 4)
            {
                if (npc.life > npc.lifeMax / 2)
                {
                    damage = (int)(damage * 0.5f);
                }
            }
        }
        public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit)
        {
            if (elite == 4)
            {
                if (npc.life > npc.lifeMax / 2)
                {
                    damage = (int)(damage * 0.5f);
                }
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (elite == 4)
            {
                if (npc.life > npc.lifeMax / 2)
                {
                    damage = (int)(damage * 0.5f);
                }
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (elite > 0)
            {
                switch (elite)
                {
                    case 1:
                        target.AddBuff(BuffID.OnFire, 180, false);
                        break;
                    case 2:
                        target.AddBuff(BuffID.Frostburn, 180, false);
                        break;
                    case 3:
                        target.AddBuff(BuffID.Electrified, 180, false);
                        break;
                    case 4:
                        break;
                    case 5:
                        target.AddBuff(BuffID.Poisoned, 180, false);
                        target.AddBuff(BuffID.Venom, 180, false);
                        break;
                    default: break;
                }
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (elite == 4 && npc.life > npc.lifeMax / 2)
            {
                Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY);
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * 0.5f);
                Color color = new Color(66, 244, 226, 50);
                SpriteEffects effects = npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos + new Vector2(0, 4), npc.frame, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos + new Vector2(0, -4), npc.frame, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos + new Vector2(4, 0), npc.frame, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos + new Vector2(-4, 0), npc.frame, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, npc.GetAlpha(drawColor), npc.rotation, drawOrigin, npc.scale, effects, 0f);
            }
        }
        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            if (MyWorld.awakenedMode)
            {
                if (!npc.boss && !dontExtraScale)
                {
                    npc.lifeMax = (int)(npc.lifeMax * 1.75f);
                    npc.damage = (int)(npc.damage * 1.75f);
                    npc.defense = (int)(npc.defense * 1.5f);
                }
                if (ElementsAwoken.calamityEnabled)
                {
                    //if (!CalamityModRevengeance)
                    {
                        npc.value *= 1.5f;
                        ScaleBossStats(npc);
                    }
                }
                else
                {
                    npc.value *= 1.5f;
                    ScaleBossStats(npc);
                }
                if (elite > 0)
                {
                    npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                }
            }
        }
        private void ScaleBossStats(NPC npc)
        {
            if (npc.type == NPCID.KingSlime)
            {
                npc.lifeMax = 3600;
                npc.damage = 80;
                npc.defense = 15;
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npc.lifeMax = 4800;
                npc.damage = 45;
                npc.defense = 17;
            }
            if (npc.type == NPCID.EaterofWorldsHead)
            {
                npc.lifeMax = 120;
                npc.damage = 60;
                npc.defense = 6;
            }
            if (npc.type == NPCID.EaterofWorldsBody)
            {
                npc.lifeMax = 350;
                npc.damage = 25;
                npc.defense = 9;
            }
            if (npc.type == NPCID.EaterofWorldsTail)
            {
                npc.lifeMax = 450;
                npc.damage = 23;
                npc.defense = 15;
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npc.lifeMax = 2500;
                npc.damage = 60;
                npc.defense = 20;
            }
            if (npc.type == NPCID.QueenBee)
            {
                npc.lifeMax = 6500;
                npc.damage = 65;
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npc.lifeMax = 11000;
                npc.damage = 90;
            }
            if (npc.type == NPCID.SkeletronHand)
            {
                npc.lifeMax = 2000;
                npc.damage = 65;
                npc.defense = 20;
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npc.lifeMax = 15000;
                npc.damage = 200;
                npc.defense = 25;
            }
            if (npc.type == NPCID.WallofFleshEye)
            {
                npc.lifeMax = 15000;
                npc.damage = 200;
                npc.defense = 9;
            }
            if (npc.type == NPCID.Retinazer)
            {
                npc.lifeMax = 45000;
                npc.damage = 90;
                npc.defense = 15;
            }
            if (npc.type == NPCID.Spazmatism)
            {
                npc.lifeMax = 53500;
                npc.damage = 100;
                npc.defense = 15;
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                npc.lifeMax = 175000;
                npc.damage = 350;
                npc.defense = 5;
            }
            if (npc.type == NPCID.TheDestroyerBody)
            {
                npc.damage = 110;
                npc.defense = 40;
            }
            if (npc.type == NPCID.TheDestroyerTail)
            {
                npc.damage = 80;
                npc.defense = 45;
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                npc.lifeMax = 60000;
                npc.damage = 90;
            }
            if (npc.type == NPCID.PrimeCannon)
            {
                npc.lifeMax = 15000;
                npc.damage = 60;
                npc.defense = 30;
            }
            if (npc.type == NPCID.PrimeLaser)
            {
                npc.lifeMax = 13000;
                npc.damage = 60;
                npc.defense = 30;
            }
            if (npc.type == NPCID.PrimeSaw)
            {
                npc.lifeMax = 17500;
                npc.damage = 120;
                npc.defense = 45;
            }
            if (npc.type == NPCID.PrimeVice)
            {
                npc.lifeMax = 17500;
                npc.damage = 120;
                npc.defense = 42;
            }
            if (npc.type == NPCID.Plantera)
            {
                npc.lifeMax = 60000;
                npc.damage = 130;
                npc.defense = 24;
            }
            if (npc.type == NPCID.GolemHead)
            {
                npc.lifeMax = 35000;
                npc.damage = 130;
                npc.defense = 25;
            }
            if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight)
            {
                npc.lifeMax = 15000;
                npc.damage = 120;
                npc.defense = 35;
            }
            if (npc.type == NPCID.Golem)
            {
                npc.lifeMax = 20000;
                npc.damage = 140;
                npc.defense = 35;
            }
            if (npc.type == NPCID.GolemHeadFree)
            {
                npc.damage = 140;
            }
            if (npc.type == NPCID.DukeFishron)
            {
                npc.lifeMax = 75000;
                npc.damage = 170;
                npc.defense = 60;
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npc.lifeMax = 75000;
                npc.damage = 120;
                npc.defense = 60;
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                npc.lifeMax = 100000;
                npc.defense = 90;
            }
            if (npc.type == NPCID.MoonLordHand)
            {
                npc.lifeMax = 50000;
                npc.defense = 55;
            }
            if (npc.type == NPCID.MoonLordHead)
            {
                npc.lifeMax = 75000;
                npc.defense = 65;
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (MyWorld.awakenedMode)
            {
                if (!ElementsAwoken.calamityEnabled)
                {
                    spawnRate = (int)(spawnRate / 2f);
                    maxSpawns = (int)(maxSpawns * 2f);
                }
            }
        }
    }
}