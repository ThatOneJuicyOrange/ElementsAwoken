using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.OreStaffs
{
    public class AdamantiteStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 42;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 24;
            item.useAnimation = 24;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 4;
            item.mana = 10;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AdamantiteLaser");
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Beamer");
            Tooltip.SetDefault("Shoots an adamantite laser");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
