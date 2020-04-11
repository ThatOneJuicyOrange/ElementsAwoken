using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class FetidBlades : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.damage = 46;
            item.knockBack = 3f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
                
            item.useAnimation = 18;
            item.useStyle = 1;
            item.useTime = 18;

            item.UseSound = SoundID.Item39;
            item.maxStack = 1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.shoot = ProjectileType<FetidBladeP>();
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fetid Blades");
            Tooltip.SetDefault("Throws multiple sticking fetid blades\nRight click to make all blades explode into damaging goop");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == item.shoot && proj.alpha < 100 && proj.ai[1] != 0)
                    {
                        proj.Kill();
                        ProjectileUtils.Explosion(proj, new int[] { 46 }, proj.damage, "thrown",0.5f,0.75f);
                    }
                }
                return false;
            }
            else
            {
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(5);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 5f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
