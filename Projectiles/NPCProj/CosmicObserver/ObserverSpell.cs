using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.CosmicObserver
{
    public class ObserverSpell : ModProjectile
    {
        public float rotSpeed = 0.05f;
        public float innerRot = 0f;
        public float middleRot = 0f;
        public override void SetDefaults()
        {
            projectile.width = 56;
            projectile.height = 56;

            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 10000;
            projectile.light = 0.4f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Observer Shard");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }

        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[1]];
            Player P = Main.player[Main.myPlayer];
            projectile.Center = parent.Center;
            if (projectile.localAI[0] == 0)
            {
                projectile.scale = 0.2f;
                projectile.localAI[0]++;
            }

            if (rotSpeed <= 0.3)
            {
                rotSpeed += 0.0005f;
            }
            projectile.rotation += rotSpeed;
            innerRot -= rotSpeed;
            middleRot += rotSpeed;

            if (projectile.scale < 1f)
            {
                projectile.scale += 0.005f;
            }
            projectile.ai[0]++;
            if (projectile.ai[0] > 180)
            {
                projectile.Kill();
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
                float rotation = (float)Math.Atan2(projectile.Center.Y - P.Center.Y, projectile.Center.X - P.Center.X);
                float speed = 15f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("ObserverFireball"), projectile.damage, 0f, 0);
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Height * 0.5f);
            Vector2 drawPos = projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);

            spritebatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, Color.White, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            spritebatch.Draw(mod.GetTexture("Projectiles/NPCProj/CosmicObserver/ObserverSpell1"), drawPos, null, Color.White, innerRot, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            spritebatch.Draw(mod.GetTexture("Projectiles/NPCProj/CosmicObserver/ObserverSpell2"), drawPos, null, Color.White, middleRot, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}