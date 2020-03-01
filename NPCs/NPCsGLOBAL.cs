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
        public bool acidBurn = false;
        public bool corroding = false;
        // public bool soulRip = false;
        //public int soulRipTimer = 0;
        //public float soulRipAlpha = 0.3f;

        public bool impishCurse = false;

        public int lifeDrainAmount = 0;

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
            acidBurn = false;
            corroding = false;

            impishCurse = false;
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public static void ImmuneAllEABuffs(NPC npc)
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("AncientDecay")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("DragonFire")] = true;
            npc.buffImmune[mod.BuffType("Discord")] = true;
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
        public static bool AnyBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC boss = Main.npc[i];
                if (boss.boss && boss.active)
                {
                    return true;
                }
            }
            return false;
        }
        public static void GoThroughPlatforms(NPC npc)
        {
            Vector2 platform = npc.Bottom / 16;
            if (TileID.Sets.Platforms[Framing.GetTileSafely((int)platform.X, (int)platform.Y).type]) npc.noTileCollide = true;
            else npc.noTileCollide = false;
            /*if (ModContent.GetInstance<Config>().debugMode)
            {
                Dust dust = Main.dust[Dust.NewDust(platform * 16, 16, 16, 135)];
                dust.noGravity = true;
            }*/
        }
        public static int ReducePierceDamage(int damage, Projectile projectile)
        {
            if (projectile.type == ProjectileID.LastPrismLaser && ModContent.GetInstance<Config>().vItemChangesDisabled) return (int)(damage * 0.1f);
            else if (projectile.type == ProjectileID.LastPrismLaser && !ModContent.GetInstance<Config>().vItemChangesDisabled) return (int)(damage * 0.85f);
            else if (projectile.maxPenetrate == -1 && ProjectileID.Sets.YoyosMaximumRange[projectile.type] == 0) return (int)(damage * 0.5f);
            else if (projectile.maxPenetrate > 10) return (int)(damage * 0.5f);
            else if (projectile.maxPenetrate > 6) return (int)(damage * 0.75f);
            else if (projectile.maxPenetrate > 3) return (int)(damage * 0.9f);
            else return damage;
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.FindBuffIndex(mod.BuffType("CalamityPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 12.5f);
                maxSpawns = (int)(maxSpawns * 12.5f);
            }
            else if (player.FindBuffIndex(mod.BuffType("ChaosPotionBuff")) != -1 || player.FindBuffIndex(mod.BuffType("CalamityBannerBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 7.5f);
                maxSpawns = (int)(maxSpawns * 7.5f);
            }
            else if (player.FindBuffIndex(mod.BuffType("ChaosBannerBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }
            else if(player.FindBuffIndex(mod.BuffType("HavocPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 4f);
                maxSpawns = (int)(maxSpawns * 4f);
            }
            else if (player.FindBuffIndex(mod.BuffType("HavocBannerBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 2f);
                maxSpawns = (int)(maxSpawns * 2f);
            }
            if (MyWorld.aggressiveEnemies)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }
            if (MyWorld.credits)
            {
                maxSpawns = 0;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (AnyBoss()) pool.Clear();
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

            if (acidBurn)
            {
                npc.lifeRegen -= 20;
                if (damage < 3)
                {
                    damage = 3;
                }
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
            if (corroding)
            {
                npc.lifeRegen -= 75;
                if (damage < 4) damage = 4;
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
            if (lifeDrainAmount > 0 && !npc.SpawnedFromStatue)
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
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
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
            if (acidBurn)
            {
                npc.color = new Color(178, 244, 124, 120);
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

            if (impishCurse)
            {
                if (!npc.ichor) drawColor = new Color(255, 80, 80);
                else drawColor = new Color(255, 180, 40);
                Lighting.AddLight(npc.Center, 0.6f, 0.2f, 0.3f);
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
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant && NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("ThrowableBook"));
                shop.item[nextSlot].shopCustomPrice = 80;
                nextSlot++;
            }
            if (type == NPCID.Dryad && Main.bloodMoon)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("DryadsRadar"));
                nextSlot++;
            }
            if (type == NPCID.Wizard)
            {
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Dictionary"));
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
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("FeatheredGoggles"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(2, 0, 0, 0);
                nextSlot++;
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
            /*if (npc.type == NPCID.Guide && Main.rand.Next(10) == 0 && MyWorld.downedAncients)
            {
                Main.npcChatText = "At last, the storyteller is gone!"; // crap text 
            }*/
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
                if (NPC.downedBoss1 && !MyWorld.downedWasteland)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 0: chat = "That scorpion, 'Wasteland' as you call it, why do we fear it?"; break;
                            case 1: chat = "I feel her speaking to me... she’s afraid... "; break;
                            case 2: chat = "All creatures deserve a chance to live. Live and be free..."; break;
                        }
                    }
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