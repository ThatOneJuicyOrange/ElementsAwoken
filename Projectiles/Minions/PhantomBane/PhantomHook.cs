using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ElementsAwoken.Projectiles.Minions.PhantomBane;

namespace ElementsAwoken.Projectiles.Minions.PhantomBane
{
    public class PhantomHook : ModProjectile
    {
        public bool hasGivenBuff = false;
        public override void SetDefaults()
        {
            projectile.width = 56;
            projectile.height = 56;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0.5f;
            projectile.light = 2f;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 388;
            projectile.aiStyle = 66;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantom Hook");
            //Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
        }

        private static int[] minionToType = new int[6];
        private int minion;

        public PhantomHook() : this(1) { }

        public PhantomHook(int minion)
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
                for (int k = 0; k <= 3; k++)
                {
                    ModProjectile next = new PhantomHook(k);
                    mod.AddProjectile(name + k, next);
                    minionToType[k] = next.projectile.type;
                }
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            bool flag64 = projectile.type == mod.ProjectileType("PhantomHook");

            if (!hasGivenBuff)
            {
                player.AddBuff(mod.BuffType("PhantomHookBuff"), 3600);

                hasGivenBuff = true;
            }
            if (player.dead)
            {
                modPlayer.phantomHook = false;
            }
            if (modPlayer.phantomHook)
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

            ProjectileUtils.PushOtherEntities(projectile);

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