using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class Hailgun : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Gatligator);
            item.damage = 20;
            item.ranged = true;
            item.width = 58;
            item.height = 32;
            item.useTime = 4;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item11;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.autoReuse = true;
            item.shoot = ProjectileID.SnowBallFriendly;
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Snowball;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hailgun");
            Tooltip.SetDefault("It's raining snowballs, from out of the sky\n90% chance not to consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num6 = Main.rand.Next(2, 3);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speedY + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(2))
                {
                    case 0: type = ProjectileID.SnowBallFriendly; break;
                    default: break;
                }
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .9f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.SnowballCannon);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
