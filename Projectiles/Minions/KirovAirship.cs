using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{   
    public class KirovAirship : MinionINFO
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.minion = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.ignoreWater = true;

            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;

            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            inertia = 50f;
            shoot = ProjectileID.RocketI;
            shootCool = 45;
            shootSpeed = 12f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kirov Airship");
        }
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("KirovAirshipBuff"), 3600);
            if (player.dead)
            {
                modPlayer.kirovAirship = false;
            }
            if (modPlayer.kirovAirship)
            {
                projectile.timeLeft = 2;
            }
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