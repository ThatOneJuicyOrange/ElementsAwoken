using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class MiningChargeSpark : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 90;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Charge");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(3)) target.AddBuff(ModContent.BuffType<Incineration>(), 120);
            else target.AddBuff(BuffID.OnFire, 120);
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.16f;
            if (Main.rand.NextBool(6))
            {
                Dust smoke = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31)];
                smoke.velocity = projectile.velocity * 0.2f;
                smoke.scale *= 1.5f;
                smoke.noGravity = true;
            }
            Dust fire = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f)];
            fire.velocity *= 0.6f;
            fire.scale *= 0.6f;
            fire.noGravity = true;
        }
    }
}