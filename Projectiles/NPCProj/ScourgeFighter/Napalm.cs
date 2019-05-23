using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ScourgeFighter
{
    public class Napalm : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 16;
            projectile.aiStyle = 14;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 360;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Napalm");
        }
        public override void AI()
        {
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
            projectile.rotation = -projectile.velocity.X * 0.05f;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}