using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class GustStrike : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 25;
            item.knockBack = 10f;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 1, 50, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("GustStrikeCloud");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gust Strike");
            Tooltip.SetDefault("Harness the wind\nShoots out a blast of cloud and occasionally electrocutes enemies\nThe player moves faster when striking the enemy with the blade");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            player.AddBuff(mod.BuffType("WindsBlessing"), 600);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 6);
            recipe.AddIngredient(ItemID.Obsidian, 18);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
