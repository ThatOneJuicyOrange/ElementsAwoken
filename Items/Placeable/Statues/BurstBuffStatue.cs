using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Statues
{
    public class BurstBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ArmorStatue);
            item.createTile = mod.TileType("BurstBuff");
			item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statue of the Story Spinner");
            Tooltip.SetDefault("I am not really creative with tooltips, y'know. I do not even know if I exist.\nMaybe I am even just an illusion? Oh my god, my whole life was a lie!\n15% increased ranged damage\n15% decreased movement speed");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("StatueBuffAmadis")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffGenihWat")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffRanipla")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOinite")) == -1)
            {
                return true;
            }
            else
            {
                Main.NewText("You cant stack buff statues!", Color.Red, false);
                return false;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(null, "BurstStatue", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
