using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier3
{
    public class PlasmaThrower : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 33;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 6;

            item.useAnimation = 28;
            item.useTime = 28;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;

            item.shootSpeed = 8f;
            item.shoot = ProjectileType<PlasmaThrowerBeam>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Thrower");
            Tooltip.SetDefault("Instantly heats a path in front of it");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item73, position);
            Main.PlaySound(SoundID.Item38, position);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemType<GoldWire>(), 6);
            recipe.AddIngredient(ItemType<Capacitor>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
