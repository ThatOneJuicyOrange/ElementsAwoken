using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AstralTear : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 66;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 600;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Tear");
        }
        public override void AI()
        {
            projectile.velocity.X = 0f;
            projectile.velocity.Y = 0f;
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 173);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
            }

        }
    }
}