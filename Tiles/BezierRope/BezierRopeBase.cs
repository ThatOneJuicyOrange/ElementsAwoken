using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.BezierRope
{
    public abstract class BezierRopeBase : ModTile
    {
        private readonly int _texSize;
        public BezierRopeBase(int texSize)
        {
            _texSize = texSize;
        }
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;

        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];

            if (ModContent.GetInstance<Config>().debugMode && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<Items.Placeable.BezierRope.BezierRopeTest>())
            {
                for (int k = 0; k < 16; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 6)];
                    dust.noGravity = true;
                    dust.velocity *= 0;
                }
                for (int k = 0; k < 16; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(t.frameX * 16, t.frameY * 16), 16, 16, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.velocity *= 0;
                }
            }
            Vector2 endPoint = new Vector2((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16) * 16;
            endPoint = new Vector2((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y) ;
            if (new Vector2(t.frameX, t.frameY) != Vector2.Zero) endPoint = new Vector2(t.frameX * 16 + 8, t.frameY * 16);
            BezierDraw(i, j, spriteBatch, endPoint, _texSize);
        }
        public virtual void ExtraDetails(SpriteBatch spriteBatch, List<Vector2> curvePoints, Vector2 start, Vector2 end)
        {

        }
        private void BezierDraw(int i, int j, SpriteBatch spriteBatch, Vector2 endPoint, int texSize = 16)
        {
            Tile t = Main.tile[i, j];
            Vector2 p1 = new Vector2(i * 16 + 8, j * 16);
            Vector2 p3 = endPoint;
            float offset = 40f * (float)((Math.Sin((float)(MyWorld.generalTimer + i * 10f) / 50f) + 1f) / 2f);
            Vector2 p2 = new Vector2((p1.X + p3.X) / 2f, (p1.Y + p3.Y) / 2f + 50f + offset);
            Vector2 p2_ = new Vector2((p1.X + p3.X) / 2f, (p1.Y + p3.Y) / 2f + 50f + 25f); // to stop adding and subtracting more textures
            Vector2[] pointsCheck = { p1, p2_, p3 };
            Vector2[] points = { p1, p2, p3 };
            BezierCurve curve = new BezierCurve(pointsCheck);
            // find length of rope
            List<Vector2> points2 = curve.GetPoints(50);
            float dist = 0;
            for (int k = 0; k < points2.Count; k++)
            {
                if (k != 0) dist += Vector2.Distance(points2[k], points2[k - 1]);
            }
            Texture2D tex = Main.tileTexture[Type];
            // find textures needed
            int num = (int)(dist / texSize) + 1;
            curve = new BezierCurve(points);
            List<Vector2> points3 = curve.GetPoints(num);
            if (points3[points3.Count -1] != endPoint) points3.Add(endPoint);

            int pointsCount = points3.Count;
            for (int k = 0; k < pointsCount; k++)
            {
                Vector2 prevPos = points3[k];
                if (k != pointsCount - 1) prevPos = points3[k + 1];
                else prevPos = points3[k - 1];
                Vector2 pos = points3[k];

                Vector2 direction = prevPos - pos ;
                Vector2 next = direction;
                next.Normalize();
                if (k == pointsCount - 1) direction = (pos + next) - pos;
                float rot = direction.ToRotation() + 1.57f;
                SpriteEffects effects = direction.X < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                /*Dust dust = Main.dust[Dust.NewDust(pos, 1, 1, 6)];
                dust.noGravity = true;
                dust.velocity *= 0;
                dust.scale *= 2f;*/

                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen) zero = Vector2.Zero;

                Color color = Lighting.GetColor((int)(pos.X / 16), (int)(pos.Y / 16));
                spriteBatch.Draw(tex, pos - Main.screenPosition + zero, null, color, rot, tex.Size() / 2, 1f, effects, 0f);
            }
            ExtraDetails(spriteBatch, points3, p1, p3);
        }
    }
}