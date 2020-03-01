using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ScourgeFighter
{
    public class Napalm : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 22;
            projectile.aiStyle = 14;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 360;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Napalm");
            Main.projFrames[projectile.type] = 6;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.4f, 0.8f);

            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.velocity.X = projectile.velocity.X * -0.1f;
            }
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.velocity.X = projectile.velocity.X * -0.25f;
            }
            if (projectile.velocity.Y != projectile.velocity.Y && projectile.velocity.Y > 1f)
            {
                projectile.velocity.Y = projectile.velocity.Y * -0.25f;
            }

            if (Main.rand.NextBool(10))
            {
                int num202 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                Dust var_2_88D1_cp_0_cp_0 = Main.dust[num202];
                var_2_88D1_cp_0_cp_0.position.X = var_2_88D1_cp_0_cp_0.position.X - 2f;
                Dust var_2_88EE_cp_0_cp_0 = Main.dust[num202];
                var_2_88EE_cp_0_cp_0.position.Y = var_2_88EE_cp_0_cp_0.position.Y + 2f;
                Dust dust = Main.dust[num202];
                dust.scale += (float)Main.rand.Next(50) * 0.01f;
                Main.dust[num202].noGravity = true;
                Dust var_2_8945_cp_0_cp_0 = Main.dust[num202];
                var_2_8945_cp_0_cp_0.velocity.Y = var_2_8945_cp_0_cp_0.velocity.Y - 2f;
                if (Main.rand.Next(2) == 0)
                {
                    int num203 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                    Dust var_2_89A6_cp_0_cp_0 = Main.dust[num203];
                    var_2_89A6_cp_0_cp_0.position.X = var_2_89A6_cp_0_cp_0.position.X - 2f;
                    Dust var_2_89C3_cp_0_cp_0 = Main.dust[num203];
                    var_2_89C3_cp_0_cp_0.position.Y = var_2_89C3_cp_0_cp_0.position.Y + 2f;
                    dust = Main.dust[num203];
                    dust.scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
                    Main.dust[num203].noGravity = true;
                    dust = Main.dust[num203];
                    dust.velocity *= 0.1f;
                }
                if ((double)projectile.velocity.Y < 0.25 && (double)projectile.velocity.Y > 0.15)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.8f;
                }
            }
            projectile.rotation = -projectile.velocity.X * 0.05f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 5)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}