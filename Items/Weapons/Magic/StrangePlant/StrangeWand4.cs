using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.StrangePlant
{
    public class StrangeWand4 : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 18;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 36;
            item.useAnimation = 36;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("StrangePlantBall4");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Wand");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StrangePlant4, 1);
            recipe.AddIngredient(null, "Stardust", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
