using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class CursedScepter : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 28;
            item.mana = 9;

            item.useTime = 30;
            item.useAnimation = 30;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useStyle = 5;
            item.knockBack = 2;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.UseSound = SoundID.Item42;
            item.shoot = mod.ProjectileType("CursedBall");
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Scepter");
            Tooltip.SetDefault("Fires a cursed flame ball that explodes into sparks upon contact");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CursedFlame, 20);
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
