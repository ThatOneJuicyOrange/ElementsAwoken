using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PuzzlePiece : ModProjectile
    {
        public int type = 0;
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.penetrate = 1;

            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.magic = true;

            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charge Rifle");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(0, 3);
                projectile.localAI[0] = 1;
            }
            projectile.velocity.Y += 0.16f;

            projectile.rotation += Main.rand.NextFloat(0.08f, 0.1f);
        }

        public override void Kill(int timeLeft)
        {        
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
        }
    }
}