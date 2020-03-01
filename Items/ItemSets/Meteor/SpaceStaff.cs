using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Meteor
{
    public class SpaceStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 14;
            item.mana = 9;
            item.knockBack = 5;

            item.useTime = 35;
            item.useAnimation = 35;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = false;

            item.useStyle = 5;

            item.value = Item.sellPrice(0, 0, 40, 0);
            item.rare = 1;

            item.UseSound = SoundID.Item42;
            item.shoot = mod.ProjectileType("MeteoricBolt");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Staff");
            Tooltip.SetDefault("Fires a short burst of meteoric bolts");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2,4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(18));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.armor[0].type == ItemID.MeteorHelmet && player.armor[1].type == ItemID.MeteorSuit && player.armor[2].type == ItemID.MeteorLeggings) mult = 0;
        }
    }
}
