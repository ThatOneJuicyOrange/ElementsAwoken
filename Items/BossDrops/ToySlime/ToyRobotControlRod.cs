using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{  
    public class ToyRobotControlRod : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 34;

            item.damage = 12;
            item.mana = 10;
            item.knockBack = 3;

            item.summon = true;
            item.noMelee = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 1;

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
            recipe.AddIngredient(null, "BrokenToys", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
