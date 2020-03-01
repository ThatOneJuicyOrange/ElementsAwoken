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
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 2;

            projectile.penetrate = -1;

            projectile.melee = true;
            projectile.friendly = true;

            projectile.timeLeft = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prince Aura");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.ai[0]--;
            if (projectile.ai[0] <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-projectile.width / 2, projectile.width / 2), projectile.position.Y, 0f, 9f, mod.ProjectileType("PrinceRain"), projectile.damage, 0f, projectile.owner, 0f, 0f);
                projectile.ai[0] = 8;
            }
            
            if (projectile.localAI[0] == 0) projectile.ai[1] += 3;
            else projectile.ai[1] -= 3;
            if (projectile.ai[1] >= projectile.width) projectile.localAI[0]++;
            if (projectile.ai[1] <= 0) projectile.localAI[0] = 0;


                int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
                for (int i = 0; i < dustLength; i++)
                {
                    float Y = (float)Math.Sin(projectile.ai[1] / 5) * 10;
                    Vector2 dustPos = new Vector2(projectile.ai[1], Y);

                    Dust dust = Main.dust[Dust.NewDust(projectile.position + dustPos, 2, 2, mod.DustType("PrinceDust"))];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                    dust.alpha = projectile.alpha;
                }
            if (player.ownedProjectileCounts[mod.ProjectileType("PrinceAura")] > 5)
            {
                projectile.Kill();
            }
        }
    }
}