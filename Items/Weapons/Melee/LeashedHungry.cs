using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class LeashedHungry : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 42;
            item.knockBack = 4;

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;
            item.channel = true;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("HungryFlailP");
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leashed Hungry");
            Tooltip.SetDefault("Heals the player on enemy hit");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(null, "DemonicFleshClump", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

