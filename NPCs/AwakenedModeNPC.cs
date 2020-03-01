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
                if (NPC.CountNPCS(npc.type) > 5 && !cantElite && !npc.friendly && npc.damage != 0 && !npc.townNPC && npc.realLife == -1)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        elite = Main.rand.Next(1, 6);
                    }
                    if (elite == 3)
                    {
                        if (!NPC.downedBoss1)
                        {
                            while (elite == 3)
                            {
                                elite = Main.rand.Next(1, 6);
                            }
                        }
                        else if (!Main.hardMode)
                        {
                            if (Main.rand.NextBool(3)) elite = Main.rand.Next(1, 6);
                        }
                    }
                }

            }
            
        }
        public override void PostAI(NPC npc)
        {
            string basename = "";
            if (npc.GivenName == "") basename = npc.TypeName;
            else basename = npc.GivenName;


            if (elite > 0 && !hasAssignedElite)
            {
                switch (elite)
                {
                    case 1:
                        npc.GivenName = "Blazing " + basename;
                        break;
                    case 2:
                        npc.GivenName = "Frozen " + basename;
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
                npc.netUpdate = true;
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
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (elite == 2) drawColor = new Color(175, 247, 240);
            if (elite == 5) drawColor = new Color(44, 199, 44);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
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
                //spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, npc.GetAlpha(drawColor), npc.rotation, drawOrigin, npc.scale, effects, 0f);
            }
            return base.PreDraw(npc, spriteBatch, drawColor);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {

        }
        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            if (MyWorld.awakenedMode)
            {
                if (!npc.boss && !dontExtraScale && !BossParts(npc))
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
        private bool BossParts(NPC npc)
        {
            if (npc.type == NPCID.TheDestroyer) return true;
            else if (npc.type == NPCID.TheDestroyerBody) return true;
            else if (npc.type == NPCID.SkeletronHand) return true;
            else if (npc.type == NPCID.TheDestroyerTail) return true;
            else if (npc.type == NPCID.EaterofWorldsHead) return true;
            else if (npc.type == NPCID.EaterofWorldsBody) return true;
            else if (npc.type == NPCID.EaterofWorldsTail) return true;
            else if (npc.type == NPCID.PrimeCannon) return true;
            else if (npc.type == NPCID.PrimeLaser) return true;
            else if (npc.type == NPCID.PrimeSaw) return true;
            else if (npc.type == NPCID.PrimeVice) return true;
            else if (npc.type == NPCID.GolemFistLeft) return true;
            else if (npc.type == NPCID.GolemFistRight) return true;
            else if (npc.type == NPCID.GolemHeadFree) return true;
            else if (npc.type == NPCID.GolemHead) return true;
            else if (npc.type == NPCID.MoonLordHand) return true;
            else if (npc.type == NPCID.MoonLordHead) return true;
            return false;
        }
        private void ScaleBossStats(NPC npc)
        {
            if (npc.type == NPCID.KingSlime)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.32f);
                npc.damage = (int)(npc.damage * 1.5f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.EaterofWorldsHead)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.EaterofWorldsBody)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.EaterofWorldsTail)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.4f);
            }
            if (npc.type == NPCID.QueenBee)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.35f);
                npc.damage = (int)(npc.damage * 1.20f);
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.25f);
                npc.damage = (int)(npc.damage * 1.3f);
            }
            if (npc.type == NPCID.SkeletronHand)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.5f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.33f);
                npc.defense = (int)(npc.defense * 1.4f);
            }
            if (npc.type == NPCID.WallofFleshEye)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.33f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.Retinazer)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.22f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.Spazmatism)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.3f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.25f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = 5;
            }
            if (npc.type == NPCID.TheDestroyerBody)
            {
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.TheDestroyerTail)
            {
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.2f);
                npc.damage = (int)(npc.damage * 1.15f);
            }
            if (npc.type == NPCID.PrimeCannon)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.3f);
            }
            if (npc.type == NPCID.PrimeLaser)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.3f);
            }
            if (npc.type == NPCID.PrimeSaw)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.2f);
            }
            if (npc.type == NPCID.PrimeVice)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.36f);
                npc.defense = (int)(npc.defense * 1.2f);
            }
            if (npc.type == NPCID.Plantera)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.42f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.GolemHead)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.45f);
                npc.damage = (int)(npc.damage * 1.3f);
                npc.defense = (int)(npc.defense * 1.25f);
            }
            if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5f);
                npc.damage = (int)(npc.damage * 1.25f);
                npc.defense = (int)(npc.defense * 1.25f);
            }
            if (npc.type == NPCID.Golem)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.48f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.34f);
            }
            if (npc.type == NPCID.GolemHeadFree)
            {
                npc.damage = (int)(npc.damage * 1.1f);
            }
            if (npc.type == NPCID.DukeFishron)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.25f);
                npc.damage = (int)(npc.damage * 1.2f);
                npc.defense = (int)(npc.defense * 1.2f);
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.8f);
                npc.damage = (int)(npc.damage * 1.6f);
                npc.defense = (int)(npc.defense * 1.5f);
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.33f);
                npc.defense = (int)(npc.defense * 1.3f);
            }
            if (npc.type == NPCID.MoonLordHand)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.33f);
                npc.defense = (int)(npc.defense * 1.375f);
            }
            if (npc.type == NPCID.MoonLordHead)
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.2f);
                npc.defense = (int)(npc.defense * 1.3f);
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (MyWorld.awakenedMode)
            {
                float spawnMult = 1.3f;
                if (NPC.downedBoss1 || WorldGen.shadowOrbSmashed || MyWorld.downedToySlime) spawnMult = 1.5f;
                if (NPC.downedBoss3) spawnMult = 2;
                spawnRate = (int)(spawnRate / spawnMult);
                maxSpawns = (int)(maxSpawns * spawnMult);

            }
        }
    }
}