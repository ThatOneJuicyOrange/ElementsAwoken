using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BlightRang : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/BlightRing"; } }

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;

            projectile.friendly = true;
            projectile.thrown = true;
            projectile.tileCollide = false;

            projectile.extraUpdates = 3;
            projectile.penetrate = -1;
            projectile.aiStyle = 3;
            projectile.timeLeft = 1600;
            aiType = 52;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blight Ring");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 300);
            target.immune[projectile.owner] = 4;
        }
        public override void AI()
        {
            for (int l = 0; l < 3; l++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= Main.rand.NextFloat(0.5f, 0.9f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}