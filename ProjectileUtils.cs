using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken
{
    public class ProjectileUtils : GlobalProjectile
    {
        public static void PushOtherEntities(Projectile projectile, List<int> otherTypes = null, float pushStrength = 0.05f)
        {
            if (otherTypes == null) otherTypes = new List<int>();
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile other = Main.projectile[k];
                if (k != projectile.whoAmI && (other.type == projectile.type || otherTypes.Contains(projectile.type)) && other.active && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    if (projectile.position.X < other.position.X)
                    {
                        projectile.velocity.X -= pushStrength;
                    }
                    else
                    {
                        projectile.velocity.X += pushStrength;
                    }
                    if (projectile.position.Y < other.position.Y)
                    {
                        projectile.velocity.Y -= pushStrength;
                    }
                    else
                    {
                        projectile.velocity.Y += pushStrength;
                    }
                }
            }
        }
        public static void HeldWandPos(Projectile projectile, Player player, float additionalRotation = 0f)
        {
            Vector2 offset = projectile.velocity;
            offset.Normalize();
            offset *= Main.projectileTexture[projectile.type].Width / 3;

            Vector2 vector24 = player.RotatedRelativePoint(player.MountedCenter, true) + offset.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2));
            player.heldProj = projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            projectile.position.X = vector24.X - (float)(projectile.width / 2);
            projectile.position.Y = vector24.Y - (float)(projectile.height / 2);
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));

            float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
            Vector2 vector3 = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
            if (player.gravDir == -1f)
            {
                value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
            }
            Vector2 vector4 = Vector2.Normalize(value2);
            if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
            {
                vector4 = -Vector2.UnitY;
            }
            vector4 *= scaleFactor;
            if (vector4.X != projectile.velocity.X || vector4.Y != projectile.velocity.Y)
            {
                projectile.netUpdate = true;
            }
            projectile.velocity = vector4;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)(Math.PI / 4);

            projectile.rotation += additionalRotation;
        }
        public static int Explosion(Projectile projectile, int dustID, int damage = -1, string damageType = "normal", float numDustScale = 1f, float dustScale = 1f, int soundID = 14)
        {
            return Explosion(projectile, new int[] { dustID }, damage, damageType, numDustScale, dustScale, soundID);
        }
        public static int Explosion(Projectile projectile, int[] dustIDs, int damage = -1, string damageType = "normal", float numDustScale = 1f, float dustScale = 1f, int soundID = 14)
        {
            if (damage == -1) damage = projectile.damage;
            int expID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ProjectileType<Explosion>(), damage, projectile.knockBack, projectile.owner);
            Projectile exp = Main.projectile[expID];
            exp.melee = damageType == "melee" ? true : false;
            exp.ranged = damageType == "ranged" ? true : false;
            exp.thrown = damageType == "thrown" ? true : false;
            exp.magic = damageType == "magic" ? true : false;
            exp.minion = damageType == "minion" ? true : false;
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, soundID);
            ExplosionDust(projectile, dustIDs, numDustScale, dustScale);
            return expID;
        }
        public static int HostileExplosion(Projectile projectile, int dustID, int damage = -1, float numDustScale = 1f, float dustScale = 1f, int soundID = 14)
        {
            return HostileExplosion(projectile, new int[] { dustID }, damage, numDustScale, dustScale, soundID);
        }
        public static int HostileExplosion(Projectile projectile, int[] dustIDs, int damage = -1, float numDustScale = 1f, float dustScale = 1f, int soundID = 14)
        {
            if (damage == -1) damage = projectile.damage;
            int expID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ProjectileType<ExplosionHostile>(), damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, soundID);
            ExplosionDust(projectile, dustIDs, numDustScale, dustScale);
            return expID;
        }
        public static void FadeOut(Projectile projectile, int rate)
        {
            projectile.alpha += rate;
            if (projectile.alpha >= 255) projectile.Kill();
        }
        private static void ExplosionDust(Projectile projectile, int[] dustIDs, float numDustScale = 1f, float dustScale = 1f)
        {
            int num = GetInstance<Config>().lowDust ? 10 : 20;
            num = (int)(num * numDustScale);
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
                dust.scale *= dustScale;
            }
            int num2 = GetInstance<Config>().lowDust ? 5 : 10;
            num2 = (int)(num2 * numDustScale);
            for (int i = 0; i < num2; i++)
            {
                int dustID = dustIDs[Main.rand.Next(dustIDs.Length)];
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust.scale *= dustScale;
                int dustID2 = dustIDs[Main.rand.Next(dustIDs.Length)];
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
                dust.scale *= dustScale;
            }
            int num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
        public static void CreateDustRing(Projectile projectile, int dustID, int range, int amount)
        {
            float maxDist = range;
            for (int i = 0; i < amount; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset - new Vector2(4, 4), 0, 0, dustID, 0, 0, 100)];
                dust.noGravity = true;
            }
        }
        public static void OutwardsCircleDust(Projectile projectile, int dustID, int numDusts, float vel, bool fromCenter = false, int dustAlpha = 0, float targetX = -1, float targetY = -1, bool randomiseVel = false, float dustScale = 1.5f, float dustFadeIn = 0)
        {
            Vector2 pos = projectile.Center;
            if (targetX != -1) pos = new Vector2(targetX, targetY);
            for (int i = 0; i < numDusts; i++)
            {
                 Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                //Vector2 position = (Vector2.One * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                Vector2 velocity = position - pos;
                Vector2 spawnPos = position + (fromCenter ? Vector2.Zero : velocity);
                Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustID, velocity.X * 2f, velocity.Y * 2f, dustAlpha, default(Color), dustScale)];
                dust.noGravity = true;
                dust.noLight = true;
                dust.velocity = Vector2.Normalize(velocity) * vel * (randomiseVel ? Main.rand.NextFloat(0.8f, 1.2f) : 1);
                dust.fadeIn = dustFadeIn;
            }
        }
        public static bool Home(Projectile projectile, float speed, float maxDist = 400)
        {
            float targetX = projectile.Center.X;
            float targetY = projectile.Center.Y;
            float closestEntity = maxDist;
            bool home = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(projectile.Center.X - nPC.Center.X) + Math.Abs(projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity)
                    {
                        closestEntity = dist;
                        targetX = nPC.Center.X;
                        targetY = nPC.Center.Y;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float goToX = targetX - projectile.Center.X;
                float goToY = targetY - projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                dist = speed / dist;
                goToX *= dist;
                goToY *= dist;
                projectile.velocity.X = (projectile.velocity.X * 20f + goToX) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + goToY) / 21f;
            }
            return home;
        }
        public static int CountProjectiles(int type, int ownerID = -1)
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type && (Main.projectile[i].owner == ownerID || ownerID == -1)) num++; // if ownerid = -1 then it will look for all projectiles
            }
            return num;
        }
        public static bool HasLeastTimeleft(int whoAmI)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Main.projectile[whoAmI].type && Main.projectile[whoAmI].timeLeft > Main.projectile[i].timeLeft) return false;
            }
            return true;
        }
    }
}
