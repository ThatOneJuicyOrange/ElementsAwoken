using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    public class TrueWaterBolt : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WaterBolt);
            item.damage = 100;
            item.shootSpeed *= 2f;
            item.shoot = ProjectileID.WaterBolt;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wavebolt");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num6 = 3;
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speedY + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(2))
                {
                    case 0: type = ProjectileID.WaterBolt; break;
                    default: break;
                }
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddIngredient(ItemID.WaterBolt, 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
