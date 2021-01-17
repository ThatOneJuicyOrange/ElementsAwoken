using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Accessories.Environmental
{
    public class GravitronBoots : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 2, 50);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gravitron Boots");
            Tooltip.SetDefault("Allows you to traverse normally during the lava lakes eruptions\nOnly turns on during eruptions and does not slow down the player\nAllows the player to fall much faster in liquids");
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ItemType<IronBoots>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (MyWorld.plateauWeather == 2 && modPlayer.zonePlateau) modPlayer.ironBoots = 2;
            if (player.wet) player.maxFallSpeed += 10;
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PinkThread, 15);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
