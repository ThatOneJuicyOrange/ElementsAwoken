using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class DragonsBreath : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 16; 
            
            item.damage = 16;
            item.knockBack = 2f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = false;

            item.useTime = 10;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.UseSound = SoundID.Item34;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("DragonFire");
            item.shootSpeed = 5f;
            item.useAmmo = AmmoID.Gel;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Breath");
            Tooltip.SetDefault("50% chance to not consume ammo");
        }


        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .5f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonFire"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
