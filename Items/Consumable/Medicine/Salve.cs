using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Medicine
{
    public class Salve : ModItem
    {
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.width = 20;
            item.height = 28;
            item.value = Item.buyPrice(0, 0, 50, 0);
            item.rare = 0;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Salve");
            Tooltip.SetDefault("Heal 50 health over 10 seconds");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("MedicineCooldown")) == -1)
            {
                return true;
            }
            return false;
        }
        public override bool UseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("MedicineCooldown")) == -1)
            {
                player.AddBuff(mod.BuffType("MedicineCooldown"), 1500);
                player.AddBuff(mod.BuffType("SalveBuff"), 600);
                return true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GrassSeeds, 1);
            recipe.AddIngredient(ItemID.VineRope, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
