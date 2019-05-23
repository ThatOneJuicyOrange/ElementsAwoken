using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    public class DragonFang : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 60;
            item.width = 60;

            item.damage = 62;
            item.knockBack = 4.75f;

            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 7, 50, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("DragonFangP");
            item.shootSpeed = 11f;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fang");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonFangShadow"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
