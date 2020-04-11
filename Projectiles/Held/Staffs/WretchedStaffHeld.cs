using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Held.Staffs
{
    public class WretchedStaffHeld : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wretched Staff");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            int chargeDur = 70;
            if (Main.myPlayer == projectile.owner)
            {
                if ((player.channel && player.HeldItem.mana > 0 && player.CheckMana(player.inventory[player.selectedItem].mana, false, false)) && !player.noItems && !player.CCed)
                {
                    projectile.ai[1] += 1f;
                }
                else
                {
                    Main.PlaySound(SoundID.Item43, projectile.position);
                    int numProj = (int)MathHelper.Lerp(1, 10, MathHelper.Clamp(projectile.ai[1] / chargeDur, 0, 1));
                    float rotation = (float)Math.Atan2(projectile.Center.Y -Main.MouseWorld.Y, projectile.Center.X - Main.MouseWorld.X);
                    for (int i = 0; i < numProj; i++)
                    {
                        float speed = Main.rand.NextFloat(12, 15);
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<PutridSpike>(), projectile.damage, 0f, 0);
                    }
                    projectile.Kill();
                }
                if (projectile.ai[1] <= chargeDur)
                {
                    int num = (int)MathHelper.Lerp(14, 1, MathHelper.Clamp(projectile.ai[1] / chargeDur, 0, 1));
                    float vel = MathHelper.Lerp(2, 6, MathHelper.Clamp(projectile.ai[1] / chargeDur, 0, 1));
                    if (projectile.ai[1] % num == 0)
                    {
                        ProjectileUtils.OutwardsCircleDust(projectile, 46, 36, vel, true, 150);
                    }

                    int soundDelay = (int)MathHelper.Lerp(12, 2, MathHelper.Clamp(projectile.ai[1] / chargeDur, 0, 1));
                    if (projectile.soundDelay <= 0)
                    {
                        projectile.soundDelay = 2 + soundDelay;
                        projectile.soundDelay *= 2;
                        Main.PlaySound(SoundID.Item15, projectile.position);
                    }
                }
                ProjectileUtils.HeldWandPos(projectile, player, MathHelper.ToRadians(25));
            }
        }
    }
}
 