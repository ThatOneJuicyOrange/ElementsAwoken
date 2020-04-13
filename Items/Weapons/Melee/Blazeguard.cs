using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Blazeguard : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;
            
            item.damage = 290;
            item.knockBack = 5;

            item.useTime = 16;
            item.useAnimation = 8;
            item.useStyle = 1;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item1;

            item.shoot = ModContent.ProjectileType<Projectiles.BlazeguardWave>();
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazeguard");
            Tooltip.SetDefault("Fires two exploding fireballs");
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
