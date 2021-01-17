using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Bosses
{
    public class LavaSquidTentacle : ModNPC
    {
        private float parentID
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float tentNum
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float pulseAI
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiState
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        private float pulseMax = 45;
        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 20;
            
            npc.aiStyle = -1;

            npc.damage = 120;
            npc.defense = 12;
            npc.lifeMax = 1200;
            npc.knockBackResist = 0.05f;

            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.noGravity = true;

            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;

            banner = npc.type;
            bannerItem = ModContent.ItemType<Items.Banners.InfernoSpiritBanner>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volkraken Tentacle");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void FindFrame(int frameHeight)
        {

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            NPC parent = Main.npc[(int)parentID];
            Vector2 p1 = new Vector2(parent.Center.X - 10 + 2 * tentNum, parent.Bottom.Y - 20);
            Vector2 p3 = new Vector2(npc.Center.X, npc.Center.Y);
            Vector2 p2 = new Vector2((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2 + 200);
            Vector2[] points = { p1, p2, p3 };
            BezierCurve curve = new BezierCurve(points);
            List<Vector2> points2 = curve.GetPoints(50);
            float dist = 0;
            for (int k = 0; k < points2.Count; k++)
            {
                if (k != 0) dist += Vector2.Distance(points2[k], points2[k - 1]);
            }
            Texture2D segmentTex = ModContent.GetTexture("ElementsAwoken/NPCs/VolcanicPlateau/Bosses/LavaSquidTentacle");
            int num = (int)(dist / 8);
            List<Vector2> points3 = curve.GetPoints(num);

            Vector2 finalPos = Vector2.Zero;
            int pointsCount = points3.Count;
            for (int k = 0; k < pointsCount; k++)
            {
                for (int l = 0; l < 2; l++) // interpolate to cover gaps
                {
                    Vector2 prevPos = points3[k];
                    if (k != 0) prevPos = points3[k - 1];
                    Vector2 pos = points3[k];
                    if (k != 0) pos = (points3[k - l] + pos) / 2;

                    Vector2 direction = prevPos - pos;
                    float rot = direction.ToRotation() + 1.57f;

                    Color color = Lighting.GetColor((int)(pos.X / 16), (int)(pos.Y / 16));
                    Rectangle frame = new Rectangle(0, 20, 16, 8);
                    if (k == 0) frame = new Rectangle(0, 0, 16, 18);
                    if (k == pointsCount - 3) frame = new Rectangle(0, 30, 16, 8);
                    if (k == pointsCount - 2) frame = new Rectangle(0, 40, 16, 8);

                    if (k == pointsCount - 2)
                        finalPos = pos;
                    float scale = 1f;
                    if (tentNum == 0 || tentNum == 9)
                    {
                        if (k > pointsCount - 7)
                        {
                            scale = MathHelper.Lerp(0.5f, 1f, (float)(pointsCount - k) / 7);
                        }
                    }
                    if (k != pointsCount - 1) spriteBatch.Draw(segmentTex, pos - Main.screenPosition, frame, color, rot, frame.Size() / 2, scale, SpriteEffects.None, 0f);
                }
            }
            Vector2 direction2 = finalPos - points[2];
            float rot2 = direction2.ToRotation() + 1.57f;
            if (tentNum == 0 || tentNum == 9)
            {
                Rectangle frame2 = new Rectangle(0, 60, 16, 28);
                spriteBatch.Draw(segmentTex, points[2] - Main.screenPosition, frame2, Color.White, rot2, frame2.Size() / 2, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                Rectangle frame2 = new Rectangle(0, 50, 16, 8);
                spriteBatch.Draw(segmentTex, points[2] - Main.screenPosition, frame2, Color.White, rot2, frame2.Size() / 2, 1f, SpriteEffects.None, 0f);
            }
            return false;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            NPC parent = Main.npc[(int)parentID];
            if (!parent.active) npc.active = false;
            if (tentNum != 0 && tentNum != 9)
            {
                npc.dontTakeDamage = true;
                npc.immortal = true;
            }
            EAUtils.PushOtherEntities(npc);

            if (Vector2.Distance(parent.Center, npc.Center) > 500)
            {
                float moveSpeed = 3f;
                Vector2 toTarget = new Vector2(parent.Center.X - npc.Center.X, parent.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            if (tentNum == 0 || tentNum == 9)
            {
                /* float moveSpeed = 0.2f;
                 Vector2 toTarget = new Vector2(parent.Center.X - npc.Center.X + 200 * (tentNum == 0 ? -1 :1), parent.Center.Y - npc.Center.Y - 100);
                 toTarget.Normalize();
                 npc.velocity += toTarget * moveSpeed;*/
                 Vector2 goTo = new Vector2(parent.Center.X + 200 * (tentNum == 0 ? -1 : 1), parent.Center.Y - 100);
                FlyTo(goTo, 0.01f, 3f);
            }
        }
        private void FlyTo(Vector2 location, float acceleration, float speed)
        {
            float targetX = location.X - npc.Center.X;
            float targetY = location.Y - npc.Center.Y;
            float targetPos = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            targetPos = speed / targetPos;
            targetX *= targetPos;
            targetY *= targetPos;
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0f && targetX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                }
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0f && targetX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                }
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                }
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 400, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.Pyroplasm>(), Main.rand.Next(1, 4));
        }
    }
}