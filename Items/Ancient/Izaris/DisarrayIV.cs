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
    public class DisarrayIV : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 190;
            item.mana = 10;
            item.knockBack = 2f;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.shoot = mod.ProjectileType("DisarrayEntity");
            item.shootSpeed = 10f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disarray IV");
            Tooltip.SetDefault("Summons a familliar crystalline entity to protect you\nEach crystalline entity takes up 2 minion slots");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, 3);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DisarrayIII", 1);
            recipe.AddIngredient(null, "AncientShard", 5);
            recipe.AddIngredient(null, "VoiditeBar", 4);
            recipe.AddIngredient(null, "DiscordantBar", 20);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
