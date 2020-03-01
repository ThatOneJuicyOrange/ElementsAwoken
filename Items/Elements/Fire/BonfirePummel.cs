using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class BonfirePummel : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            
            item.damage = 22;
            item.knockBack = 2;
            item.mana = 16;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.UseSound = SoundID.Item42;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("FireBall");
            item.shootSpeed = 11f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bonfire Pummel");
            Tooltip.SetDefault("Fires fireball that explodes into sparks on impact");
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Fire);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
