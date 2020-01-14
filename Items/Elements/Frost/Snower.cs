using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class Snower : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.width = 42;
            item.height = 16;
            item.useTime = 4;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SnowSpray");
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Gel;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snower");
            Tooltip.SetDefault("Sprays cold white snow onto your enemies\n75% chance to not consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(4);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 1f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .75f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
