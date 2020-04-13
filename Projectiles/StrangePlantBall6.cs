using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class StrangePlantBall6 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        int spawnProj = 10;
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 0;
            projectile.alpha = 255;
            projectile.timeLeft = 150;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.penetrate = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Orb");
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 63, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
                Main.dust[dust].velocity *= 0.1f;
                if (projectile.velocity == Vector2.Zero)
                {
                    Main.dust[dust].velocity.Y -= 1f;
                    Main.dust[dust].scale = 1.2f;
                }
                else
                {
                    Main.dust[dust].velocity += projectile.velocity * 0.2f;
                }
                Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
                Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
                Main.dust[dust].noGravity = true;
            }
            //projectile.velocity.Y += 0.05f;
            spawnProj--;
            if (spawnProj <= 0)
            {
                int type = 0;
                switch (Main.rand.Next(3))
                {
                    case 0: type = mod.ProjectileType("StrangePlantBall2"); break;
                    case 1: type = mod.ProjectileType("StrangePlantBall3"); break;
                    case 2: type = mod.ProjectileType("StrangePlantBall5"); break;
                }
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 6f, type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                spawnProj = 18 + Main.rand.Next(6);
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 62, damageType: "magic");
            int numberProjectiles = 1;
                for (int num252 = 0; num252 < numberProjectiles; num252++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    while (value15.X == 0f && value15.Y == 0f)
                    {
                        value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    }
                    value15.Normalize();
                    value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
                    int num1 = projectile.damage / 2;
                    Projectile.NewProjectile(projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, mod.ProjectileType("StrangePlantBall1"), num1, 0f, projectile.owner, 0f, 0f);
                }
            
        }
    }
}