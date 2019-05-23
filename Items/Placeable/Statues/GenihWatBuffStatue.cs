using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Statues
{
    public class GenihWatBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ArmorStatue);
            item.createTile = mod.TileType("GenihWatBuff");
			item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulgar Bard Statue");
            Tooltip.SetDefault("'Yes, THIS is what I want to look like when you carve my statue to look like!\nNO, I don't CARE how hard it is to 'sprite' or whatever!!' - Da Gweat Genih Wat\n15% increased magic damage and increased Mana Regen by 5\nEnemies are more aggressive and spawn more frequently\nSwearing can be disabled by right clicking the statue");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("StatueBuffBurst")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffAmadis")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffRanipla")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOinite")) == -1)
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
            recipe.AddIngredient(null, "GenihWatStatue", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
