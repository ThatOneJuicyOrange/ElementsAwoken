using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.OreStaffs
{
    public class CobaltStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 35;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 16;
            item.useAnimation = 16;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 4;
            item.mana = 10;
            item.UseSound = SoundID.Item42;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("CobaltScythe");
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Scythe");
            Tooltip.SetDefault("Fires a cobalt scythe");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
