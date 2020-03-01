using System;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaGiantCloud : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.penetrate = 1;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 420;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spore Cloud");
        }
        public override void AI()
        {
            projectile.velocity *= 0.96f;

            projectile.ai[1]--;
            if (projectile.ai[1] <= 0)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.NextFloat(-3.5f, 3.5f), Main.rand.NextFloat(-1.5f, 1.5f)), mod.ProjectileType("AzanaCloud"), projectile.damage, projectile.knockBack, Main.myPlayer);
                projectile.ai[1] = Main.rand.Next(2, 12);
            }
            if (MathHelper.Distance(projectile.velocity.X, 0) < 0.1f && MathHelper.Distance(projectile.velocity.Y, 0) < 0.1f) projectile.velocity = Vector2.Zero;
            if (projectile.velocity == Vector2.Zero)
            {
                projectile.ai[0]++;

                // shoot in circle
                Vector2 offset = new Vector2(400, 0);
                float rotateSpeed = 0.005f;
                projectile.ai[0] += 1;
                float spinAI = projectile.ai[0] / rotateSpeed;

                if (projectile.ai[0] % 60 == 0)
                {
                    if (projectile.ai[0] % 120 == 0) Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/GiantLaser"));

                    float speed = 3f;
                    Vector2 shootTarget1 = projectile.Center + offset.RotatedBy(spinAI);
                    float rotation = (float)Math.Atan2(projectile.Center.Y - shootTarget1.Y, projectile.Center.X - shootTarget1.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                    Projectile.NewProjectile(projectile.Center + projSpeed * 20, projSpeed, mod.ProjectileType("AzanaBeam"), projectile.damage, 0f, Main.myPlayer);
                }
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
    }
}