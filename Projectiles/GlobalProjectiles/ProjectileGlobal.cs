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
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Buffs;

namespace ElementsAwoken.Projectiles.GlobalProjectiles
{
    public class ProjectileGlobal : GlobalProjectile
    {
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
            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.GreenLaser)
                {
                    projectile.penetrate = 2;
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
            /*if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.LastPrismLaser)
                {
                    Projectile laser = Main.projectile[(int)projectile.ai[1]];
                    if (laser.ai[0] >= 180) damage = (int)(damage * 0.5f);
                }
            }*/
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
            if (projectile.melee && modPlayer.eaMagmaStone && !projectile.noEnchantments)
            {
                bool makeDust = Main.rand.Next(3) == 0;
                if (ModContent.GetInstance<Config>().lowDust) makeDust = Main.rand.Next(8) == 0;
                if (makeDust)
                {
                    int num70 = Dust.NewDust(new Vector2(projectile.position.X - 4f, projectile.position.Y - 4f), projectile.width + 8, projectile.height + 8, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num70].scale = 1.5f;
                    }
                    Main.dust[num70].noGravity = true;
                    Dust expr_B54_cp_0 = Main.dust[num70];
                    expr_B54_cp_0.velocity.X = expr_B54_cp_0.velocity.X * 2f;
                    Dust expr_B72_cp_0 = Main.dust[num70];
                    expr_B72_cp_0.velocity.Y = expr_B72_cp_0.velocity.Y * 2f;
                }
            }
            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.LastPrismLaser)
                {
                    Projectile laser = Main.projectile[(int)projectile.ai[1]];
                    if (laser.ai[0] >= 180) projectile.damage = (int)(projectile.damage * 0.5f);
                }
            }
            if (projectile.type == ProjectileID.LastPrism && modPlayer.prismPolish)
            {
                if (projectile.ai[0] < 180) projectile.ai[0]++;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.LastPrismLaser)
                {
                    target.immune[projectile.owner] = 20;
                }
            }

            if (modPlayer.noDamageCounter > 0) modPlayer.noDamageCounter = 0;

            if (projectile.friendly)
            {
                if (modPlayer.voidArmor)
                {
                    if (target.CanBeChasedBy(this) && !target.SpawnedFromStatue)
                    {
                        if (modPlayer.voidArmorHealCD <= 0)
                        {
                            float healAmount = Main.rand.Next(2, 8);
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("VoidHeal"), 0, 0f, projectile.owner, projectile.owner, healAmount); // ai 1 is how much it heals
                            modPlayer.voidArmorHealCD = Main.rand.Next(15, 45);
                        }
                    }
                }

                if (modPlayer.heartContainer && projectile.minion)
                {
                    if (target.CanBeChasedBy(this) && !target.SpawnedFromStatue)
                    {
                        if (Main.rand.Next(16) == 0)
                        {
                            float healAmount = Main.rand.Next(2, 8);
                            int num1 = projectile.owner;
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("HeartContainerHeal"), 0, 0f, projectile.owner, (float)num1, healAmount); // ai 1 is how much it heals
                        }
                    }
                }
                if (modPlayer.venomSample || modPlayer.vilePower)
                {
                    target.AddBuff(BuffID.Venom, 300);
                    target.AddBuff(BuffID.Poisoned, 300); 
                    for (int k = 0; k < 3; k++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 171, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
                    }
                }

                if (modPlayer.frozenGauntlet)
                {
                    target.AddBuff(BuffID.Frostburn, 200);
                }
                if (modPlayer.eaMagmaStone)
                {
                    if (Main.rand.Next(7) == 0)
                    {
                        target.AddBuff(BuffID.OnFire, 360);
                    }
                    else if (Main.rand.Next(3) == 0)
                    {
                        target.AddBuff(BuffID.OnFire, 120);
                    }
                    else
                    {
                        target.AddBuff(BuffID.OnFire, 60);
                    }
                }
                if (modPlayer.dragonmailGreathelm && projectile.melee)
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
                }
                if (modPlayer.dragonmailHood && projectile.magic)
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
                }
                if (modPlayer.dragonmailVisage && projectile.ranged)
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);

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
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
                }

                if (modPlayer.voidWalkerArmor == 1 && projectile.melee)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 2 && projectile.ranged)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 3 && projectile.magic)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 4 && projectile.minion)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.abyssalRage > 0)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 420);
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
                        int randLife = Main.rand.Next(1, 5);
                        if ((player.magicCrit < 10 && projectile.magic) && (player.meleeCrit < 10 && projectile.melee) && (player.rangedCrit < 10 && projectile.ranged) && (player.thrownCrit < 10 && projectile.thrown))
                        {
                            randLife = Main.rand.Next(1, 18);
                        }
                        if ((player.magicCrit >= 10 && player.magicCrit < 25 && projectile.magic) && 
                            (player.meleeCrit >= 10 && player.meleeCrit < 25 && projectile.melee) && 
                            (player.rangedCrit >= 10 && player.rangedCrit < 25 && projectile.ranged) && 
                            (player.thrownCrit >= 10 && player.thrownCrit < 25 && projectile.thrown))
                        {
                            randLife = Main.rand.Next(1, 12);
                        }
                        if ((player.magicCrit >= 25 && player.magicCrit < 75 && projectile.magic) &&
                           (player.meleeCrit >= 25 && player.meleeCrit < 75 && projectile.melee) &&
                           (player.rangedCrit >= 25 && player.rangedCrit < 75 && projectile.ranged) &&
                           (player.thrownCrit >= 25 && player.thrownCrit < 75 && projectile.thrown))
                        {
                            randLife = Main.rand.Next(1, 8);
                        }
                        if ((player.magicCrit >= 75 && projectile.magic) && (player.meleeCrit >= 75 && projectile.melee) && (player.rangedCrit >= 75 && projectile.ranged) && (player.thrownCrit >= 75 && projectile.thrown))
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
                if (modPlayer.bleedingHeart)
                {
                    if (target.life <= 0 && playerUtils.enemiesKilledLast10Secs >= 4 && !target.SpawnedFromStatue)
                    {
                        player.AddBuff(ModContent.BuffType<Bloodbath>(), 600, false);
                    }
                }
                if (modPlayer.putridArmour)
                {
                    if (projectile.minion && projectile.minionSlots != 0 && Main.rand.NextBool(3))
                    {
                        target.AddBuff(ModContent.BuffType<FastPoison>(), 60, false);
                    }
                }
            }
        }



    }
}
