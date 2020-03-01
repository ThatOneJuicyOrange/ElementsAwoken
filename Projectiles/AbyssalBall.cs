using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AbyssalBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyss Ball");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
        }
        public override void AI()
        {
            Dust fire = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, Main.rand.NextBool() ? 234 : DustID.PinkFlame, projectile.velocity.X * 0.6f, projectile.velocity.Y * 0.6f, 130, default(Color), 3.75f)];
            fire.velocity *= 0.6f;
            fire.scale *= 0.6f;
            fire.noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, Main.rand.NextBool() ? 234 : DustID.PinkFlame, 0f, 0f, 100, default);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}