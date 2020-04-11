using ElementsAwoken.Projectiles;
using ElementsAwoken.Tiles.Crafting;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.RadiantMaster
{
    public class RadiantSword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 470;
            item.knockBack = 6;

            item.melee = true;
            item.autoReuse = true;

            item.useTime = 30;
            item.useAnimation = 18;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<RadiantStorm>();
            item.shootSpeed = 4f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magenta Storm");
            Tooltip.SetDefault("Right Click to summon sentient blades around the player");
        }
        public override bool AltFunctionUse(Player player)
        {
            if (player.ownedProjectileCounts[ProjectileType<RadiantBlade>()] != 0) return false;
            return true;
        }
        public override bool OnlyShootOnSwing => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) item.noUseGraphic = true;
            else item.noUseGraphic = false;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(position.X, position.Y, Main.rand.NextFloat(-3,3), Main.rand.NextFloat(-3, 3), ProjectileType<RadiantBlade>(), damage, knockBack, player.whoAmI);
                }
            }
            else
            {
                Main.PlaySound(SoundID.Item9, player.position);
                int numberProjectiles1 = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, (int)(damage * 1.5f), knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PinkFlame);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
