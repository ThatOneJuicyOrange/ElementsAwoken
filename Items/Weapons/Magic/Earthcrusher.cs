using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class Earthcrusher : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;

            item.damage = 39;
            item.mana = 18;
            item.knockBack = 5;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 5;
            item.UseSound = SoundID.Item43;

            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("EarthcrusherProj");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthcrusher");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, mod.ProjectileType("EarthcrusherProj"), damage, knockBack, player.whoAmI, 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ElementsAwoken:MythrilBar", 15);
            recipe.AddIngredient(ItemID.SoulofMight, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
