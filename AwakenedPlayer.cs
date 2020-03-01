using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;
using System.Linq;
using Terraria.ModLoader.IO;
using ReLogic.Graphics;
using Terraria.Graphics.Effects;
using ElementsAwoken.NPCs;
using ElementsAwoken.Effects;

namespace ElementsAwoken
{
    public class AwakenedPlayer : ModPlayer
    {
        public bool openSanityBook = false;

        public int sanity = 30;
        public int sanityMax = 150;
        public int sanityIncreaser = 150;

        public int sanityRegen = 0;
        public int sanityRegenCount = 0;
        public int sanityRegenTime = 0;

        public int craftWeaponCooldown = 0;

        public int sanityArrow = 0;
        public int sanityArrowFrame = 0;

        public int sanityGlitch = 0;
        public int sanityGlitchCooldown = 0;
        public int sanityGlitchFrame = 0;

        public List<int> sanityDrains = new List<int>();
        public List<string> sanityDrainsName = new List<string>();
        public List<int> sanityRegens = new List<int>();
        public List<string> sanityRegensName = new List<string>();

        public int bossIncreaseSanityCD = 0;

        public int mineTileCooldown = 0;
        public int mineTileCooldownMax = 3600 * 3;
        public int miningCounter = 0;

        public int nurseCooldown = 0;

        public int aleCD = 0;
        public override void ResetEffects()
        {
            sanityIncreaser = 150;

            sanityRegen = 0;

            sanityDrains = new List<int>();
            sanityDrainsName = new List<string>();
            sanityRegens = new List<int>();
            sanityRegensName = new List<string>();
        }
        public override void PostUpdateMiscEffects()
        {
            nurseCooldown--;
            if (!MyWorld.awakenedMode)
            {
                sanity = sanityMax;
            }
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            sanityMax = sanityIncreaser;
            craftWeaponCooldown--;
            aleCD--;
            // decreases
            if (MyWorld.awakenedMode)
            {
                // if (sanity > 0)
                {
                    // low life
                    if (player.statLife < player.statLifeMax2 * 0.25f)
                    {
                        int sanityRegenLoss = (int)Math.Round(MathHelper.Lerp(4, 1, player.statLife / (player.statLifeMax2 * 0.25f)));
                        //ElementsAwoken.DebugModeText(sanityRegenLoss);
                        sanityRegen -= sanityRegenLoss;

                        AddSanityDrain(sanityRegenLoss, "Low Health");
                    }
                    // in the dark
                    if (playerUtils.playerLight < 0.2)
                    {
                        int sanityRegenLoss = (int)Math.Round(MathHelper.Lerp(3, 1, playerUtils.playerLight / 0.2f));
                        //ElementsAwoken.DebugModeText(playerUtils.playerLight);
                        sanityRegen -= sanityRegenLoss;

                        AddSanityDrain(sanityRegenLoss, "Darkness");
                    }
                    // events
                    if (Main.bloodMoon)
                    {
                        sanityRegen--;
                        AddSanityDrain(1, "Blood Moon");
                    }
                    if (MyWorld.darkMoon)
                    {
                        sanityRegen -= 2;
                        AddSanityDrain(2, "Dark Moon");
                    }
                    if (MyWorld.voidInvasionUp && Main.time >= 16220 && !Main.dayTime)
                    {
                        sanityRegen -= 3;
                        AddSanityDrain(2, "Dawn of the Void");
                    }
                    if (player.ZoneUnderworldHeight)
                    {
                        sanityRegen -= 2;
                        AddSanityDrain(2, "In Hell");
                    }
                    if (player.ZoneSkyHeight && !modPlayer.cosmicalusArmor)
                    {
                        sanityRegen -= 1;
                        AddSanityDrain(1, "In Space");
                    }
                    if (miningCounter > 3600 * 10)
                    {
                        if (mineTileCooldown > mineTileCooldownMax - 300)
                        {
                            sanityRegen -= 3;// first 5 seconds after mining a tile reduces sanity
                            AddSanityDrain(3, "Mining For Too Long");
                        }
                    }
                    if (NPC.AnyNPCs(NPCID.MoonLordCore))
                    {
                        player.AddBuff(mod.BuffType("EldritchHorror"), 2);
                    }
                }

                // increases
                //if (sanity < sanityMax)
                {
                    for (int i = 0; i < 22; i++)
                    {
                        if (Main.vanityPet[player.buffType[i]])
                        {
                            sanityRegen++;
                            AddSanityRegen(1, "Pet");
                            //if (Main.time % 100 == 0) ElementsAwoken.DebugModeText("has pet");
                            break;
                        }
                    }
                    #region flowers and campfires nearby
                    int distance = 15 * 16;
                    Point topLeft = ((player.position - new Vector2(distance, distance)) / 16).ToPoint();
                    Point bottomRight = ((player.BottomRight + new Vector2(distance, distance)) / 16).ToPoint();

                    Tile closest = null;
                    Vector2 closestPos = new Vector2();
                    Tile closestVoidite = null;
                    Vector2 voiditePos = new Vector2();
                    for (int i = topLeft.X; i <= bottomRight.X; i++)
                    {
                        for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                        {
                            Tile t = Framing.GetTileSafely(i, j);
                            if (CheckValidSanityTile(t))
                            {
                                Vector2 tileCenter = new Vector2(i * 16, j * 16);
                                if (closest != null)
                                {
                                    if (Vector2.Distance(tileCenter, player.Center) < Vector2.Distance(closestPos, player.Center))
                                    {
                                        closest = t;
                                        closestPos = new Vector2(i * 16, j * 16);
                                    }
                                }
                                else
                                {
                                    closest = t;
                                    closestPos = new Vector2(i * 16, j * 16);
                                }

                            }
                            if (t.type == mod.TileType("Voidite"))
                            {
                                Vector2 tileCenter = new Vector2(i * 16, j * 16);
                                if (closestVoidite != null)
                                {
                                    if (Vector2.Distance(tileCenter, player.Center) < Vector2.Distance(voiditePos, player.Center))
                                    {
                                        closestVoidite = t;
                                        voiditePos = new Vector2(i * 16, j * 16);
                                    }
                                }
                                else
                                {
                                    closestVoidite = t;
                                    voiditePos = new Vector2(i * 16, j * 16);
                                }
                            }
                        }
                    }
                    if (Vector2.Distance(closestPos, player.Center) < distance && CheckValidSanityTile(closest))
                    {
                        int amount = (int)Math.Round(MathHelper.Lerp(3, 1, Vector2.Distance(closestPos, player.Center) / distance));
                        sanityRegen += amount;
                        string type = "Nice Object";
                        if (closest.type == TileID.Campfire)
                        {
                            type = "Campfire";
                        }
                        if (closest.type == TileID.Fireplace)
                        {
                            type = "Fireplace";
                        }
                        if (closest.type == TileID.FireflyinaBottle)
                        {
                            type = "Firefly in a Bottle";
                        }
                        if (closest.type == TileID.Sunflower)
                        {
                            type = "Sunflower";
                        }
                        if (closest.type == TileID.PlanterBox)
                        {
                            type = "Planter Box";
                        }
                        AddSanityRegen(amount, "Nearby " + type);
                    }
                    if (Vector2.Distance(voiditePos, player.Center) < distance && closestVoidite != null)
                    {
                        int amount = (int)Math.Round(MathHelper.Lerp(5, 1, Vector2.Distance(voiditePos, player.Center) / distance));
                        sanityRegen -= amount;
                        AddSanityDrain(amount, "Voidite");
                    }
                    #endregion

                    int townSanityRegen = 0;
                    int numNPCs = CountNearbyTownNPCs();
                    if (numNPCs > 5) townSanityRegen++;
                    if (numNPCs > 10) townSanityRegen++;
                    if (numNPCs > 15) townSanityRegen++;
                    if (numNPCs > 20) townSanityRegen++;
                    if (numNPCs > 25) townSanityRegen++;
                    if (townSanityRegen > 0)
                    {
                        sanityRegen += townSanityRegen;
                        AddSanityRegen(townSanityRegen, "In a Town");
                    }

                    if (miningCounter < 3600 * 10)
                    {
                        if (mineTileCooldown > mineTileCooldownMax - 300)
                        {
                            sanityRegen += 3; // first 5 seconds after mining a tile gives sanity
                            AddSanityRegen(3, "Mining");
                        }
                    }
                }

                if (sanity < sanityMax * 0.25f && sanity > sanityMax * 0.1f)
                {
                    player.allDamage *= 0.9f;
                }
                if (sanity < sanityMax * 0.1f)
                {
                    modPlayer.screenshakeAmount = 2f;
                    if (sanity != 0)
                    {
                        player.allDamage *= 0.75f;
                    }
                    else
                    {
                        player.allDamage *= 0.5f;
                    }
                    if (Main.rand.Next(1200) == 0)
                    {
                        int choice = Main.rand.Next(2);
                        if (choice == 0)
                        {
                            player.AddBuff(BuffID.Darkness, 600);
                        }
                        else if (choice == 1)
                        {
                            player.AddBuff(BuffID.Confused, 120);
                        }
                    }
                }

                // sanity regen logic
                if (!MyWorld.credits)
                {
                    sanityRegenCount = Math.Abs(sanityRegen);
                    sanityRegenTime -= sanityRegenCount;
                    if (sanityRegenTime <= 0)
                    {
                        sanityRegenTime = 450;
                        sanity += Math.Sign(sanityRegen);
                    }
                    if (sanity > sanityMax)
                    {
                        sanity = sanityMax;
                    }
                    if (sanity < 0)
                    {
                        sanity = 0;
                    }
                }
                if (mineTileCooldown > 0)
                {
                    mineTileCooldown--;
                    miningCounter++;
                }
                if (mineTileCooldown <= 0)
                {
                    miningCounter = 0;
                }
                InsanityOverlay.Update();

                #region glitch anim
                sanityGlitchCooldown--;
                if (sanityGlitchCooldown <= 0)
                {
                    sanityGlitchCooldown = 120;
                }
                if (sanityGlitchCooldown <= 12)
                {
                    sanityGlitch--;
                    if (sanityGlitch <= 0)
                    {
                        sanityGlitchFrame++;
                        sanityGlitch = 3;
                    }
                }
                else
                {
                    sanityGlitchFrame = 0;
                }
                if (sanityGlitchFrame > 4)
                {
                    sanityGlitchFrame = 1;
                }
                #endregion
                #region arrow anim
                sanityArrow--;
                if (sanityArrow <= 0)
                {
                    sanityArrowFrame++;
                    sanityArrow = 5;
                }
                if (sanityArrowFrame > 12)
                {
                    sanityArrowFrame = 0;
                }
                #endregion
            }
        }
        private bool CheckValidSanityTile(Tile t)
        {
            if (t.type == TileID.Campfire ||
                t.type == TileID.Fireplace ||
                t.type == TileID.FireflyinaBottle ||
                t.type == TileID.Sunflower ||
                t.type == TileID.PlanterBox)
            {
                return true;
            }
            return false;
        }
        private int CountNearbyTownNPCs()
        {
            int num = 0;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.townNPC && Vector2.Distance(player.Center, nPC.Center) <= 2000)
                {
                    num++;
                }
            }
            return num;
        }
        public override void OnRespawn(Player player)
        {
            sanity = sanityMax / 2;
        }

        // remove sanity on killing stuff 
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (MyWorld.awakenedMode)
            {
                if (target.life <= 0)
                {
                    if (target.damage == 0 && NPCID.Sets.TownCritter[target.type])
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 3");
                        sanity -= 3;
                    }
                    if (target.townNPC)
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 15");
                        sanity -= 15;
                    }
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (MyWorld.awakenedMode)
            {
                if (target.life <= 0)
                {
                    if (target.damage == 0 && NPCID.Sets.TownCritter[target.type])
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 3");
                        sanity -= 3;
                    }
                    if (target.townNPC)
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 15");
                        sanity -= 15;
                    }
                }
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (MyWorld.awakenedMode && damage > 0)
            {
                    sanity -= 2;
            }
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (MyWorld.awakenedMode && damage > 0)
            {
                sanity -= 2;
            }
        }

        public override void UpdateDead()
        {
            nurseCooldown = 0;
        }

        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (MyWorld.awakenedMode)
            {
                health = (int)((player.statLifeMax2 * 0.75f) - player.statLife);
                if (player.statLife > player.statLifeMax2 * 0.75f)
                {
                    return false;
                }
                if (nurseCooldown > 0)
                {
                    int nurseCDSeconds = nurseCooldown / 60;
                    chatText = "Sorry, I'm still preparing my stuff. Come back in " + nurseCDSeconds + " seconds";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            nurseCooldown = 30 * 60;
        }
        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (MyWorld.awakenedMode)
            {
                int newPrice = (int)((player.statLifeMax2 * 0.75f) - player.statLife);
                for (int j = 0; j < 22; j++)
                {
                    int debuff = player.buffType[j];
                    if (Main.debuff[debuff] && player.buffTime[j] > 5 && debuff != 28 && debuff != 34 && debuff != 87 && debuff != 89 && debuff != 21 && debuff != 86 && debuff != 199)
                    {
                        newPrice += 1000;
                    }
                }
                newPrice = (int)(newPrice * GetNursePriceScale());
                price = newPrice;
            }
        }

        private float GetNursePriceScale()
        {
            float scale = 0.5f;
            if (NPC.downedSlimeKing) scale += 0.25f;
            if (NPC.downedBoss1) scale += 0.25f;
            if (MyWorld.downedWasteland) scale += 0.25f;
            if (NPC.downedBoss2) scale += 0.25f;
            if (NPC.downedBoss3) scale += 0.5f;
            if (MyWorld.downedInfernace) scale += 0.5f;
            if (Main.hardMode) scale += 4f;
            if (NPC.downedMechBossAny) scale += 2f;
            if (MyWorld.downedScourgeFighter) scale += 1f;
            if (MyWorld.downedRegaroth) scale += 2f;
            if (NPC.downedPlantBoss) scale += 2f;
            if (MyWorld.downedCelestial) scale += 1f;
            if (MyWorld.downedPermafrost) scale += 2f;
            if (MyWorld.downedObsidious) scale += 1f;
            if (NPC.downedFishron) scale += 2f;
            if (MyWorld.downedAqueous) scale += 2f;
            if (NPC.downedMoonlord) scale += 10f;
            if (MyWorld.downedGuardian) scale += 3f;
            if (MyWorld.downedVolcanox) scale += 3f;
            if (MyWorld.downedVoidLeviathan) scale += 3f;
            if (MyWorld.downedAzana || MyWorld.sparedAzana) scale += 3f;
            if (MyWorld.downedAncients) scale += 3f;
            return scale;
        }

        public override void PostSellItem(NPC vendor, Item[] shopInventory, Item item)
        {
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            if (MyWorld.awakenedMode)
            {
                if (playerUtils.salesLastMin < 10)
                {
                    sanity++;
                }
            }
        }
        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            if (MyWorld.awakenedMode)
            {
                if (playerUtils.buysLastMin < 10)
                {
                    sanity++;
                }
            }
        }
        public override TagCompound Save()
        {
            return new TagCompound {
                {"sanity", sanity},
            };
        }
        public override void Load(TagCompound tag)
        {
            sanity = tag.GetInt("sanity");
        }
        public override void clientClone(ModPlayer clientClone)
        {
            MyPlayer clone = clientClone as MyPlayer;
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)ElementsAwokenMessageType.AwakenedSync);
            packet.Write((byte)player.whoAmI);
            packet.Write(sanity);
            packet.Send(toWho, fromWho);
        }
        public void AddSanityDrain(int amount, string type)
        {
            sanityDrains.Add(amount);
            sanityDrainsName.Add(type);
        }
        public void AddSanityRegen(int amount, string type)
        {
            sanityRegens.Add(amount);
            sanityRegensName.Add(type);
        }
        /*private void QuickBuff()
        {
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            // only checks potions
            if (player.noItems)
            {
                return;
            }
            for (int i = 0; i < 58; i++)
            {
                if (playerUtils.CheckValidInvPotion(i))
                {
                    if (playerUtils.potionsConsumedLastMin > 5)
                    {
                        sanity -= 3;
                    }
                }
            }
        }*/
    }
    class SanityNPC : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();
            if (MyWorld.awakenedMode)
            {
                if (npc.boss)
                {
                    if (playerUtils.bossesKilledLastFiveMin < 3)
                    {
                        modPlayer.sanity += modPlayer.sanityMax / 5;
                    }
                    else
                    {
                        modPlayer.sanity -= 8;
                    }
                }
            }
        }
    }
    class ReduceSanityItem : GlobalItem
    {
        public int reduceSanityCD = 0;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ReduceSanityItem myClone = (ReduceSanityItem)base.Clone(item, itemClone);
            myClone.reduceSanityCD = reduceSanityCD;
            return myClone;
        }
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            Player player = Main.player[item.owner];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (MyWorld.awakenedMode)
            {
                // lavawet isnt set yet so check it manually
                reduceSanityCD--; // takes 3 frames for vanilla to destroy the voodoo doll (idk why) so add a cooldown so it doesnt reduce it too much
                if (item.type == ItemID.GuideVoodooDoll && Collision.LavaCollision(item.position, item.width, item.height) && Main.netMode != NetmodeID.MultiplayerClient && NPC.AnyNPCs(NPCID.Guide) && reduceSanityCD <= 0)
                {
                    reduceSanityCD = 5;
                    modPlayer.sanity -= 20;
                    ElementsAwoken.DebugModeText("voodoll drain");
                }
            }
        }
        public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
        {
            if (MyWorld.awakenedMode)
            {
                reforgePrice = (int)(reforgePrice * 1.5f);
            }
            return base.ReforgePrice(item, ref reforgePrice, ref canApplyDiscount);
        }
        // check using potions
        public override bool UseItem(Item item, Player player)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();
            if (MyWorld.awakenedMode)
            {
                if (item.buffType != 0 && item.useStyle == 2 && item.consumable && item.type != mod.ItemType("SanityRegenerationPotion") && item.type != ItemID.Ale)
                {
                    if (playerUtils.potionsConsumedLastMin > 5)
                    {
                        modPlayer.sanity -= 3;
                    }
                }
            }
            /*if (item.Name.Contains("Potion") && item.buffType != 0)
            {

            }*/
            if (modPlayer.aleCD <= 0 && item.type == ItemID.Ale)
            {
                modPlayer.sanity += 3;
                modPlayer.aleCD = 60 * 30;
            }
            return base.UseItem(item, player);
        }
        public override void OnCraft(Item item, Recipe recipe)
        {
            Player player = Main.player[item.owner];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (MyWorld.awakenedMode)
            {
                if (item.damage > 0 && modPlayer.craftWeaponCooldown <= 0)
                {
                    modPlayer.sanity += 15;
                    modPlayer.craftWeaponCooldown = 3600;
                }
            }
        }
    }
}