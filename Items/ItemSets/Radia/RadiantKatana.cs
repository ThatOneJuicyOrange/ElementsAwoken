using ElementsAwoken.Projectiles;
using ElementsAwoken.Tiles.Crafting;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class RadiantKatana : ModItem
    {
        private int timer = 0;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 430;
            item.knockBack = 6;

            item.melee = true;
            item.autoReuse = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<RadiantKatanaStar>();
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Katana");
            Tooltip.SetDefault("Right Click slash towards the cursor");
        }
        public override bool AltFunctionUse(Player player)
        {
            if (timer > 0) return false;
            return true;
        }
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
                Projectile slash  = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<RadiantKatanaSlash>(), damage, knockBack, player.whoAmI)];
                slash.spriteDirection = player.direction;
               timer = 90;
            }
            else
            {
                Main.PlaySound(SoundID.Item9,player.position);
                int numberProjectiles1 = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            timer--;
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
