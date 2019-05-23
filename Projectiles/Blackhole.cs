using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Blackhole : ModProjectile
    {
        public bool hasOrbital = false;

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blackhole");
            Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 80);
            target.immune[projectile.owner] = 5;
        }
        public override void AI()
        {
            projectile.velocity.X = 0f;
            projectile.velocity.Y = 0f;
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
            }
            if (!hasOrbital)
            {
                int swirlCount = 12;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("BlackholeOrbit"), projectile.damage, projectile.knockBack, 0, l * distance, projectile.whoAmI);
                }
                hasOrbital = true;
            }
            if (ProjectileUtils.CountProjectiles(projectile.type) > 3)
            {
                if (ProjectileUtils.HasLeastTimeleft(projectile.whoAmI))
                {
                    projectile.alpha += 10;
                    if (projectile.alpha >= 255)
                    {
                        projectile.Kill();
                    }
                }
            }
            // grav
            int maxDist = 400;
            float gravStength = 0.3f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                bool immune = false;
                foreach (int k in ElementsAwoken.instakillImmune)
                {
                    if (npc.type == k)
                    {
                        immune = true;
                    }
                }
                if (!immune && npc.active && npc.damage > 0 && !npc.boss && npc.lifeMax < 10000 && Vector2.Distance(npc.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - npc.Center.X, projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity.X += toTarget.X * gravStength;
                    npc.velocity.Y += toTarget.Y * gravStength * 5;
                }
            }
            for (int i = 0; i < Main.item.Length; i++)
            {
                Item item = Main.item[i];
                if (item.active && Vector2.Distance(item.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - item.Center.X, projectile.Center.Y - item.Center.Y);
                    toTarget.Normalize();
                    item.velocity += toTarget * gravStength;
                }
            }

        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}