using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Pets.Wyvern
{
    public class WyvernHead : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.minion = true;
            //Main.projPet[projectile.type] = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.timeLeft *= 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if ((int)Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (!player.active)
            {
                projectile.active = false;
                return;
            }

            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.wyvernPet = false;
            }
            if (modPlayer.wyvernPet)
            {
                projectile.timeLeft = 2;
            }

            // head
            Vector2 center14 = player.Center;
            int num1049 = 10;
            int num1053 = -1;
            if (projectile.Distance(center14) > 2000f)
            {
                projectile.Center = center14;
                projectile.netUpdate = true;
            }
            if (num1053 != -1)
            {
                NPC nPC15 = Main.npc[num1053];
                Vector2 vector148 = nPC15.Center - projectile.Center;
                float num1057 = (float)(vector148.X > 0f).ToDirectionInt();
                float num1058 = (float)(vector148.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector148.Length() < 600f)
                {
                    scaleFactor15 = 0.6f;
                }
                if (vector148.Length() < 300f)
                {
                    scaleFactor15 = 0.8f;
                }
                if (vector148.Length() > nPC15.Size.Length() * 0.75f)
                {
                    projectile.velocity += Vector2.Normalize(vector148) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(projectile.velocity, vector148) < 0.25f)
                    {
                        projectile.velocity *= 0.8f;
                    }
                }
                float num1059 = 30f;
                if (projectile.velocity.Length() > num1059)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num1059;
                }
            }
            else
            {
                float num1060 = 0.2f;
                Vector2 vector149 = center14 - projectile.Center;
                if (vector149.Length() < 200f)
                {
                    num1060 = 0.12f;
                }
                if (vector149.Length() < 140f)
                {
                    num1060 = 0.06f;
                }
                if (vector149.Length() > 100f)
                {
                    if (Math.Abs(center14.X - projectile.Center.X) > 20f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num1060 * (float)Math.Sign(center14.X - projectile.Center.X);
                    }
                    if (Math.Abs(center14.Y - projectile.Center.Y) > 10f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num1060 * (float)Math.Sign(center14.Y - projectile.Center.Y);
                    }
                }
                else if (projectile.velocity.Length() > 2f)
                {
                    projectile.velocity *= 0.96f;
                }
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                }
                float num1061 = 15f;
                if (projectile.velocity.Length() > num1061)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num1061;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int direction = projectile.direction;
            projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
            if (direction != projectile.direction)
            {
                projectile.netUpdate = true;
            }
            float num1062 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
            projectile.position = projectile.Center;
            projectile.scale = 1f + num1062 * 0.01f;
            projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
            projectile.Center = projectile.position;      
        }
    }
}
