using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PrinceAura : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 100;
            projectile.height = 2;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prince Aura");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.localAI[0]--;
            if (projectile.localAI[0] <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-40, 40), projectile.position.Y, 0f, 9f, mod.ProjectileType("PrinceRain"), projectile.damage, 0f, projectile.owner, 0f, 0f);
                projectile.localAI[0] = 5;
            }
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("PrinceDust"), 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.4f;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("PrinceAura")] > 5)
            {
                projectile.Kill();
            }
        }
    }
}