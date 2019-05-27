using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.NPCs
{
    public class NPCsGLOBAL : GlobalNPC
    {
        public bool iceBound = false;
        public bool extinctionCurse = false;
        public bool handsOfDespair = false;
        public bool endlessTears = false;
        public bool ancientDecay = false;
        public bool soulInferno = false;
        public bool dragonfire = false;
        public bool discordDebuff = false;
        public bool chaosBurn = false;
        public bool electrified = false;
        // public bool soulRip = false;
        //public int soulRipTimer = 0;
        //public float soulRipAlpha = 0.3f;

        public bool impishCurse = false;

        public bool glowing = false;

        public bool lifeDrain = false;
        public int lifeDrainAmount = 1;

        public bool hasHands = false;

        public bool delete = false;

        public bool shrinking = false;
        public bool shrunk = false;
        public bool growing = false;
        public bool grown = false;
        public bool storedScale = false;
        public float initialScale = 1f;
        public override void ResetEffects(NPC npc)
        {
            iceBound = false;
            extinctionCurse = false;
            handsOfDespair = false;
            endlessTears = false;
            ancientDecay = false;
            soulInferno = false;
            dragonfire = false;
            discordDebuff = false;
            chaosBurn = false;
            electrified = false;

            impishCurse = false;

            glowing = false;

            lifeDrain = false;
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void AI(NPC npc)
        {
            bool immune = false;
            foreach (int k in ElementsAwoken.instakillImmune)
            {
                if (npc.type == k)
                {
                    immune = true;
                }
            }
            if (!immune && npc.active && npc.damage > 0 && !npc.dontTakeDamage && !npc.boss && npc.lifeMax < 1000)
            {
                if (shrinking)
                {
                    if (!storedScale)
                    {
                        initialScale = npc.scale;
                        if (grown)
                        {
                            initialScale = npc.scale / 1.5f;
                        }
                        storedScale = true;
                    }
                    if (npc.scale > initialScale * 0.75)
                    {
                        npc.scale *= 0.99f;
                    }
                    else
                    {
                        shrinking = false;
                    }
                    shrunk = true;
                    grown = false;
                }
                if (growing)
                {
                    if (!storedScale)
                    {
                        initialScale = npc.scale;
                        if (shrunk)
                        {
                            initialScale = npc.scale / 0.75f;
                        }
                        storedScale = true;
                    }
                    if (npc.scale < initialScale * 1.5)
                    {
                        npc.scale *= 1.01f;
                    }
                    else
                    {
                        growing = false;
                    }
                    grown = true;
                    shrunk = false;
                }
                if (!shrinking && !growing)
                {
                    storedScale = false;
                }
                if (shrinking && growing)
                {
                    npc.scale = 1f;
                    shrinking = false;
                    growing = false;
                    shrunk = false;
                    grown = false;
                    // make dust in an expanding circle
                    int numDusts = 36;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.Normalize(new Vector2(5, 5)) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                        Vector2 velocity = position - npc.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, 131, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                    }
                }
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.FindBuffIndex(mod.BuffType("CalamityPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 30f);
                maxSpawns = (int)(maxSpawns * 30f);
            }
            if (player.FindBuffIndex(mod.BuffType("ChaosPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 20f);
                maxSpawns = (int)(maxSpawns * 20f);
            }
            if (player.FindBuffIndex(mod.BuffType("HavocPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 15f);
                maxSpawns = (int)(maxSpawns * 15f);
            }
            if (MyWorld.aggressiveEnemies)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }

        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            NPC bossCheck = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].boss && Main.npc[i].active)
                {
                    bossCheck = Main.npc[i];
                    break;
                }
            }
            if (bossCheck.boss && bossCheck.active)
            {
                pool.Clear();
            }
        }

        private bool AnyBoss()
        {
            bool anyboss = false;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].boss && Main.npc[i].active)
                {
                    anyboss = true;
                    break;
                }
            }
            return anyboss;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            // poisoned: -4
            // on fire: -8
            // frostburn: -16
            // shadowflame: -30
            if (iceBound)
            {
                npc.velocity.Y = 0f;
                npc.velocity.X = 0f;
            }
            if (endlessTears)
            {
                npc.velocity *= 0.8f;
            }

            if (dragonfire)
            {
                npc.lifeRegen -= 40;
                if (damage < 2)
                {
                    damage = 2;
                }
            }
            if (electrified)
            {
                npc.lifeRegen -= 40;
                if (damage < 4)
                {
                    damage = 4;
                }
            }
            if (ancientDecay)
            {
                npc.lifeRegen -= 50;
                if (damage < 3)
                {
                    damage = 3;
                }
            }
            if (soulInferno)
            {
                npc.lifeRegen -= 75;
                if (damage < 3)
                {
                    damage = 3;
                }
            }
            if (handsOfDespair)
            {
                npc.lifeRegen -= 75;
                if (damage < 10)
                {
                    damage = 10;
                }
                if (!hasHands && !npc.boss)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("HandsOfDespair"), 0, 0f, 0, 1f, npc.whoAmI);
                    hasHands = true;
                }
            }
            else
            {
                hasHands = false;
            }
            if (extinctionCurse)
            {
                npc.lifeRegen -= 100;
                if (damage < 12)
                {
                    damage = 12;
                }
            }
            if (discordDebuff)
            {
                npc.lifeRegen -= 300;
                if (damage < 26)
                {
                    damage = 26;
                }
            }
            if (chaosBurn)
            {
                npc.lifeRegen -= 300;
                if (damage < 26)
                {
                    damage = 26;
                }
            }
            if (lifeDrain && !npc.SpawnedFromStatue)
            {
                npc.lifeRegen -= lifeDrainAmount;
                if (damage < lifeDrainAmount / 2)
                {
                    damage = lifeDrainAmount / 2;
                }
            }
            if (delete)
            {
                npc.active = false;
                if (npc.active)
                {
                    npc.StrikeNPC(npc.life, 0f, 0, false, false, false);
                }
                npc.netUpdate = true;
            }
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            modPlayer.buffDPS += Math.Abs(npc.lifeRegen / 2);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (ancientDecay)
            {
                drawColor = Color.LightYellow;
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("AncientDust"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.25f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.35f;
                    }
                }
                Lighting.AddLight(npc.position, 0.025f, 0f, 0f);
            }
            if (glowing)
            {
                Lighting.AddLight(npc.position, 0.5f, 0.4f, 0.6f);
            }
            if (MyWorld.swearingEnemies && npc.damage > 0)
            {
                if (Main.rand.Next(600) == 0)
                {
                    string s = "";
                    for (int l = 0; l < 4; l++)
                    {
                        int choice = Main.rand.Next(7);
                        if (choice == 0)
                        {
                            s = s + "$";
                        }
                        if (choice == 1)
                        {
                            s = s + "#";
                        }
                        if (choice == 2)
                        {
                            s = s + "!";
                        }
                        if (choice == 3)
                        {
                            s = s + "$";
                        }
                        if (choice == 4)
                        {
                            s = s + "@";
                        }
                        if (choice == 5)
                        {
                            s = s + "*";
                        }
                        if (choice == 6)
                        {
                            s = s + "?";
                        }
                    }
                    CombatText.NewText(npc.getRect(), Color.Red, s, false, false);
                }
            }
            
            // seeing the npc id
            //Main.spriteBatch.DrawString(Main.fontMouseText, "whoAmI: " + npc.whoAmI, new Vector2(npc.Center.X - 30 - Main.screenPosition.X, npc.Top.Y - 20 - Main.screenPosition.Y), Color.White);
        }

        /*public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (soulRip && soulRipTimer <= 30 && !npc.boss)
            {
                    SpriteEffects effects = npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);

                    Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, -2);
                    drawPos.Y -= soulRipTimer;
                    Color color = new Color (109, 20, 106, soulRipAlpha);
                    Main.spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                    Main.NewText(soulRipTimer);
            }
            return true;
        }
        public override void AI(NPC npc)
        {
            if (!soulRip)
            {
                soulRipTimer = 0;
                soulRipAlpha = 0.3f;
            }
            else
            {
                soulRipTimer++;

                soulRipAlpha += 0.03f;
            }
        }*/

        public override void SetDefaults(NPC npc)
        {
            // all vanilla bosses
            #region buff immunity
            if (npc.type == NPCID.EyeofCthulhu ||
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail ||
                npc.type == NPCID.BrainofCthulhu ||
                npc.type == NPCID.Creeper ||
                npc.type == NPCID.SkeletronHead ||
                npc.type == NPCID.SkeletronHand ||
                npc.type == NPCID.QueenBee ||
                npc.type == NPCID.KingSlime ||
                npc.type == NPCID.WallofFlesh ||
                npc.type == NPCID.WallofFleshEye ||
                npc.type == NPCID.TheDestroyer ||
                npc.type == NPCID.TheDestroyerBody ||
                npc.type == NPCID.TheDestroyerTail ||
                npc.type == NPCID.Retinazer ||
                npc.type == NPCID.Spazmatism ||
                npc.type == NPCID.SkeletronPrime ||
                npc.type == NPCID.PrimeCannon ||
                npc.type == NPCID.PrimeSaw ||
                npc.type == NPCID.PrimeVice ||
                npc.type == NPCID.PrimeLaser ||
                npc.type == NPCID.Plantera ||
                npc.type == NPCID.PlanterasTentacle ||
                npc.type == NPCID.Golem ||
                npc.type == NPCID.GolemHead ||
                npc.type == NPCID.GolemFistLeft ||
                npc.type == NPCID.GolemFistRight ||
                npc.type == NPCID.DukeFishron ||
                npc.type == NPCID.CultistBoss ||
                npc.type == NPCID.MoonLordHead ||
                npc.type == NPCID.MoonLordHand ||
                npc.type == NPCID.MoonLordCore)
            {
                npc.buffImmune[mod.BuffType("IceBound")] = true;
                npc.buffImmune[mod.BuffType("EndlessTears")] = true;
                npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            }
            // later bosses
            if (npc.type == NPCID.DukeFishron ||
                npc.type == NPCID.CultistBoss ||
                npc.type == NPCID.MoonLordHead ||
                npc.type == NPCID.MoonLordHand ||
                npc.type == NPCID.MoonLordCore)
            {
                npc.buffImmune[mod.BuffType("AncientDecay")] = true;
                npc.buffImmune[mod.BuffType("SoulInferno")] = true;
                npc.buffImmune[mod.BuffType("DragonFire")] = true;
            }
            #endregion

            if (MyWorld.aggressiveEnemies)
            {
                npc.damage = (int)(npc.damage * 1.25f);
            }
            if (npc.SpawnedFromStatue)
            {
                npc.buffImmune[mod.BuffType("LifeDrain")] = true;
            }
            /*// infinity gauntlet deletion blacklist
            if (npc.type == NPCID.EyeofCthulhu ||
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail ||
                npc.type == NPCID.BrainofCthulhu ||
                npc.type == NPCID.Creeper ||
                npc.type == NPCID.SkeletronHead ||
                npc.type == NPCID.SkeletronHand ||
                npc.type == NPCID.QueenBee ||
                npc.type == NPCID.KingSlime ||
                npc.type == NPCID.WallofFlesh ||
                npc.type == NPCID.WallofFleshEye ||
                npc.type == NPCID.TheDestroyer ||
                npc.type == NPCID.TheDestroyerBody ||
                npc.type == NPCID.TheDestroyerTail ||
                npc.type == NPCID.Retinazer ||
                npc.type == NPCID.Spazmatism ||
                npc.type == NPCID.SkeletronPrime ||
                npc.type == NPCID.PrimeCannon ||
                npc.type == NPCID.PrimeSaw ||
                npc.type == NPCID.PrimeVice ||
                npc.type == NPCID.PrimeLaser ||
                npc.type == NPCID.Plantera ||
                npc.type == NPCID.PlanterasTentacle ||
                npc.type == NPCID.Golem ||
                npc.type == NPCID.GolemHead ||
                npc.type == NPCID.GolemFistLeft ||
                npc.type == NPCID.GolemFistRight ||
                npc.type == NPCID.GolemHeadFree ||
                npc.type == NPCID.DukeFishron ||
                npc.type == NPCID.CultistBoss ||
                npc.type == NPCID.MoonLordHead ||
                npc.type == NPCID.MoonLordHand ||
                npc.type == NPCID.MoonLordCore ||
                npc.type == NPCID.MoonLordFreeEye ||
                npc.type == NPCID.DungeonGuardian ||
                npc.type == NPCID.IceGolem ||
                npc.type == NPCID.WyvernHead ||
                npc.type == NPCID.WyvernLegs ||
                npc.type == NPCID.WyvernTail ||
                npc.type == NPCID.WyvernBody ||
                npc.type == NPCID.WyvernBody2 ||
                npc.type == NPCID.WyvernBody3 ||
                npc.type == NPCID.Mothron ||
                npc.type == NPCID.PlanterasHook ||
                npc.type == NPCID.PlanterasTentacle ||
                npc.type == NPCID.Paladin ||
                npc.type == NPCID.HeadlessHorseman ||
                npc.type == NPCID.MourningWood ||
                npc.type == NPCID.Pumpking ||
                npc.type == NPCID.PumpkingBlade ||
                npc.type == NPCID.Yeti ||
                npc.type == NPCID.Everscream ||
                npc.type == NPCID.IceQueen ||
                npc.type == NPCID.Krampus ||
                npc.type == NPCID.MartianSaucer ||
                npc.type == NPCID.MartianSaucerCannon ||
                npc.type == NPCID.MartianSaucerCore ||
                npc.type == NPCID.MartianSaucerTurret ||
                npc.type == NPCID.MoonLordCore ||
                npc.type == NPCID.LunarTowerVortex ||
                npc.type == NPCID.LunarTowerStardust ||
                npc.type == NPCID.LunarTowerSolar ||
                npc.type == NPCID.LunarTowerNebula ||
                npc.type == NPCID.CultistArcherBlue ||
                npc.type == NPCID.CultistArcherWhite ||
                npc.type == NPCID.CultistDevote ||
                npc.type == NPCID.CultistDragonBody1 ||
                npc.type == NPCID.CultistDragonBody2 ||
                npc.type == NPCID.CultistDragonBody3 ||
                npc.type == NPCID.CultistDragonBody4 ||
                npc.type == NPCID.CultistDragonHead ||
                npc.type == NPCID.CultistDragonTail ||
                npc.type == NPCID.CultistTablet ||
                npc.type == NPCID.GoblinSummoner ||
                npc.type == NPCID.BigMimicJungle ||
                npc.type == NPCID.BigMimicCorruption ||
                npc.type == NPCID.BigMimicHallow ||
                npc.type == NPCID.BigMimicCrimson ||
                npc.type == NPCID.PirateShip ||
                npc.type == NPCID.PirateShipCannon ||
                npc.type == NPCID.SandElemental ||
                npc.type == NPCID.DD2Betsy ||
                npc.type == NPCID.DD2DarkMageT1 ||
                npc.type == NPCID.DD2DarkMageT3 ||
                npc.type == NPCID.SolarCrawltipedeBody ||
                npc.type == NPCID.SolarCrawltipedeHead ||
                npc.type == NPCID.DD2OgreT2 ||
                npc.type == NPCID.DD2OgreT3 ||
                npc.type == NPCID.RainbowSlime ||
                npc.type == NPCID.PirateCaptain ||
                npc.type == mod.NPCType("CosmicObserver") ||
                npc.type == mod.NPCType("ToySlime") ||
                npc.type == mod.NPCType("AncientWyrmArms") ||
                npc.type == mod.NPCType("AncientWyrmBody") ||
                npc.type == mod.NPCType("AncientWyrmHead") ||
                npc.type == mod.NPCType("AncientWyrmTail") ||
                npc.type == mod.NPCType("AndromedaHead") ||
                npc.type == mod.NPCType("AndromedaBody") ||
                npc.type == mod.NPCType("AndromedaTail") ||
                npc.type == mod.NPCType("BarrenSoul") ||
                npc.type == mod.NPCType("Furosia") ||
                npc.type == mod.NPCType("ObsidiousHand") ||
                npc.type == mod.NPCType("RegarothHead") ||
                npc.type == mod.NPCType("RegarothBody") ||
                npc.type == mod.NPCType("RegarothTail") ||
                npc.type == mod.NPCType("ShadeWyrmHead") ||
                npc.type == mod.NPCType("ShadeWyrmBody") ||
                npc.type == mod.NPCType("ShadeWyrmTail") ||
                npc.type == mod.NPCType("SolarFragment") ||
                npc.type == mod.NPCType("SoulOfInfernace") ||
                npc.type == mod.NPCType("VoidLeviathanHead") ||
                npc.type == mod.NPCType("VoidLeviathanBody") ||
                npc.type == mod.NPCType("VoidLeviathanBodyWeak") ||
                npc.type == mod.NPCType("VoidLeviathanTail") ||
                npc.type == mod.NPCType("VolcanoxHook") ||
                npc.type == mod.NPCType("VolcanoxTentacle")
                )
            {
                npc.buffImmune[mod.BuffType("DeletionMark")] = true;
            }*/
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant && NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("ThrowableBook"));
                shop.item[nextSlot].shopCustomPrice = 80;
                nextSlot++;
            }
            if (type == NPCID.Dryad)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("AlchemistsTimer"));
                nextSlot++;
            }
            if (type == NPCID.Wizard)
            {
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("ThrowableDictionary"));
                    shop.item[nextSlot].shopCustomPrice = 800;
                    nextSlot++;
                }
                if (MyWorld.downedAncients)
                {
                    shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("CrystalAmalgamate"));
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 25, 0, 0);
                    nextSlot++;
                }
            }
            if (type == NPCID.Steampunker)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("SonicArm"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 25, 0, 0);
            }
            if (type == NPCID.Cyborg)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Computer"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Desk"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 50, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("OfficeChair"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 50, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("LabLightFunctional"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 50, 0);
                nextSlot++;
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            int storyteller = NPC.FindFirstNPC(mod.NPCType("Storyteller"));
            int alchemist = NPC.FindFirstNPC(mod.NPCType("Alchemist"));
            if (npc.type == NPCID.Guide && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = "Dont trust " + Main.npc[storyteller].GivenName + ", he isn't as helpful as you may think.";
                }
            }
            if (npc.type == NPCID.Guide && Main.rand.Next(10) == 0 && MyWorld.downedAncients)
            {
                //Main.npcChatText = "At last, the storyteller is gone!"; // crap text 
            }
            if (npc.type == NPCID.Nurse && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = "Does " + Main.npc[storyteller].GivenName + " ever age? Everyone seems to be getting older but he stays the same";
                }
            }
            if (npc.type == NPCID.Dryad && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = "How old is " + Main.npc[storyteller].GivenName + "? I seem to have memories of him from when I was a kid...";
                }
            }
            if (npc.type == NPCID.ArmsDealer && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = Main.npc[storyteller].GivenName + " keeps talking down my weapons! I'll teach him a lesson one day.";
                }
                if (Main.rand.Next(10) == 0)
                {
                    chat = "Dont buy " + Main.npc[storyteller].GivenName + "'s weapons, something feels off about them...";
                }
            }
            if (npc.type == NPCID.Truffle && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = Main.npc[storyteller].GivenName + " seems to be the only person in this town that doesn't want to eat me. Does he eat at all?";
                }
            }
            if (npc.type == NPCID.Cyborg)
            {
                if (Main.rand.Next(15) == 0)
                {
                    chat = "Some townsfolk have been finding strange abandoned labs underground... I think some have been staying overnight "; //reference to the bug where npcs will move into labs
                }
            }
        }
    }
}