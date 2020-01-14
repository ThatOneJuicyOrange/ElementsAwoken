using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Accessories
{
    public class TempleSpikeBoots : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 7;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lihzahrdian Explorer's Boots");
            Tooltip.SetDefault("Immunity to wooden spikes");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.templeSpikeBoots = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SpikeBoots", 1);
            recipe.AddIngredient(null, "SunFragment", 5);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 25);
            recipe.AddIngredient(ItemID.WoodenSpike, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                    if (slot != i && player.armor[i].type == mod.ItemType("SpikeBoots"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
