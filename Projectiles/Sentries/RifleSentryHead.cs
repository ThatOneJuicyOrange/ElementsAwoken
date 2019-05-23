using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Sentries
{
    public class RifleSentryHead : ModProjectile
    {
        public int shootTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
           // projectile.aiStyle = 53;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.sentry = true;

            projectile.timeLeft = Projectile.SentryLifeTime;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rifle Sentry");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            Projectile parent = Main.projectile[(int)projectile.ai[0]];
            NPC target = Main.npc[0];
            if (!parent.active)
            {
                projectile.Kill();
            }
            projectile.Center = parent.Center + new Vector2(1, -14);

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (ValidTarget(nPC))
                {
                    target = nPC;
                }
            }
            if (ValidTarget(target))
            {
                Vector2 direction = target.Center - projectile.Center;
                if (direction.X > 0f)
                {
                    projectile.spriteDirection = 1;
                    parent.spriteDirection = 1;
                    float desiredRotation = direction.ToRotation();
                    projectile.rotation = desiredRotation;
                }
                if (direction.X < 0f)
                {
                    projectile.spriteDirection = -1;
                    parent.spriteDirection = -1;
                    float desiredRotation = direction.ToRotation() + 3.14f;
                    projectile.rotation = desiredRotation;
                }
                shootTimer--;
                if (shootTimer <= 0 && Collision.CanHit(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                {
                    float Speed = 9f;
                    //float rotation = (float)Math.Atan2(projectile.Center.Y - target.Center.Y, projectile.Center.X - target.Center.X);
                    int inverse = projectile.spriteDirection == -1 ? -1 : 1;
                    Vector2 speed = new Vector2((float)((Math.Cos(projectile.rotation) * Speed) * inverse), (float)((Math.Sin(projectile.rotation) * Speed) * inverse));
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 11);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, ProjectileID.Bullet, projectile.damage, projectile.knockBack, projectile.owner);
                    shootTimer = 90;
                }
            }
            else
            {
                /*if (projectile.spriteDirection == -1)
                {
                    RotateTo(5, 0.01f);
                }
                else
                {
                    RotateTo(1, 0.01f);
                }*/
                RotateTo(0, 0.01f);
            }
        }
        private void RotateTo(float desiredRotation, float speed = 0.025f)
        {
            if (MathHelper.Distance(projectile.rotation, desiredRotation) > 0.1f)
            {
                if (projectile.rotation > desiredRotation)
                {
                    projectile.rotation -= speed;
                }
                else if (projectile.rotation < desiredRotation)
                {
                    projectile.rotation += speed;
                }
            }
            else
            {
                projectile.rotation = desiredRotation;
            }
        }
        private bool ValidTarget(NPC target)
        {
            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && target.CanBeChasedBy(projectile, true) && Vector2.Distance(target.Center, projectile.Center) <= 400)
            {
                return true;
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Vector2 drawPos = Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            SpriteEffects spriteEffects = projectile.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, default(Color), projectile.rotation, drawOrigin, projectile.scale, spriteEffects, 0f);

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
    }
}