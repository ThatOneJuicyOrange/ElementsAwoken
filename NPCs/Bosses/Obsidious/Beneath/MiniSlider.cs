using ElementsAwoken.Projectiles.NPCProj.Obsidious;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Obsidious.Beneath
{
    public class MiniSlider : Liftable.HeldNPCBase
    {
        private float aiState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer2
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiTimer3
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Slider");
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;

            npc.aiStyle = -1;

            npc.alpha = 255;
            npc.lifeMax = 500;
            npc.damage = 50;
            npc.defense = 75;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.gfxOffY = -4;

            npc.HitSound = SoundID.DD2_SkeletonHurt;
            npc.DeathSound = SoundID.DD2_LightningBugDeath;

            npc.GetGlobalNPC<NPCsGLOBAL>().liftable = false;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * 2f);
            npc.lifeMax = (int)(npc.lifeMax * 2f);
            if (MyWorld.awakenedMode)
            {
                npc.damage = (int)(npc.damage * 1.25f);
                npc.lifeMax = (int)(npc.lifeMax * 1.25f);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (aiState != 999)
            {
                Texture2D texture = mod.GetTexture("NPCs/Bosses/Obsidious/Beneath/" + GetType().Name + "_Glow");
                Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
                Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White * (1 - ((float)npc.alpha / 255)), npc.rotation, origin, npc.scale, effects, 0.0f);
            }
        }
        public override void ExtraAI(bool held, Player player)
        {
            npc.TargetClosest(false);
            Player target = Main.player[npc.target];
            EAUtils.PushOtherEntities(npc, new List<int> { ModContent.NPCType<MediumSlider>() }, 0.2f);
            float slideSpeed = 7;
            if (aiState == 0)
            {
                Point playerWorld = (player.Center / 16).ToPoint();
                Rectangle arena = new Rectangle(EAWorldGen.obsidiousTempleLoc.X + 6, EAWorldGen.obsidiousTempleLoc.Y + 66, 137, 33);
                if (arena.Contains(playerWorld)) aiState++;
            }
            else if (aiState == 1)
            {
                if (npc.alpha > 0) npc.alpha -= 10;
                else
                {
                    npc.alpha = 0;
                    aiTimer++;
                    float height = MathHelper.Lerp(-40, -150, MathHelper.Clamp(MathHelper.Distance(npc.Center.X, target.Center.X) / 500, 0, 1));
                    Vector2 targetPos = new Vector2(target.Center.X, target.Center.Y + height);
                    if (Vector2.Distance(targetPos, npc.Center) < 10) npc.velocity = Vector2.Zero;
                    else
                    {
                        Vector2 toTarget = new Vector2(targetPos.X - npc.Center.X, targetPos.Y - npc.Center.Y);
                        toTarget.Normalize();
                        float maxSpeed = 2;
                        if (npc.velocity.X > -maxSpeed && toTarget.X < 0 || npc.velocity.X < maxSpeed && toTarget.X > 0) npc.velocity.X += toTarget.X * 0.2f;
                        if (npc.velocity.Y > -maxSpeed && toTarget.Y < 0 || npc.velocity.Y < maxSpeed && toTarget.Y > 0) npc.velocity.Y += toTarget.Y * 0.2f;
                    }
                    if (aiTimer > 300)
                    {
                        aiState++;
                        aiTimer = 0;
                    }
                }
            }
            else if (aiState == 2)
            {
                Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, npc.position);
                if (Main.rand.NextBool() || MathHelper.Distance(npc.Center.X, target.Center.X) > 300)
                {
                    if (target.Center.X > npc.Center.X) aiState = 5;
                    else aiState = 6;
                }
                else
                {
                    if (target.Center.Y > npc.Center.Y) aiState = 3;
                    else aiState = 4;
                }
                npc.netUpdate = true;
            }
            else if (aiState == 3)
            {
                Slide(target, slideSpeed, 0, 1);    // slide down from above
            }   
            else if (aiState == 4)
            {
                Slide(target, slideSpeed, 0, -1);   // slide up from below
            }
            else if (aiState == 5)
            {
                Slide(target, slideSpeed, 1, 0);    // slide right from left
            }
            else if (aiState == 6)
            {
                Slide(target, slideSpeed, -1, 0);   // slide left from right
            }
            if (aiState == 999)
            {
                npc.GetGlobalNPC<NPCsGLOBAL>().liftable = true;
                npc.damage = 0;
            }
            Point npcWorld = npc.BottomLeft.ToTileCoordinates();
            bool onPlatform = false;
            for (int i = npcWorld.X; i < npcWorld.X + npc.width / 16; i++)
            {
                /*Dust dust = Main.dust[Dust.NewDust(new Vector2(i, npcWorld.Y) * 16, 16, 16, 6)];
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;*/

                Tile t = Framing.GetTileSafely(i,npcWorld.Y);
                if (Main.tileSolid[t.type] && t.active() && !TileID.Sets.Platforms[t.type])
                {
                    onPlatform = false;
                    break;
                }
                if (TileID.Sets.Platforms[t.type] && t.active())
                {
                    onPlatform = true;
                }
            }
            if (onPlatform) 
                npc.noTileCollide = true;
            else npc.noTileCollide = false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (aiState == 5 || aiState == 6)
            {
                target.velocity += npc.velocity * 1f - new Vector2(0, 3);
                npc.velocity *= -0.33f;
                aiTimer2 = 0;
                aiTimer = 0;
                aiState = 1;
            }
        }
        private void Slide(Player target, float slideSpeed, int x, int y)
        {

            if (aiTimer == 0)
            {
                aiTimer2++;
                Vector2 targetPos = new Vector2(target.Center.X - x * 150, target.Center.Y - y * 150);
                if (x != 0) targetPos.Y -= 10;
                Vector2 toTarget = new Vector2(targetPos.X - npc.Center.X, targetPos.Y - npc.Center.Y);
                toTarget.Normalize();
                bool cantReach = aiTimer2 > 40 && Vector2.Distance(npc.velocity, Vector2.Zero) < 0.4f;
                npc.velocity.X = toTarget.X * 8;
                npc.velocity.Y = toTarget.Y * 8 * MathHelper.Clamp(MathHelper.Distance(targetPos.Y, npc.Center.Y) / 500, 1, 2);

                if (Vector2.Distance(npc.Center, targetPos) < 15 || cantReach)
                {
                    aiTimer = 1;
                    aiTimer2 = 0;
                    npc.velocity = Vector2.Zero;
                    Main.PlaySound(SoundID.DD2_DarkMageCastHeal, npc.position);
                }
            }
            else if (aiTimer == 1)
            {
                aiTimer2++;
                if (x > 0 && npc.Center.X > target.Center.X + x * 200 || x < 0 && npc.Center.X < target.Center.X + x * 200)
                {
                    aiTimer2 = 0;
                    aiTimer = 0;
                    aiState = 1;
                }
                if (aiTimer2 > 10 && ((Math.Abs(npc.velocity.Y) < 0.3f && y != 0) || Math.Abs(npc.velocity.X) < 0.3f && x != 0))
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: 1f);
                    aiTimer2 = 0;
                    aiTimer = 0;
                    aiState = 1;
                }
                if (npc.velocity.Y < slideSpeed && y != 0) npc.velocity.Y += y * 0.4f;
                else if (npc.velocity.X < slideSpeed && x != 0) npc.velocity.X += x * 0.4f;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
