using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Statues
{
    public class RaniplaBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ArmorStatue);
            item.createTile = mod.TileType("RaniplaBuff");
			item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arctic Roamer Statue");
            Tooltip.SetDefault("Furries don't recieve enough appreciation and I'd like it to stay that way.\n25% increased speed\n15% increased summon damage\n25% reduced jump height and wingtime");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("StatueBuffBurst")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffGenihWat")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffAmadis")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOinite")) == -1)
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
            recipe.AddIngredient(null, "RaniplaStatue", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
