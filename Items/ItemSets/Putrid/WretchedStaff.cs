using ElementsAwoken.Projectiles.Held.Staffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class WretchedStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 84;
            item.knockBack = 3.75f;
            item.mana = 20;

            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item15;

            item.noMelee = true;
            item.autoReuse = false;
            item.magic = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.shoot = ProjectileType<WretchedStaffHeld>();
            item.shootSpeed = 35f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wretched Staff");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
