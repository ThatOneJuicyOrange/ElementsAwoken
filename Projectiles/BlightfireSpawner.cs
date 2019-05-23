using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BlightfireSpawner : ModProjectile
    {
        public int outOfLava = 120;
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.penetrate = 1;
            projectile.timeLeft = 3600;
            projectile.alpha = 255;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blightfire");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;

            if (projectile.lavaWet)
            {
                projectile.velocity.Y -= 0.05f;
            }
            else
            {
                projectile.velocity *= Main.rand.NextFloat(0.9f, 0.95f);
                outOfLava--;
            }
            if (outOfLava <= 0)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Blightfire"));
        }
    }
}