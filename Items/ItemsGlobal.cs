using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ElementsAwoken.Items.BossDrops.zVanilla.Awakened;
using ElementsAwoken.Items.Donator.Crow;
using ElementsAwoken.Items.Accessories.Emblems;
using System;

namespace ElementsAwoken.Items
{
    public class ItemsGlobal : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public int miningRadius = 0;
        public override bool CanUseItem(Item item, Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.wispForm) return false;
            if (modPlayer.voidBlood && (item.potion || item.type == ItemID.RegenerationPotion)) return false;
            if (modPlayer.cantROD && item.type == ItemID.RodofDiscord) return false;
            if ((ElementsAwoken.encounter == 1 || modPlayer.cantMagicMirror) && 
                (item.type == ItemID.CellPhone ||
                item.type == ItemID.MagicMirror ||
                item.type == ItemID.IceMirror ||
                item.type == ItemID.RecallPotion ||
                item.type == ItemID.WormholePotion)) return false;
            if (MyWorld.credits && item.type != mod.ItemType("CreditsSetup"))return false;
            if (modPlayer.meteoricPendant && item.magic)
            {
                Item startItem = new Item();
                startItem.SetDefaults(item.type);
                startItem.Prefix(item.prefix);
                item.useTime = (int)(startItem.useTime * 0.8f);
                item.useAnimation = (int)(startItem.useAnimation * 0.8f);
                startItem.TurnToAir();
            }
            if (!modPlayer.meteoricPendant && item.magic)
            {
                Item startItem = new Item();
                startItem.SetDefaults(item.type);
                startItem.Prefix(item.prefix);
                item.useTime = startItem.useTime;
                item.useAnimation = startItem.useAnimation;
                startItem.TurnToAir();
            }
            return base.CanUseItem(item, player);
        }
        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i)
                    {
                        if (item.type == ItemID.SorcererEmblem && player.armor[i].type == mod.ItemType("NebulaEmblem")) return false;
                        if (item.type == ItemID.WarriorEmblem && player.armor[i].type == mod.ItemType("SolarEmblem")) return false;
                        if (item.type == ItemID.RangerEmblem && player.armor[i].type == mod.ItemType("VortexEmblem")) return false;
                        if (item.type == ItemID.SummonerEmblem && player.armor[i].type == mod.ItemType("StardustEmblem")) return false;
                    }
                }
            }
            return base.CanEquipAccessory(item, player, slot);
        }
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag")
            {
                if (arg == ItemID.WallOfFleshBossBag && Main.rand.Next(4) == 0) player.QuickSpawnItem(ModContent.ItemType<ThrowerEmblem>());
                if (arg == ItemID.PlanteraBossBag && Main.rand.Next(6) == 0) player.QuickSpawnItem(ModContent.ItemType<CrematedChaos>());
                // awakened drops
                if (MyWorld.awakenedMode)
                {
                    if (arg == ItemID.KingSlimeBossBag) player.QuickSpawnItem(ModContent.ItemType<SlimeBooster>());
                    if (arg == ItemID.EyeOfCthulhuBossBag) player.QuickSpawnItem(ModContent.ItemType<GreatLens>());
                    if (arg == ItemID.EaterOfWorldsBossBag) player.QuickSpawnItem(ModContent.ItemType<CorruptedFang>());
                    if (arg == ItemID.BrainOfCthulhuBossBag) player.QuickSpawnItem(ModContent.ItemType<BleedingHeart>());
                    if (arg == ItemID.QueenBeeBossBag) player.QuickSpawnItem(ModContent.ItemType<CrystalNectar>());
                    if (arg == ItemID.SkeletronBossBag) player.QuickSpawnItem(ModContent.ItemType<FadedCloth>());
                    if (arg == ItemID.WallOfFleshBossBag) player.QuickSpawnItem(ModContent.ItemType<HellishFleshHeart>());
                }
            }
            else if (context == "crate")
            {
                if (arg == ItemID.FloatingIslandFishingCrate && Main.rand.NextBool(3)) player.QuickSpawnItem(ItemID.SkyMill);
            }
        }
        public override void RightClick(Item item, Player player)
        {
        }
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.JungleSpores)
            {
                item.ammo = ItemID.JungleSpores;
                item.shoot = mod.ProjectileType("BioLightning");
                item.consumable = true;
            }
            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (item.type == ItemID.SpaceGun)
                {
                    item.useTime = 19;
                    item.useAnimation = 19;
                    item.shootSpeed = 8f;
                }
                if (item.type == ItemID.LastPrism)
                {
                    item.damage = 90;
                }
            }
        }
    }
    public class AOEPick : ModPlayer
    {
        public override void PostItemCheck()
        {
            if (!player.HeldItem.IsAir)
            {
                Item item = player.HeldItem;
                bool flag18 = player.position.X / 16f - (float)Player.tileRangeX - (float)item.tileBoost <= (float)Player.tileTargetX && (player.position.X + (float)player.width) / 16f + (float)Player.tileRangeX + (float)item.tileBoost - 1f >= (float)Player.tileTargetX && player.position.Y / 16f - (float)Player.tileRangeY - (float)item.tileBoost <= (float)Player.tileTargetY && (player.position.Y + (float)player.height) / 16f + (float)Player.tileRangeY + (float)item.tileBoost - 2f >= (float)Player.tileTargetY;
                if (player.noBuilding)
                {
                    flag18 = false;
                }
                if (flag18)
                {
                    if (item.GetGlobalItem<ItemsGlobal>().miningRadius > 0)
                    {
                        if (player.toolTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
                        {
                            if (item.pick > 0)
                            {
                                for (int i = -item.GetGlobalItem<ItemsGlobal>().miningRadius; i <= item.GetGlobalItem<ItemsGlobal>().miningRadius; i++)
                                {
                                    for (int j = -item.GetGlobalItem<ItemsGlobal>().miningRadius; j <= item.GetGlobalItem<ItemsGlobal>().miningRadius; j++)
                                    {
                                        if ((i != 0 || j != 0) && !Main.tileAxe[(int)Main.tile[Player.tileTargetX + i, Player.tileTargetY + j].type] && !Main.tileHammer[(int)Main.tile[Player.tileTargetX + i, Player.tileTargetY + j].type])
                                        {
                                            player.PickTile(Player.tileTargetX + i, Player.tileTargetY + j, item.pick);
                                        }
                                    }
                                }
                                player.itemTime = (int)((float)item.useTime * player.pickSpeed);
                            }                         
                                player.poundRelease = false;
                         
                        }
                        if (player.releaseUseItem)
                        {
                            player.poundRelease = true;
                        }                       
                    }
                }
            }
        }
    }
}