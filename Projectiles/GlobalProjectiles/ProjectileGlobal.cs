using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Projectiles.GlobalProjectiles
{
    public class ProjectileGlobal : GlobalProjectile
    {
        public static int[] cantHit = {
            NPCID.TargetDummy
        };
        public bool dontScaleDamage = false;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.hostile)
            {
                if (MyWorld.awakenedMode && !dontScaleDamage)
                {
                    projectile.damage = (int)(projectile.damage * 1.5f);
                }
            }
        }
        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (target.FindBuffIndex(mod.BuffType("FrostShield")) != -1 && damage > 0 && projectile.active && !projectile.friendly && projectile.hostile)
            {
                if (Main.rand.Next(3) == 0)
                {
                    target.statLife += damage; // to stop damage
                    projectile.velocity.X = -projectile.velocity.X;
                    projectile.velocity.Y = -projectile.velocity.Y;
                }
            }

        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            if (Main.expertMode && dontScaleDamage && projectile.hostile)
            {
                damage = (int)(damage * 0.5f); // cut damage in half in expert 
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.GetGlobalNPC<NPCsGLOBAL>().impishCurse)
            {
                damage = (int)(damage * 1.75f);
            }
        }
        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (modPlayer.sonicArm && projectile.GetGlobalProjectile<EAProjectileType>().whip)
            {
                if (projectile.ai[0] == projectile.GetGlobalProjectile<EAProjectileType>().whipAliveTime / 2)
                {
                    Projectile.NewProjectile(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, mod.ProjectileType("WhipCrack"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/WhipCrack"));
                }
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (modPlayer.noDamageCounter > 0) modPlayer.noDamageCounter = 0;

            if (modPlayer.voidArmor)
            {
                foreach (int nPC in cantHit)
                {
                    if (target.type != nPC && !target.SpawnedFromStatue)
                    {
                        if (modPlayer.voidArmorHealCD <= 0)
                        {
                            float healAmount = Main.rand.Next(2, 8);
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("VoidHeal"), 0, 0f, projectile.owner, projectile.owner, healAmount); // ai 1 is how much it heals
                            modPlayer.voidArmorHealCD = Main.rand.Next(15,45);
                        }
                    }
                }
            }

            if (modPlayer.heartContainer && projectile.minion)
            {
                foreach (int nPC in cantHit)
                {
                    if (target.type != nPC && !target.SpawnedFromStatue)
                    {
                        if (Main.rand.Next(16) == 0)
                        {
                            float healAmount = Main.rand.Next(2, 8);
                            int num1 = projectile.owner;
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("HeartContainerHeal"), 0, 0f, projectile.owner, (float)num1, healAmount); // ai 1 is how much it heals
                        }
                    }
                }
            }

            if (modPlayer.venomSample)
            {
                target.AddBuff(BuffID.Venom, 120);
                target.AddBuff(BuffID.Poisoned, 120);
            }

            if (modPlayer.frozenGauntlet)
            {
                target.AddBuff(BuffID.Frostburn, 200);
            }

            if (modPlayer.dragonmailGreathelm && projectile.melee)
            {
                target.AddBuff(mod.BuffType("Dragonfire"), 300, false);
            }
            if (modPlayer.dragonmailHood && projectile.magic)
            {
                target.AddBuff(mod.BuffType("Dragonfire"), 300, false);
            }
            if (modPlayer.dragonmailVisage && projectile.ranged)
            {
                target.AddBuff(mod.BuffType("Dragonfire"), 300, false);

                if (Main.rand.Next(15) == 0)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 174, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                        Projectile.NewProjectile(projectile.position.X, projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, mod.ProjectileType("GreekFire"), projectile.damage / 2, projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f);
                    }
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                }
            }
            if (modPlayer.dragonmailMask && projectile.minion)
            {
                target.AddBuff(mod.BuffType("Dragonfire"), 300, false);
            }

            if (modPlayer.voidWalkerArmor == 1 && projectile.melee)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 300, false);
            }
            if (modPlayer.voidWalkerArmor == 2 && projectile.ranged)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 300, false);
            }
            if (modPlayer.voidWalkerArmor == 3 && projectile.magic)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 300, false);
            }
            if (modPlayer.voidWalkerArmor == 4 && projectile.minion)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 300, false);
            }

            if (modPlayer.neovirtuoBonus && Main.rand.Next(9) == 0 && projectile.type != mod.ProjectileType("NeovirtuoHoming"))
            {
                if (modPlayer.neovirtuoTimer <= 0)
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 84);
                    int neoDamage = 200;
                    int speed = 8;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speed, speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speed, -speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -speed, speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -speed, -speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    modPlayer.neovirtuoTimer = 15;
                }
            }

            if (modPlayer.immortalResolve)
            {
                if (crit && modPlayer.immortalResolveCooldown <= 0)
                {
                    int randLife = 0;
                    if (player.magicCrit < 10 && player.meleeCrit < 10 && player.rangedCrit < 10 && player.thrownCrit < 10)
                    {
                        randLife = Main.rand.Next(1, 18);
                    }
                    if (player.magicCrit >= 10 && player.meleeCrit >= 10 && player.rangedCrit >= 10 && player.thrownCrit >= 10 && player.magicCrit < 25 && player.meleeCrit < 25 && player.rangedCrit < 25 && player.thrownCrit < 25)
                    {
                        randLife = Main.rand.Next(1, 12);
                    }
                    if (player.magicCrit >= 25 && player.meleeCrit >= 25 && player.rangedCrit >= 25 && player.thrownCrit >= 25 && player.magicCrit < 75 && player.meleeCrit < 75 && player.rangedCrit < 75 && player.thrownCrit < 75)
                    {
                        randLife = Main.rand.Next(1, 8);
                    }
                    if (player.magicCrit >= 75 && player.meleeCrit >= 75 && player.rangedCrit >= 75 && player.thrownCrit >= 75)
                    {
                        randLife = Main.rand.Next(1, 5);
                    }
                    player.statLife += randLife;
                    player.HealEffect(randLife);
                    modPlayer.immortalResolveCooldown = 10;
                }
            }

            if (modPlayer.crowsArmor && modPlayer.crowsArmorCooldown <= 0)
            {
                float lightningSpeed = 8f;
                Vector2 spawnpoint = new Vector2(target.Center.X, target.Center.Y - 100);
                float rotation = -(float)Math.Atan2(spawnpoint.X - target.Center.Y, spawnpoint.X - target.Center.X);
                Vector2 speed = new Vector2((float)((Math.Cos(rotation) * lightningSpeed) * -1), (float)((Math.Sin(rotation) * lightningSpeed) * -1));

                Vector2 vector94 = new Vector2(speed.X, speed.Y);
                float ai = (float)Main.rand.Next(100);
                Vector2 vector95 = Vector2.Normalize(vector94) * 2f;
                Projectile.NewProjectile(spawnpoint.X, spawnpoint.Y, vector95.X, vector95.Y, mod.ProjectileType("CrowLightning"), 100, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                Projectile.NewProjectile(spawnpoint.X, spawnpoint.Y, 0f, 0f, mod.ProjectileType("CrowStorm"), 0, 0f, Main.myPlayer);
                modPlayer.crowsArmorCooldown = 30;
            }

            if (modPlayer.cosmicGlass && crit && modPlayer.cosmicGlassCD <= 0 && projectile.type != mod.ProjectileType("ChargeRifleHalf"))
            {
                if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(player.Center.Y - target.Center.Y, player.Center.X - target.Center.X);

                    Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speed.X, speed.Y, mod.ProjectileType("ChargeRifleHalf"), 30, 3f, projectile.owner, 0f);
                    modPlayer.cosmicGlassCD = 3;
                }
            }
            if (modPlayer.sufferWithMe && Main.rand.Next(4) == 0)
            {
                target.AddBuff(mod.BuffType("ChaosBurn"), 300, false);
            }
            int strikeChance = 10;
            if (NPC.downedBoss3) strikeChance = 7;
            if (Main.hardMode) strikeChance = 5;
            if (NPC.downedPlantBoss) strikeChance = 4;
            if (NPC.downedMoonlord) strikeChance = 2;
            if (modPlayer.strangeUkulele && Main.rand.Next(strikeChance) == 0 && projectile.type != mod.ProjectileType("UkuleleArc"))
            {
                List<int> availableNPCs = new List<int>();
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.active && !other.friendly && other.damage > 0 && !other.dontTakeDamage && Vector2.Distance(other.Center, player.Center) < 300)
                    {
                        availableNPCs.Add(other.whoAmI);
                    }
                }
                if (availableNPCs.Count > 0)
                {
                    NPC arcTarget = Main.npc[availableNPCs[Main.rand.Next(availableNPCs.Count)]];
                    if (arcTarget.active && !arcTarget.friendly && arcTarget.damage > 0 && !arcTarget.dontTakeDamage)
                    {
                        Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"));

                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(player.Center.Y - target.Center.Y, player.Center.X - target.Center.X);
                        rotation += MathHelper.ToRadians(Main.rand.Next(-60, 60));
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, speed.X, speed.Y, mod.ProjectileType("UkuleleArc"), (int)(projectile.damage * 0.5f), 3f, player.whoAmI, arcTarget.whoAmI);
                    }
                }
            }
        }


        public static void Explosion(Projectile projectile, int[] dustIDs, int damage, string damageType = "normal")
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            Projectile exp = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), damage, projectile.knockBack, projectile.owner, 0f, 0f)];
            exp.melee = damageType == "melee" ? true : false;
            exp.ranged = damageType == "ranged" ? true : false;
            exp.thrown = damageType == "thrown" ? true : false;
            exp.magic = damageType == "magic" ? true : false;
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                int dustID = dustIDs[Main.rand.Next(dustIDs.Length)];
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                int dustID2 = dustIDs[Main.rand.Next(dustIDs.Length)];
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
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
        public static void HostileExplosion(Projectile projectile, int[] dustIDs, int damage)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionHostile"), damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                int dustID = dustIDs[Main.rand.Next(dustIDs.Length)];
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                int dustID2 = dustIDs[Main.rand.Next(dustIDs.Length)];
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
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
        public static void Home(Projectile projectile, float speed)
        {
            float targetX = projectile.Center.X;
            float targetY = projectile.Center.Y;
            float closestEntity = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
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
                return;
            }
        }
    }
}
