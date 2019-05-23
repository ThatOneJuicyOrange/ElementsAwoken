using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Statues
{
    public class OiniteBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ArmorStatue);
            item.createTile = mod.TileType("OiniteBuff");
			item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Balancer Statue");
            Tooltip.SetDefault("Fun isn't something one considers when balancing the universe. But this... does put a smile on my face.\nDoubles all buff and debuff duration");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("StatueBuffBurst")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffGenihWat")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffAmadis")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffRanipla")) == -1)
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
            recipe.AddIngredient(null, "OiniteStatue", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
