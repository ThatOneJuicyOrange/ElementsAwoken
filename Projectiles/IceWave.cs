using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class IceWave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 40;

            projectile.penetrate = -1;
            projectile.timeLeft = 20;

            projectile.tileCollide = false;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockwave");
            Main.projFrames[projectile.type] = 5;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.direction = (int)projectile.ai[1];
            projectile.spriteDirection = (int)projectile.ai[1];
            if (projectile.frame < 3)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 3)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                }
            }
            return true;
        }
        public override void AI()
        {
            //projectile.ai[0]; is the strength of the shockwave
            //projectile.ai[1]; is the direction
            Point projPos = projectile.Center.ToTileCoordinates();

            Point nextUnderTilePoint = new Point(projPos.X + (int)projectile.ai[1], projPos.Y + 1);
            Tile nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);

            Point nextTilePoint = new Point(projPos.X + (int)projectile.ai[1], projPos.Y);
            Tile nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);

            if (projectile.ai[0] > 0)
            {
                if (Main.tileSolid[nextTile.type] && nextTile.active() && !TileID.Sets.Platforms[nextTile.type])
                {
                    nextTilePoint.Y -= 1;
                    nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);
                    if (Main.tileSolid[nextTile.type] && nextTile.active() && !TileID.Sets.Platforms[nextTile.type])
                    {
                        return;
                    }
                }
                if ((!Main.tileSolid[nextUnderTile.type] && nextUnderTile.active()) || !nextUnderTile.active())
                {
                    nextUnderTilePoint.Y += 1;
                    nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);
                    nextTilePoint.Y += 1;
                    nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);
                    if ((!Main.tileSolid[nextUnderTile.type] && nextUnderTile.active()) || !nextUnderTile.active())
                    {
                        return;
                    }
                }
                projectile.localAI[0]++;
                if (projectile.localAI[0] == 3)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(nextTilePoint.X * 16 + 8, nextTilePoint.Y * 16, 0f, 0f, projectile.type, projectile.damage, 0f, projectile.owner, projectile.ai[0] - 1, projectile.ai[1])];
                    for (int i = 0; i < 4; i++)
                    {
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(nextUnderTilePoint.X, nextUnderTilePoint.Y, nextUnderTile)];
                        dust.velocity.Y = Main.rand.NextFloat(-0.2f, -3f);
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= Main.rand.NextFloat(5f,12f) * target.knockBackResist;
        }
    }
}