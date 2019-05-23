using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Items
{
    public class ItemsGlobal : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.RodofDiscord)
            {
                if (player.GetModPlayer<MyPlayer>(mod).cantROD == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.CellPhone)
            {
                if (player.GetModPlayer<MyPlayer>(mod).cantMagicMirror == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.MagicMirror)
            {
                if (player.GetModPlayer<MyPlayer>(mod).cantMagicMirror == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.IceMirror)
            {
                if (player.GetModPlayer<MyPlayer>(mod).cantMagicMirror == true)
                {
                    return false;
                }
            }
            if (item.type == ItemID.GrapplingHook)
            {
                if (player.GetModPlayer<MyPlayer>(mod).cantGrapple == true)
                {
                    player.cGrapple = 1;
                    return false;
                }
            }
            if (MyWorld.encounter1)
            {
                if (item.type == ItemID.MagicMirror || item.type == ItemID.RecallPotion || item.type == ItemID.WormholePotion || item.type == ItemID.CellPhone)
                {
                    return false;
                }
            }
            return true;
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag && Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ThrowerEmblem"));
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
        }
    }
}