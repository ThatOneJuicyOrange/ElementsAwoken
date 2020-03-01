using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FreezeBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freeze Ray");
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
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 4f)
            {
                for (int i = 0; i < 4; i++)
                {
                        Vector2 vector33 = projectile.position;
                        vector33 -= projectile.velocity * ((float)i * 0.25f);
                        projectile.alpha = 255;
                        int num448 = Dust.NewDust(vector33, 1, 1, 135, 0f, 0f, 0, default(Color), 0.75f);
                        Main.dust[num448].position = vector33;
                        Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                        Main.dust[num448].velocity *= 0.05f;
                        Main.dust[num448].noGravity = true;
                }
                return;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("IceBound"), 200);
        }
    }
}