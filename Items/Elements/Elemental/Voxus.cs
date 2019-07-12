using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Voxus : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 92;
            item.height = 28;

            item.damage = 210;
            item.knockBack = 1.75f;


            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item91;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.shoot = mod.ProjectileType("VoxusP");
            item.shootSpeed = 26f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voxus");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(ItemID.StarCannon, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
