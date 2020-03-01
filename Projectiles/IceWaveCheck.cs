using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class IceWaveCheck : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.tileCollide = false;

            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Wave Check");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Bottom;
            if (player.itemAnimation <= 5)
            {
                Point tilePoint = new Point((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16);
                if (Framing.GetTileSafely(tilePoint.X, tilePoint.Y).active())
                {
                    Main.PlaySound(SoundID.Item69, projectile.Center);
                    Projectile.NewProjectile(tilePoint.X * 16 + 8, tilePoint.Y * 16 - 16, 0f, 0f, ModContent.ProjectileType<IceWave>(), projectile.damage, 0f, Main.myPlayer, 24f, player.direction);
                }
                projectile.Kill();
            }
        }
    }
}