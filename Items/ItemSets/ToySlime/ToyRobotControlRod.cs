using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{  
    public class ToyRobotControlRod : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 34;

            item.damage = 21;
            item.mana = 10;
            item.knockBack = 3;

            item.summon = true;
            item.noMelee = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 3;

            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("ToyRobot");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Robot Control Rod");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
