using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class Shockwave : ModProjectile
    {
        public int whichSand = 0;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 20;

            projectile.penetrate = 1;
            projectile.timeLeft = 15;

            projectile.tileCollide = false;
            projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockwave");
            Main.projFrames[projectile.type] = 4;
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
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D projTexture = Main.projectileTexture[projectile.type];
            int width = projTexture.Width / 4;
            int height = projTexture.Height / Main.projFrames[projectile.type];
            int drawX = width * whichSand;

            Rectangle rectangle = new Rectangle(drawX, height * projectile.frame, width, height);
            Vector2 origin = rectangle.Size() / 4f - new Vector2(4,4); // idk why we need to subtract i messed it up

            Main.spriteBatch.Draw(projTexture, projectile.position - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            //projectile.ai[0]; is the strength of the shockwave
            //projectile.ai[1]; is the direction
            Point projPos = projectile.Center.ToTileCoordinates();
            Tile currTile = Framing.GetTileSafely(projPos.X, projPos.Y);
            Tile currTileUnder = Framing.GetTileSafely(projPos.X, projPos.Y + 1);

            Point nextUnderTilePoint = new Point(projPos.X + (int)projectile.ai[1], projPos.Y + 1);
            Tile nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);

            Point nextTilePoint = new Point(projPos.X + (int)projectile.ai[1], projPos.Y);
            Tile nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);

            if (currTileUnder.type == TileID.Sand)
            {
                whichSand = 0;
            }
            else if (currTileUnder.type == TileID.Ebonsand)
            {
                whichSand = 1;
            }
            else if (currTileUnder.type == TileID.Crimsand)
            {
                whichSand = 2;
            }
            else if (currTileUnder.type == TileID.Pearlsand)
            {
                whichSand = 3;
            }
            else
            {
                whichSand = 2;
            }
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
                if (projectile.localAI[0] == 5)
                {
                    Projectile.NewProjectile(nextTilePoint.X * 16 + 8, nextTilePoint.Y * 16 + 8, 0f, 0f, mod.ProjectileType("Shockwave"), 0, 0f, projectile.owner, projectile.ai[0] - 1, projectile.ai[1]);
                    for (int i = 0; i < 4; i++)
                    {
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(nextUnderTilePoint.X, nextUnderTilePoint.Y, nextUnderTile)];
                        dust.velocity.Y = Main.rand.NextFloat(-0.2f, -3f);
                    }
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.velocity.Y -= 15f;
        }
    }
}