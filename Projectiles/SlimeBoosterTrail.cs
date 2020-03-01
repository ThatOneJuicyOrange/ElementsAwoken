using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SlimeBoosterTrail : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;

            projectile.penetrate = -1; // having penetrate of 1 makes it hit a lot
            projectile.timeLeft = 20;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override void AI()
        {
            Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 4, 0, 0, 150, new Color(0, 80, 255, 100), 2f)];
            dust.velocity.X *= 0.6f;
            dust.velocity.Y *= 0.3f;
            dust.scale *= 0.6f;
            dust.noGravity = true;
        }
    }
}