using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class SandSprayer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 16; 
            
            item.damage = 8;
            item.knockBack = 3.25f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item34;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("SandSpray");
            item.shootSpeed = 4.5f;
            item.useAmmo = AmmoID.Sand;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Sprayer");
            Tooltip.SetDefault("Sprays coarse sand at your enemies... Useful\n20% chance to not consume ammo");
        }


        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .8f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddIngredient(ItemID.Sandgun);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
