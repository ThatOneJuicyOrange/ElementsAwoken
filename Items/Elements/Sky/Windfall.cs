using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class Windfall : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.width = 42;
            item.height = 16;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WindfallFire");
            item.shootSpeed = 6f;
            item.useAmmo = AmmoID.Gel;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windfall");
            Tooltip.SetDefault("50% chance to not consume ammo");
        }

        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .75f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
