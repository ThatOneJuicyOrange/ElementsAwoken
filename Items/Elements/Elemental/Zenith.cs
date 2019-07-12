using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Zenith : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;

            item.damage = 150;
            item.knockBack = 5;
            item.mana = 18;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;

            Item.staff[item.type] = true;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);
      
            item.shoot = mod.ProjectileType("ZenithOrb1");
            item.shootSpeed = 24f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenith");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1 + Main.rand.Next(2); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (Main.rand.Next(4))
                {
                    case 0: type = mod.ProjectileType("ZenithOrb1"); break;
                    case 1: type = mod.ProjectileType("ZenithOrb2"); break;
                    case 2: type = mod.ProjectileType("ZenithOrb3"); break;
                    case 3: type = mod.ProjectileType("ZenithOrb4"); break;
                    default: break;
                }
                float rand = Main.rand.Next(2, 4);
                rand = rand * 4;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X / rand, perturbedSpeed.Y / rand, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(ItemID.MagnetSphere, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
