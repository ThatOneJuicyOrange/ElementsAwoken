using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{
    public class BabyPuff : ModProjectile
    {
        public int shootTimer = 0;
        public int hitTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(shootTimer);
            writer.Write(hitTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            shootTimer = reader.ReadInt32();
            hitTimer = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 26;

            projectile.netImportant = true;
            projectile.friendly = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.tileCollide = false;
            projectile.minion = true;

            projectile.minionSlots = 1;

            projectile.aiStyle = 26;
            aiType = 266;

            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Puff");
            Main.projFrames[projectile.type] = 3;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.frame > 2)
                projectile.frame = 2;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("BabyPuffBuff"), 3600);
            if (player.dead)
            {
                modPlayer.babyPuff = false;
            }
            if (modPlayer.babyPuff)
            {
                projectile.timeLeft = 2;
            }

            if (projectile.frame == 2)
            {
                int dustSize = 10;
                for (int i = 0; i < 3; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.Bottom - new Vector2(dustSize / 2, 12), dustSize, dustSize, 21)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 3 * (float)i;
                    dust.noGravity = true;
                    dust.scale *= 0.7f;
                }
            }
            shootTimer--;
            if (shootTimer <= 0)
            {
                float max = 200f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 3f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y - 3f, mod.ProjectileType("BabyPuffSpike"), projectile.damage, projectile.knockBack, projectile.owner);
                        shootTimer = 60;
                        break;
                    }
                }
            }
            hitTimer--;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (hitTimer > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            hitTimer = 20;
        }
    }
}