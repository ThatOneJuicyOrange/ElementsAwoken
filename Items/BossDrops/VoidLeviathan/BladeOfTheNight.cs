using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class BladeOfTheNight : ModItem
    {
        public int swingNum = 1;
        public int hitNum = 1;

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 200;
            item.knockBack = 8;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = mod.ProjectileType("ExtinctionBall");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of the Night");
            Tooltip.SetDefault("Projectiles become more unstable the longer they are alive\nEvery 30th swing unleashes the spirit of The Void Leviathan to tear apart your enemies\nEvery 10th true melee hit deals 10x damage");
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (hitNum == 10) damage *= 10;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.immune[item.owner] = 3;
            hitNum++;
            if (hitNum > 10) hitNum = 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            swingNum++;
            if (swingNum > 30)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 113);

                int current = Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX * 0.75f, speedY * 0.75f, mod.ProjectileType("VoidLeviathanProjHead"), damage, 0f, Main.myPlayer);

                int previous = current;
                for (int k = 0; k < 12; k++)
                {
                    previous = current;
                    current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("VoidLeviathanProjBody"), damage, 0f, Main.myPlayer, previous);
                }
                previous = current;
                current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("VoidLeviathanProjTail"), damage, 0f, Main.myPlayer, previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;

                // make dust in an expanding circle
                int numDusts = 36;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 dustPos = (Vector2.One * new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                    Vector2 velocity = dustPos - player.Center;
                    Dust dust = Main.dust[Dust.NewDust(dustPos + velocity, 0, 0, DustID.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default, 1.4f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.velocity = Vector2.Normalize(velocity) * 3f;
                    dust.fadeIn = 1.3f;
                }

                swingNum = 1;
            }
            int numProj = 3;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
                Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("VoidSinewave"), damage, knockBack, player.whoAmI)];
                proj.localAI[0] = Main.rand.NextFloat();
                proj.melee = true;
                proj.ranged = false;
            }
            if (swingNum % 2 == 0)
            {
                int numProj2 = Main.rand.Next(1, 4);
                for (int i = 0; i < numProj2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * Main.rand.NextFloat(0.75f, 1f), perturbedSpeed.Y * Main.rand.NextFloat(0.75f, 1f), mod.ProjectileType("VoidOrb"), damage, knockBack, player.whoAmI);
                }
            }

            return false;
        }
    }
}
