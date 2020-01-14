using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class SearingArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.scale = 1f;
            projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Searing Arrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void AI()
        { 
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f)];
                dust.noGravity = true;
            }
            Dust dust2 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1f)];
            dust2.color = new Color(25, 25, 25, 100);
            dust2.noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            int numberProjectiles = Main.rand.Next(0,3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-1.2f, 1.2f), Main.rand.NextFloat(-1.2f, 1.2f), mod.ProjectileType("ThickSmoke"), (int)(projectile.damage * 0.5f), 2f, projectile.owner, 0f, 0f);
            }
        }
    }
}