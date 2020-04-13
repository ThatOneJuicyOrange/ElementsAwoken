using ElementsAwoken.Items.Tech.Accessories.Tier4;
using ElementsAwoken.Items.Tech.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Accessories.Tier7
{
    public class HyperDrive : ModItem
    {
        public bool hasShot = false;
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 10;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyper Drive");
            Tooltip.SetDefault("");
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ItemType<BoostDrive>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = "<Special Ability Unbound>";

            string baseTooltip = "Pressing the special ability key (" + hotkey + ") will greatly speed up the player for 5 seconds\nWARNING: travelling at such high speeds can be damaging\nActivating it will consume 150 energy";
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.text = baseTooltip;
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.boostDrive = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemType<BoostDrive>(), 1);
            recipe.AddIngredient(ItemType<HeatSink>(), 3);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
