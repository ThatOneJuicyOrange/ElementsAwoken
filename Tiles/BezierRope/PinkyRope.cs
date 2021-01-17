using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ElementsAwoken.Tiles.BezierRope
{
    public class PinkyRope : BezierRopeBase
    {
        public PinkyRope() : base(8) { }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Main.tileFrameImportant[Type] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Tile endTile = Framing.GetTileSafely(i, j - 1);
            if (Main.tile[i, j].frameX != 0 && Main.tile[i, j].frameY != 0) endTile = Framing.GetTileSafely(Main.tile[i, j].frameX, Main.tile[i, j].frameY - 1);
            if (!Framing.GetTileSafely(i, j - 1).active() || !endTile.active()) WorldGen.KillTile(i, j);
        }
        public override void ExtraDetails(SpriteBatch spriteBatch, List<Vector2> curvePoints, Vector2 start, Vector2 end)
        {
            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/PinkyRopeBlobs");

            int pointsCount = curvePoints.Count;
            for (int k = 0; k < pointsCount; k++)
            {
                if (k % 2 == 0) continue;
                Vector2 prevPos = curvePoints[k];
                if (k != pointsCount - 1) prevPos = curvePoints[k + 1];
                else prevPos = curvePoints[k - 1];
                Vector2 pos = curvePoints[k];

                Vector2 direction = prevPos - pos;
                Vector2 next = direction;
                next.Normalize();
                if (k == pointsCount - 1) direction = (pos + next) - pos;
                SpriteEffects effects = direction.X < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen) zero = Vector2.Zero;

                Color color = Lighting.GetColor((int)(pos.X / 16), (int)(pos.Y / 16));

                UnifiedRandom random = new UnifiedRandom((int)pos.X);
                float rot = direction.ToRotation() + 1.57f;


                Rectangle frame = new Rectangle(0, 12 * random.Next(0, 3), 16, 12);
                spriteBatch.Draw(tex, pos - Main.screenPosition + zero, frame, color, rot, frame.Size() / 2, 1f, effects, 0f);
            }
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (ModContent.GetInstance<Config>().debugMode) return true;
            return false;
        }
    }
}