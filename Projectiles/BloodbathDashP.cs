using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BloodbathDashP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodbath Dash");
        }
        public override void AI()
        {
            projectile.velocity *= 0.97f;

            Dust dust = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, 5, projectile.velocity.X * 0.6f, projectile.velocity.Y * 0.6f, 130, default(Color), 2f)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;

            ProjectileGlobal.Home(projectile, 6f);
        }
    }
}