using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.Tiles.BezierRope
{
    // the chain is too large so the tile goes out of render and despawns which is why we need this. 
    public class ChainRopeNPC : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public Texture2D tex = null;
        public Tile parent = null;
        private float texSize
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float frameX
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float frameY
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 16;
            npc.height = 16;

            npc.aiStyle = -1;

            npc.lifeMax = 1;

            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.immortal = true;
            npc.friendly = true;
            npc.noGravity = true;
            npc.behindTiles = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Tile t = parent;
            if (!t.active())
            {
                npc.active = false;
                return;
            }
            if (tex != null)
            {
                Vector2 endPoint = new Vector2((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16) * 16;
                endPoint = new Vector2((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y);
                if (new Vector2(frameX, frameY) != Vector2.Zero) endPoint = new Vector2(frameX * 16 + 8, frameY * 16);
                BezierDraw((int)npc.position.X / 16, (int)npc.position.Y / 16, spriteBatch, endPoint, (int)texSize);
            }
        }
        public override bool CheckActive()
        {
            if (Vector2.Distance(Main.LocalPlayer.position, npc.position) < 3000 || Vector2.Distance(Main.LocalPlayer.position, new Vector2(frameX * 16, frameY * 16)) < 3000) return false;
            return false;
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
            Texture2D tex = Main.tileTexture[parent.type];
            // find textures needed
            int num = (int)(dist / texSize) + 1;
            curve = new BezierCurve(points);
            List<Vector2> points3 = curve.GetPoints(num);
            if (points3[points3.Count - 1] != endPoint) points3.Add(endPoint);

            int pointsCount = points3.Count;
            for (int k = 0; k < pointsCount; k++)
            {
                Vector2 prevPos = points3[k];
                if (k != pointsCount - 1) prevPos = points3[k + 1];
                else prevPos = points3[k - 1];
                Vector2 pos = points3[k];

                Vector2 direction = prevPos - pos;
                Vector2 next = direction;
                next.Normalize();
                if (k == pointsCount - 1) direction = (pos + next) - pos;
                float rot = direction.ToRotation() + 1.57f;
                SpriteEffects effects = direction.X < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                /*Dust dust = Main.dust[Dust.NewDust(pos, 1, 1, 6)];
                dust.noGravity = true;
                dust.velocity *= 0;
                dust.scale *= 2f;*/

                Color color = Lighting.GetColor((int)(pos.X / 16), (int)(pos.Y / 16));
                spriteBatch.Draw(tex, pos - Main.screenPosition, null, color, rot, tex.Size() / 2, 1f, effects, 0f);
            }
            //ExtraDetails(spriteBatch, points3, p1, p3);
        }
    }
}
