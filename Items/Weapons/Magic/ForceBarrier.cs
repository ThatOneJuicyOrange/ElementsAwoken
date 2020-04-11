using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class ForceBarrier : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;

            item.damage = 50;
            item.mana = 18;
            item.knockBack = 12f;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true; 
            item.autoReuse = true;
            item.useTurn = true;

            item.UseSound = SoundID.Item113;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.shoot = ProjectileType<Barrier>();
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force Barrier");
            Tooltip.SetDefault("Summons a magical barrier that pushes enemies away\nOnly 3 barriers can be active at once");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.UnicornHorn, 1);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
