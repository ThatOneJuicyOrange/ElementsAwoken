using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WispBoltReflector : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 30;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wisp Bolt");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Incineration>(), 300);
        }
        public override void AI()
        {
            foreach (Projectile proj in Main.projectile)
            {
                if (projectile.Hitbox.Intersects(proj.Hitbox) && proj.hostile)
                {
                    Vector2 toMouse = Main.MouseWorld - Main.player[projectile.owner].Center;
                    toMouse.Normalize();
                    proj.velocity = toMouse * 20;
                    proj.hostile = false;
                    proj.friendly = true;
                }
            }
        }
    }
}