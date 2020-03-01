using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class DiscordantArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            aiType = ProjectileID.WoodenArrowFriendly;

            projectile.extraUpdates = 1;
            projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Arrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ChaosBurn"), 200);
            target.immune[projectile.owner] = 5;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.6f, 0.1f, 0.3f);

            int numChaosArrows = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("DiscordantArrowChaos")) numChaosArrows++;
            }
            Main.NewText(numChaosArrows);
            if (numChaosArrows < 100)
            {
                if (Main.rand.Next(30) == 0)
                {
                    float max = 400f;
                    NPC npc = null;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC test = Main.npc[i];
                        if (test.active && !test.dontTakeDamage && Vector2.Distance(projectile.Center, test.Center) <= max)
                        {
                            npc = test;
                        }
                    }
                    if (npc != null)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - npc.Center.Y, projectile.Center.X - npc.Center.X);

                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("DiscordantArrowChaos"), projectile.damage, projectile.knockBack, projectile.owner);
                    }
                    else Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0.9f, projectile.velocity.Y * 0.9f, mod.ProjectileType("DiscordantArrowChaos"), (int)(projectile.damage * 1.5f), projectile.knockBack, projectile.owner);
                }
            }
        }
        
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(ModContent.GetTexture("ElementsAwoken/Projectiles/Arrows/DiscordantArrowChaos"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}