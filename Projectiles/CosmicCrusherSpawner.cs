using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CosmicCrusherSpawner : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.timeLeft = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center;

            projectile.localAI[1]++;
            if (projectile.localAI[1] % 6 == 0)
            {
                float shootSpeed = 9f;
                Vector2 targetPos = new Vector2(projectile.ai[0], projectile.ai[1]);

                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 shootVel = targetPos - projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= shootSpeed;

                    Vector2 perturbedSpeed = new Vector2(shootVel.X, shootVel.Y).RotatedByRandom(MathHelper.ToRadians(20));

                    int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CosmicCrusherBlade"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    projectile.netUpdate = true;
                }
            }
        }      
    }
}