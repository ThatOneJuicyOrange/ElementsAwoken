using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ElementsAwoken.Projectiles.Minions.EnchantedStaff;

namespace ElementsAwoken.Projectiles.Minions.EnchantedStaff
{
    public class EnchantedTrio : ModProjectile
    {
        public bool hasGivenBuff = false;

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0.33f;
            projectile.light = 0.5f;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 388;
            projectile.aiStyle = 66;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Trio");
            //Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 9;
        }

        private static int[] minionToType = new int[6];
        private int minion;

        public EnchantedTrio() : this(1) { }

        public EnchantedTrio(int minion)
        {
            this.minion = minion;
        }

        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override bool Autoload(ref string name)
        {
            if (mod.Properties.Autoload)
            {
                for (int k = 0; k <= 2; k++)
                {
                    ModProjectile next = new EnchantedTrio(k);
                    mod.AddProjectile(name + k, next);
                    minionToType[k] = next.projectile.type;
                }
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            bool flag64 = projectile.type == mod.ProjectileType("EnchantedTrio");

            if (!hasGivenBuff)
            {
                player.AddBuff(mod.BuffType("EnchantedTrio"), 3600);

                hasGivenBuff = true;
            }
            if (player.dead)
            {
                modPlayer.enchantedTrio = false;
            }
            if (modPlayer.enchantedTrio)
            {
                projectile.timeLeft = 2;
            }

            projectile.rotation += projectile.velocity.X * 0.04f;
            if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner && minion == 0)
            {
                for (int k = 1; k <= 5; k++)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity, minionToType[k], projectile.damage, projectile.knockBack, projectile.owner);
                }
                projectile.localAI[0] = 1f;
            }
            projectile.frame = minion;

            for (int k = 0; k < 1000; k++)
            {
                Projectile other = Main.projectile[k];
                if (k != projectile.whoAmI && other.active && other.owner == projectile.owner && minionToType.Contains(other.type) && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    const float pushAway = 0.05f;
                    if (projectile.position.X < other.position.X)
                    {
                        projectile.velocity.X -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.X += pushAway;
                    }
                    if (projectile.position.Y < other.position.Y)
                    {
                        projectile.velocity.Y -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.Y += pushAway;
                    }
                }
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
    }
}