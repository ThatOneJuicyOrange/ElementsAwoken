using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class AbyssCultist : ModProjectile
    {
        public int shootTimer = 30;

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 5f;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 317;
            projectile.aiStyle = 54;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyss Cultist");
            Main.projFrames[projectile.type] = 8;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 20)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 7)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.3f, 0.3f);

            projectile.rotation += projectile.velocity.X * 0.04f;
            bool flag64 = projectile.type == mod.ProjectileType("AbyssCultist");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(mod.BuffType("AbyssCultistBuff"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.abyssCultist = false;
                }
                if (modPlayer.abyssCultist)
                {
                    projectile.timeLeft = 2;
                }
            }
            ProjectileUtils.PushOtherEntities(projectile);

            shootTimer--;
            float max = 400f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max && Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    int numberProjectiles = Main.rand.Next(4,6);
                    Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                    int type = mod.ProjectileType("AbyssCultistBolt");
                    float Speed = 12f;
                    float rotation = (float)Math.Atan2(vector8.Y - (nPC.position.Y + (nPC.height * 0.5f)), vector8.X - (nPC.position.X + (nPC.width * 0.5f)));
                    int damage = projectile.damage / 3;
                    if (shootTimer <= 0)
                    {
                        for (int l = 0; l < numberProjectiles; l++)
                        {
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(90));
                            Projectile.NewProjectile(vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        }
                        shootTimer = 30;
                    }
                }
            }
            // idk wtf is happening, how to even arc?
           /* for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max && Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    float numberProjectiles = 3;
                    Vector2 vector8 = new Vector2(projectile.Center.X, projectile.Center.Y);
                    float rotation = MathHelper.ToRadians(45);
                    Vector2 direction = (nPC.Center - projectile.Center).SafeNormalize(-Vector2.UnitY);
                    if (shootTimer <= 0)
                    {
                        for (int k = 0; k < numberProjectiles; k++)
                        {
                            Vector2 perturbedSpeed = direction.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X * 10, perturbedSpeed.Y * 10, ProjectileID.FireArrow, 4, 3f, projectile.owner);
                        }
                        shootTimer = 50;
                    }
                }
            }*/

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}