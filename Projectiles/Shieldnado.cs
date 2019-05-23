using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Shieldnado : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.aiStyle = 3;
            aiType = 52;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield of The Desert");
            Main.projFrames[projectile.type] = 6;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 2, projectile.height + 2, 32, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            projectile.rotation = 0;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 2)
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