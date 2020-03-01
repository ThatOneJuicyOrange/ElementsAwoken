using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{   
    public class CorroderMinion : FloatyMinionINFO
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;

            projectile.width = 24;
            projectile.height = 30;

            Main.projFrames[projectile.type] = 3;

            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            Main.projPet[projectile.type] = true;

            projectile.minionSlots = 0.75f;
            projectile.penetrate = 1;
            projectile.timeLeft = 18000;

            inertia = 15f;
            sitType = 1;
            shootType = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Spirit");
        }
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("CorroderBuff"), 3600);
            if (player.dead)
            {
                modPlayer.corroder = false;
            }
            if (modPlayer.corroder)
            {
                projectile.timeLeft = 2;
            }
        }

        public override void Shoot(Vector2 targetPos)
        {
            Vector2 shootVel = targetPos - projectile.Center;
            if (shootVel == Vector2.Zero)
            {
                shootVel = new Vector2(0f, 1f);
            }
            shootVel.Normalize();
            shootVel *= 12f;
            Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType("CorroderSpit"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f)];
            proj.timeLeft = 300;
            proj.netUpdate = true;
            projectile.netUpdate = true;
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;
            }
        }
    }
}