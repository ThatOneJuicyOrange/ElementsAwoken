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
using ElementsAwoken.ScreenEffects;

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
            if (!MyWorld.awakenedMode)
            {
                sanity = sanityMax;
            }
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>(mod);

            sanityMax = sanityIncreaser;
            craftWeaponCooldown--;
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
                    if (player.ZoneSkyHeight)
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
                        }
                    }
                    if (Vector2.Distance(closestPos, player.Center) < distance && CheckValidSanityTile(closest))
                    {
                        int amount = (int)Math.Round(MathHelper.Lerp(3, 1, Vector2.Distance(closestPos, player.Center) / distance));
                        sanityRegen += amount;
                        string type = "";
                        if (closest.type == TileID.Campfire)
                        {
                            type = "Campfire";
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
                    player.magicDamage *= 0.9f;
                    player.meleeDamage *= 0.9f;
                    player.minionDamage *= 0.9f;
                    player.rangedDamage *= 0.9f;
                    player.thrownDamage *= 0.9f;
                }
                if (sanity < sanityMax * 0.1f)
                {
                    ElementsAwoken.screenshakeAmount = 2f;
                    if (sanity != 0)
                    {
                        player.magicDamage *= 0.75f;
                        player.meleeDamage *= 0.75f;
                        player.minionDamage *= 0.75f;
                        player.rangedDamage *= 0.75f;
                        player.thrownDamage *= 0.75f;
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
                if (sanity == 0)
                {
                    player.magicDamage *= 0.5f;
                    player.meleeDamage *= 0.5f;
                    player.minionDamage *= 0.5f;
                    player.rangedDamage *= 0.5f;
                    player.thrownDamage *= 0.5f;
                }
                // sanity regen logic
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

        // remove sanity on killing stuff 
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (MyWorld.awakenedMode)
            {
                if (target.life <= 0)
                {
                    if (target.damage == 0)
                    {
                        Main.NewText("reduced sanity");
                        sanity -= 3;
                    }
                    if (target.townNPC)
                    {
                        Main.NewText("reduced sanity");
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
                    if (target.damage == 0)
                    {
                        Main.NewText("reduced sanity");
                        sanity -= 3;
                    }
                    if (target.townNPC)
                    {
                        Main.NewText("reduced sanity");
                        sanity -= 15;
                    }
                }
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (MyWorld.awakenedMode)
            {
                sanity -= 2;
            }
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (MyWorld.awakenedMode)
            {
                sanity -= 2;
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
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>(mod);

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
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>(mod);
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
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            if (MyWorld.awakenedMode)
            {
                // lavawet isnt set yet so check it manually
                reduceSanityCD--; // takes 3 frames for vanilla to destroy the voodoo doll (idk why) so add a cooldown so it doesnt reduce it too much
                if (item.type == ItemID.GuideVoodooDoll && Collision.LavaCollision(item.position, item.width, item.height) && Main.netMode != 1 && NPC.AnyNPCs(NPCID.Guide) && reduceSanityCD <= 0)
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
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>(mod);
            if (MyWorld.awakenedMode)
            {
                if (item.buffType != 0 && item.useStyle == 2 && item.consumable && item.type != mod.ItemType("SanityRegenerationPotion"))
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
            return base.UseItem(item, player);
        }
        public override void OnCraft(Item item, Recipe recipe)
        {
            Player player = Main.player[item.owner];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
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