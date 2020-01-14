using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.GemLasers
{
    public class GemRay : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 5;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gem Ray");
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            for (int i = 0; i < 4; i++)
            {
                Vector2 vector33 = projectile.position;
                vector33 -= projectile.velocity * ((float)i * 0.25f);
                Dust dust = Main.dust[Dust.NewDust(vector33, 1, 1, GetDustID(), 0f, 0f, 0, default(Color), 0.75f)];
                dust.position = vector33;
                dust.scale = (float)Main.rand.Next(70, 110) * 0.013f;
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            return;
        }
        private int GetDustID()
        {
            switch (projectile.ai[0])
            {
                case 0:
                    return 62;
                case 1:
                    return 64;
                case 2:
                    return 59;
                case 3:
                    return 61;
                case 4:
                    return 64;
                case 5:
                    return 60;
                case 6:
                    return 63;
                default:
                    return 62;
            }
        }
    }
}