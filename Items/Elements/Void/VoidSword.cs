using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    public class VoidSword : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.TerraBlade);
            item.damage = 200;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("VoidSwordProj");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of the Depths");
            Tooltip.SetDefault("The edge of the blade gleams with red light");
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.immune[item.owner] = 5;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num6 = Main.rand.Next(2, 3);
            for (int index = 0; index < num6; ++index)
            {
                float num7 = speedX;
                float num8 = speedY;
                float SpeedX = speedX + (float)Main.rand.Next(-40, 41) * 0.05f;
                float SpeedY = speedY + (float)Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.5), knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}
