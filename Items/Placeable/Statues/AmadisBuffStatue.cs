using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Statues
{
    public class AmadisBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ArmorStatue);
            item.createTile = mod.TileType("AmadisBuff");
			item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statue of the Accursed");
            Tooltip.SetDefault("Odd, you feel your lifeforce lessened, but your melee and movement abilities enhanced.\n15% increased melee damage and speed\n30% reduced health");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("StatueBuffBurst")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffGenihWat")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffRanipla")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOinite")) == -1)
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
            recipe.AddIngredient(null, "AmadisStatue", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
