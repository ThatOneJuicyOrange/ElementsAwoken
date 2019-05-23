using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Water
{
    public class TheUrchin : ModItem
    {
        public override void SetDefaults()
        {       
            item.damage = 60;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = 5;
            item.knockBack = 4.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 60;
            item.width = 60;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.shoot = mod.ProjectileType("TheUrchinP");
            item.shootSpeed = 10f;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Urchin");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TheUrchinSpike"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
