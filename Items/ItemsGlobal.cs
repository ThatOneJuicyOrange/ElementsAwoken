using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ElementsAwoken.Items.BossDrops.zVanilla.Awakened;
using ElementsAwoken.Items.Donator.Crow;
using ElementsAwoken.Items.Accessories.Emblems;

namespace ElementsAwoken.Items
{
    public class ItemsGlobal : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<MyPlayer>().voidBlood && (item.potion || item.type == ItemID.RegenerationPotion))
            {
                return false;
            }
            if (item.type == ItemID.RodofDiscord)
            {
                if (player.GetModPlayer<MyPlayer>().cantROD == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.CellPhone)
            {
                if (player.GetModPlayer<MyPlayer>().cantMagicMirror == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.MagicMirror)
            {
                if (player.GetModPlayer<MyPlayer>().cantMagicMirror == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.IceMirror)
            {
                if (player.GetModPlayer<MyPlayer>().cantMagicMirror == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.GrapplingHook)
            {
                if (player.GetModPlayer<MyPlayer>().cantGrapple == true)
                {
                    player.cGrapple = 1;
                    return false;
                }
            }
            if (ElementsAwoken.encounter == 1)
            {
                if (item.type == ItemID.MagicMirror || item.type == ItemID.IceMirror || item.type == ItemID.RecallPotion || item.type == ItemID.WormholePotion || item.type == ItemID.CellPhone)return false;
            }
            if (MyWorld.credits)
            {
                if (item.type != mod.ItemType("CreditsSetup")) return false;
            }
            if (player.GetModPlayer<MyPlayer>().meteoricPendant && item.magic)
            {
                Item startItem = new Item();
                startItem.SetDefaults(item.type);
                startItem.Prefix(item.prefix);
                item.useTime = (int)(startItem.useTime * 0.8f);
                item.useAnimation = (int)(startItem.useAnimation * 0.8f);
                startItem.TurnToAir();
            }
            if (!player.GetModPlayer<MyPlayer>().meteoricPendant && item.magic)
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
}