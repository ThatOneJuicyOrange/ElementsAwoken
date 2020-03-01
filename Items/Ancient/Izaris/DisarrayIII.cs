using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Izaris
{
    public class DisarrayIII : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 70;
            item.mana = 10;
            item.knockBack = 2f;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("DisarrayEntity");
            item.shootSpeed = 10f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disarray III");
            Tooltip.SetDefault("Summons a crystalline entity to protect you\nEach crystalline entity takes up 2 minion slots");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, 2);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DisarrayII", 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
