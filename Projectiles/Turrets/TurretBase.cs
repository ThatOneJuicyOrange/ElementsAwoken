using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Turrets
{
    public abstract class TurretBase : ModProjectile
    {

        private float aiState
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        private float shootTimer
        {
            get => projectile.ai[1];
            set => projectile.ai[1] = value;
        }

        protected float maxRange = 600f;
        protected float deadBottomAngle = 0.75f;
        protected float projSpeed = 24f;
        protected float shootCDAmount = 120;
        protected string baseTex = "Projectiles/Turrets/RifleSentryBase";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC npc)
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.position += projectile.velocity;
            projectile.velocity = Vector2.Zero;
            return false;
        }
        // based off ballista code
        public override void AI()
        {
            if (aiState == 0f)
            {
                projectile.direction = (projectile.spriteDirection = Main.player[projectile.owner].direction);
                aiState = 1f;
                shootTimer = 0f;
                projectile.netUpdate = true;
                if (projectile.direction == -1)
                {
                    projectile.rotation = 3.14159274f;
                }
            }
            if (aiState == 1f)
            {
                bool flag = false;
                if (shootTimer < shootCDAmount)
                {
                    shootTimer += 1f;
                }
                else
                {
                    flag = true;
                }
                int num8 = FindTarget(maxRange, deadBottomAngle, projectile.Center);
                if (num8 != -1)
                {
                    Vector2 vector = (Main.npc[num8].Center - projectile.Center).SafeNormalize(Vector2.UnitY);
                    projectile.rotation = projectile.rotation.AngleLerp(vector.ToRotation(), 0.08f);
                    if (projectile.rotation > 1.57079637f || projectile.rotation < -1.57079637f)
                    {
                        projectile.direction = -1;
                    }
                    else
                    {
                        projectile.direction = 1;
                    }
                    if (flag && projectile.owner == Main.myPlayer)
                    {
                        projectile.direction = Math.Sign(vector.X);
                        aiState = 2f;
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    float targetAngle = 0f;
                    if (projectile.direction == -1)
                    {
                        targetAngle = 3.14159274f;
                    }
                    projectile.rotation = projectile.rotation.AngleLerp(targetAngle, 0.05f);
                }
            }
            else if (aiState == 2f)
            {
                if (shootTimer >= shootCDAmount)
                {
                    Vector2 vector2 = new Vector2((float)projectile.direction, 0f); // shoot whichever way its facing
                    int targetIndex = FindTarget(maxRange, deadBottomAngle, projectile.Center);
                    if (targetIndex != -1)
                    {
                        vector2 = (Main.npc[targetIndex].Center - projectile.Center).SafeNormalize(Vector2.UnitX * (float)projectile.direction);
                    }
                    projectile.rotation = vector2.ToRotation();
                    if (projectile.rotation > 1.57079637f || projectile.rotation < -1.57079637f)
                    {
                        projectile.direction = -1;
                    }
                    else
                    {
                        projectile.direction = 1;
                    }
                    if (projectile.owner == Main.myPlayer)
                    {
                        if (targetIndex != -1)
                        {
                            Shoot(Main.npc[targetIndex]);
                        }
                    }
                }
                aiState = 1f;
                shootTimer = 0;
            }
            projectile.spriteDirection = projectile.direction;
            projectile.tileCollide = true;
            projectile.velocity.Y = projectile.velocity.Y + 0.2f;

            ExtraAI();
        }
        public virtual void ExtraAI()
        {
        }
        private int FindTarget(float shot_range, float deadBottomAngle, Vector2 shootingSpot)
        {
            int num = -1;
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (ownerMinionAttackTargetNPC.CanBeChasedBy(this, true))
                    {
                        float num2 = Vector2.Distance(shootingSpot, ownerMinionAttackTargetNPC.Center);
                        if (num2 <= shot_range)
                        {
                            Vector2 vector = (ownerMinionAttackTargetNPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                            if ((Math.Abs(vector.X) >= Math.Abs(vector.Y) * deadBottomAngle || vector.Y <= 0f) && (num == -1 || num2 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, ownerMinionAttackTargetNPC.Center, 0, 0))
                            {
                                num = ownerMinionAttackTargetNPC.whoAmI;
                            }
                        }
                    }
                }
                if (num != -1)
                {
                    return num;
                }
            }
            for (int j = 0; j < 200; j++)
            {
                NPC nPC = Main.npc[j];
                if (nPC.CanBeChasedBy(this, true))
                {
                    float num3 = Vector2.Distance(shootingSpot, nPC.Center);
                    if (num3 <= shot_range)
                    {
                        Vector2 vector2 = (nPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                        if ((Math.Abs(vector2.X) >= Math.Abs(vector2.Y) * deadBottomAngle || vector2.Y <= 0f) && (num == -1 || num3 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, nPC.Center, 0, 0))
                        {
                            num = j;
                        }
                    }
                }
            }
            return num;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            // base
            {
                Texture2D baseTexture = mod.GetTexture(baseTex);
                Vector2 baseOrigin = baseTexture.Size() * new Vector2(0.5f, 1f);
                baseOrigin.Y -= 2f;
                Vector2 drawPos = projectile.Bottom - Main.screenPosition + new Vector2(0,projectile.gfxOffY);
                spriteBatch.Draw(baseTexture, drawPos, null, projectile.GetAlpha(lightColor), 0f, baseOrigin, projectile.scale, SpriteEffects.None & SpriteEffects.FlipHorizontally, 0f);
            }
            //head
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipVertically;
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Height * 0.5f);
                Vector2 drawPos = projectile.Top - Main.screenPosition;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, projectile.GetAlpha(lightColor), projectile.rotation, drawOrigin, projectile.scale, spriteEffects, 0f);
            }
            return false;
        }
        public virtual void Shoot(NPC target) // use this to decide how it shoots 
        {
        }
    }
}