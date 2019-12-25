using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Ancients
{
    public class TheFundamentals : ModItem
    {
        public bool supercharged = false;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 620;
            item.knockBack = 18;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 75, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.shoot = mod.ProjectileType("FundementalStrike");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Fundamentals");
            Tooltip.SetDefault("Hold the weapon without damage for 10 seconds to deal a strike that does 8x damage");
        }
        public override void HoldStyle(Player player)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            modPlayer.noDamageCounter++;
            if (modPlayer.noDamageCounter == 600)
            {
                int numDusts = 50;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                    Vector2 velocity = position - player.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 63, velocity.X * 2f, velocity.Y * 2f, 100, default, 1.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                }
            }
            if (modPlayer.noDamageCounter > 600)
            {
                if (Main.rand.Next(6) == 0)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 63)];
                    dust.position -= player.velocity / 6f;
                    dust.noGravity = true;
                    dust.scale = 1.5f;
                    dust.velocity *= 1.8f;
                }
            }
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (modPlayer.noDamageCounter > 600) mult = 8;
            else mult = 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
