using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ImbalancerMine : ModProjectile
    {
        public int shootTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(shootTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            shootTimer = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imbalancer");
        }
        public override void AI()
        {
            int maxDist = 150;
            if (ProjectileUtils.CountProjectiles(projectile.type, projectile.owner) > 6 && ProjectileUtils.HasLeastTimeleft(projectile.whoAmI)) projectile.Kill();
            if (projectile.ai[1] == 1)
            {
                NPC stick = Main.npc[(int)projectile.ai[0]];
                if (stick.active)
                {
                    projectile.Center = stick.Center - projectile.velocity * 2f;
                    projectile.gfxOffY = stick.gfxOffY;
                    stick.AddBuff(ModContent.BuffType<ElectrifiedNPC>(), 300);
                }
                else projectile.Kill();
            }
            else if (projectile.ai[1] == 2)
            {
                projectile.velocity = Vector2.Zero;

                if ((projectile.velocity.X > 0.5f || projectile.velocity.X < -0.5f) || (projectile.velocity.Y > 0.5f || projectile.velocity.Y < -0.5f))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                        dust.velocity = Vector2.Zero;
                        dust.position -= projectile.velocity / 6f * (float)i;
                        dust.noGravity = true;
                        dust.scale = 1f;
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                        Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset - Vector2.One * 4, 0, 0, 226, 0, 0, 100)];
                        dust.noGravity = true;
                        dust.velocity *= 0.2f;
                        if (Collision.SolidCollision(dust.position, 4, 4)) dust.active = false;
                    }
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC nPC = Main.npc[i];
                            if (nPC.CanBeChasedBy(this) && Collision.CanHit(projectile.Center, 2, 2, nPC.Center, 2, 2) && Vector2.Distance(projectile.Center, nPC.Center) <= maxDist)
                            {
                                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"), 1);

                                float Speed = 9f;
                                float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                                Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projSpeed.X, projSpeed.Y, ModContent.ProjectileType<ImbalancerLightning>(), projectile.damage * 2, projectile.knockBack, projectile.owner);
                                shootTimer = 20;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                projectile.velocity.Y += 0.13f;
                projectile.rotation += projectile.velocity.X * 0.02f;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] == 2) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = target.whoAmI;
            projectile.ai[1] = 1;
            projectile.velocity =(target.Center - projectile.Center) * 0.75f;
            projectile.netUpdate = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            Projectile exp = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<Explosion>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f)];
            exp.ranged = true;
            int numDusts = 30;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = (Vector2.One * new Vector2((float)projectile.width / 2f, (float)projectile.height) / 2f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                Vector2 velocity = position - projectile.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 226, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[1] = 2;
            return false;
        }
    }
}