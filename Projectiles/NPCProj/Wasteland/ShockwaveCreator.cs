using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class ShockwaveCreator : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.penetrate = 1;
            projectile.timeLeft = 180;

            projectile.tileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockwaver");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.16f;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 shockwavePosition = new Vector2(projectile.position.X, projectile.position.Y);
            Point shockwavePoint = shockwavePosition.ToTileCoordinates();
            Tile shockwaveTile = Framing.GetTileSafely((int)shockwavePoint.X, (int)shockwavePoint.Y);
            if (!Main.tileSolid[shockwaveTile.type] && shockwaveTile.active())
            {
                for (int i = 0; i < 3; i++)
                {
                    if (shockwaveTile.active())
                    {
                        if (!Main.tileSolid[shockwaveTile.type])
                        {
                            shockwavePosition -= new Vector2(0f, 16);
                            shockwavePoint = shockwavePosition.ToTileCoordinates();
                            shockwaveTile = Framing.GetTileSafely((int)shockwavePosition.X, (int)shockwavePosition.Y);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            Projectile.NewProjectile(shockwavePoint.X * 16 + 8, shockwavePoint.Y * 16 + 8, 0f, 0f, mod.ProjectileType("Shockwave"), 0, 0f, projectile.owner, 9f, 1f);
            Projectile.NewProjectile(shockwavePoint.X * 16 + 8, shockwavePoint.Y * 16 + 8, 0f, 0f, mod.ProjectileType("Shockwave"), 0, 0f, projectile.owner, 9f, -1f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 69);
        }
    }
}