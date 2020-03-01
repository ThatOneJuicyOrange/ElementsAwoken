using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DrakoniteEruption : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 30;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.ignoreWater = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.alpha = 0;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Eruption");
        }
        public override void AI()
        {
            projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }

            if (ProjectileUtils.CountProjectiles(projectile.type,projectile.owner) > 3 && ProjectileUtils.HasLeastTimeleft(projectile.whoAmI)) projectile.Kill();

            Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.BottomLeft.X, projectile.BottomLeft.Y), projectile.width, 2, DustID.Fire)];
            dust.scale *= 1.2f;
            dust.velocity = new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-4, -2.5f));
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}