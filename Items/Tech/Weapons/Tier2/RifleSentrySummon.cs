using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier2
{
    public class RifleSentrySummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 15;
            item.knockBack = 2f;
            item.GetGlobalItem<ItemEnergy>().energy = 8;

            item.noMelee = true;
            item.summon = true;
            item.sentry = true;

            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 2;
            
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("RifleSentry");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rifle Sentry Remote");
            Tooltip.SetDefault("Summons a rifle sentry to defend you\nThe rifle sentry uses 1 energy every two seconds");
        }
        public override bool CanUseItem(Player player)
        {
            // if mouse on tile 
            Point mousePoint = Main.MouseWorld.ToTileCoordinates();
            if (Main.tile[mousePoint.X, mousePoint.Y].nactive() && Main.tileSolid[(int)Main.tile[mousePoint.X, mousePoint.Y].type] && !Main.tileSolidTop[(int)Main.tile[mousePoint.X, mousePoint.Y].type] && Main.tile[mousePoint.X, mousePoint.Y].type != TileID.Rope)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num154 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, type, damage, knockBack, Main.myPlayer, 0f, 0f);
            player.UpdateMaxTurrets();
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ElementsAwoken:EvilBar", 2);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}