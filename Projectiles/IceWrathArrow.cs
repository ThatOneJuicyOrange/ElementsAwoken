using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class IceWrathArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Wrath");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
            int type = mod.ProjectileType("IceWrathBeam");
            //up
            Projectile.NewProjectile(vector8.X, vector8.Y, -1, -8, type, damage, 0f, 0);
            Projectile.NewProjectile(vector8.X, vector8.Y, 1, -8, type, damage, 0f, 0);

            Projectile.NewProjectile(vector8.X, vector8.Y, 8, 0, type, damage, 0f, 0);
            Projectile.NewProjectile(vector8.X, vector8.Y, -8, 0, type, damage, 0f, 0);
            Projectile.NewProjectile(vector8.X, vector8.Y, 8, 8, type, damage, 0f, 0);
            Projectile.NewProjectile(vector8.X, vector8.Y, -8, -8, type, damage, 0f, 0);
            Projectile.NewProjectile(vector8.X, vector8.Y, -8, 8, type, damage, 0f, 0);
            Projectile.NewProjectile(vector8.X, vector8.Y, 8, -8, type, damage, 0f, 0);
            target.AddBuff(BuffID.Frostburn, 200);

        }
    }
}