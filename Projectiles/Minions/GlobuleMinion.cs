using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.IO;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.Minions.MinionProj;
using ElementsAwoken.Buffs.MinionBuffs;

namespace ElementsAwoken.Projectiles.Minions
{
    public class GlobuleMinion : ModProjectile
    {
        public float hitNum = 0;
        public float splitCD = 900;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(hitNum);
            writer.Write(splitCD);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            hitNum = reader.ReadSingle();
            splitCD = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 2f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Globule");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(BuffType<GlobuleMinionBuff>(), 3600);
            if (player.dead) modPlayer.globule = false;
            if (modPlayer.globule)  projectile.timeLeft = 2;

            projectile.rotation = projectile.velocity.X * 0.075f;
            ProjectileUtils.PushOtherEntities(projectile);
            splitCD--;
            if (splitCD == 0)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), ProjectileType<GlobuleMinionSmall>(), (int)(projectile.damage * 1.25f), projectile.knockBack, Main.myPlayer, 0f, projectile.whoAmI);
                        Main.PlaySound(SoundID.Item94, projectile.position);
                    }
                }
            }
            else if (splitCD <= 0)
            {
                projectile.alpha = 255;
                projectile.Center = player.Center + new Vector2(0, -80);
                if (CountProjectiles() <= 0)
                {
                    splitCD = 900;
                    int numDusts = 36;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.One * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                        Vector2 velocity = position - projectile.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, DustID.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 4f;
                    }
                }
            }
            else
            {
                projectile.alpha = 0;
                Vector2 targetPos = projectile.position;
                float targetDist = 500;
                bool target = false;
                projectile.tileCollide = true;
                NPC manualTarget = projectile.OwnerMinionAttackTargetNPC;
                if (manualTarget != null)
                {
                    targetDist = Vector2.Distance(manualTarget.Center, projectile.Center);
                    targetPos = manualTarget.Center;
                    target = true;
                }
                else
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC npc = Main.npc[k];
                        if (npc.CanBeChasedBy(this, false))
                        {
                            float distance = Vector2.Distance(npc.Center, projectile.Center);
                            if ((distance < targetDist) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                            {
                                targetDist = distance;
                                targetPos = npc.Center;
                                target = true;
                            }
                        }
                    }
                }
                if (!target)
                {
                    projectile.friendly = true;
                    float speed = 16f;
                    Vector2 toTarget = player.Center - projectile.Center;
                    float dist = (float)Math.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);
                    if (dist < 100)
                    {
                        projectile.velocity *= 0.97f;
                    }
                    else if (dist < 2000)
                    {
                        dist = speed / dist;
                        toTarget.X *= dist;
                        toTarget.Y *= dist;
                        projectile.velocity.X = (projectile.velocity.X * 20f + toTarget.X) / 21f;
                        projectile.velocity.Y = (projectile.velocity.Y * 20f + toTarget.Y) / 21f;
                    }
                    else
                    {
                        projectile.Center = player.Center;
                    }
                  
                    projectile.friendly = false;
                    return;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                if (projectile.ai[1] == 0f)
                {
                    projectile.ai[0]--;
                    projectile.friendly = true;
                    float speed = 8f;
                    float num1042 = targetPos.X - projectile.Center.X;
                    float num1041 = targetPos.Y - projectile.Center.Y;
                    float dist = (float)Math.Sqrt(num1042 * num1042 + num1041 * num1041);
                    if (dist < 100f)
                    {
                        speed = 10f;
                    }
                    if (dist < 300f && projectile.ai[0] <= 0)
                    {
                        Vector2 toTarget = new Vector2(num1042, num1041);
                        toTarget.Normalize();
                        toTarget *= 20f;
                        projectile.velocity.X = toTarget.X;
                        projectile.velocity.Y = toTarget.Y;

                        projectile.ai[0] = 30;
                    }
                    dist = speed / dist;
                    num1042 *= dist;
                    num1041 *= dist;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num1042) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num1041) / 15f;
                }
                else
                {
                    projectile.friendly = false;
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 10f)
                    {
                        projectile.velocity *= 1.05f;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffType<Buffs.Debuffs.Starstruck>(), 300);
            target.immune[projectile.owner] = 5;

            hitNum++; 
            if (hitNum > 10)
            {
                hitNum = 0;
            }
            else if (hitNum > 3)
            {
                projectile.ai[1] = 1f;
            }
            else
            {
                projectile.ai[1] = 8f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
        private int CountProjectiles()
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ProjectileType<GlobuleMinionSmall>() && Main.projectile[i].ai[1] == projectile.whoAmI) num++;
            }
            return num;
        }
    }
}