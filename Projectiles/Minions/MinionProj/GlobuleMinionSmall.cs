using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions.MinionProj
{
    public class GlobuleMinionSmall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Minions/GlobuleMinion"; } }
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;

            projectile.friendly = true;
            projectile.minion = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;

            projectile.scale = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Globule");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.X * 0.075f;
            Player player = Main.player[projectile.owner];
            ProjectileUtils.PushOtherEntities(projectile);
            if (!ProjectileUtils.Home(projectile, 8f, 800f))
            {
                Vector2 toTarget = new Vector2(player.Center.X - projectile.Center.X, player.Center.Y - projectile.Center.Y);
                toTarget.Normalize();
                projectile.velocity += toTarget * 0.25f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath1, projectile.position);
            for (int p = 1; p <= 2; p++)
            {
                float strength = p * 2f;
                int numDusts = p * 10;
                ProjectileUtils.OutwardsCircleDust(projectile, DustID.PinkFlame, numDusts, strength, randomiseVel: true);
            }
        }
    }
}