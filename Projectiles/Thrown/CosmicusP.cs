using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class CosmicusP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = -1;

            //projectile.aiStyle = 93;
            //aiType = ProjectileID.Daybreak;

            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wrath");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] != 0)
            {
                return false;
            }
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (crit)
            {
                damage *= 3;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 200);

            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] = 0f;
                int num14 = -target.whoAmI - 1;
                projectile.ai[0] = (float)num14;
                projectile.velocity = target.Center - projectile.Center;
            }

        }
        public override void AI()
        {
            if (projectile.velocity.Y > 18f)
            {
                projectile.velocity.Y = 18f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] > 20f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + 0.1f;
                    projectile.velocity.X = projectile.velocity.X * 0.992f;
                }
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                return;
            }
            projectile.tileCollide = false;
            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
                projectile.velocity *= 0.6f;
            }
            else
            {
                projectile.tileCollide = false;
                int num895 = (int)(-(int)projectile.ai[0]);
                num895--;
                projectile.position = Main.npc[num895].Center - projectile.velocity;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                if (!Main.npc[num895].active || Main.npc[num895].life < 0)
                {
                    projectile.tileCollide = true;
                    projectile.ai[0] = 0f;
                    projectile.ai[1] = 20f;
                    projectile.velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    projectile.velocity.Normalize();
                    projectile.velocity *= 6f;
                    projectile.netUpdate = true;
                }
                else if (projectile.velocity.Length() > (float)((Main.npc[num895].width + Main.npc[num895].height) / 3))
                {
                    projectile.velocity *= 0.99f;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 242, 197, 6 }, damageType: "thrown");
        }
    }   
}