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

namespace ElementsAwoken.NPCs.Bosses.Obsidious
{
    public class ObsidiousIllusion : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/Obsidious/Obsidious"; } }
        private int maxAlpha = 120;
        private int parentID
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float illusionNum
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float illusionType
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
            DisplayName.SetDefault("Obsidious Illusion");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.width = 148;
            npc.height = 148;

            npc.scale *= 1.22f;
            npc.aiStyle = -1;

            npc.lifeMax = 75000;
            npc.damage = 75;
            npc.defense = 55;
            npc.knockBackResist = 0f;
            npc.alpha = 255;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.gfxOffY = -4;
            npc.GivenName = " ";
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 90;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 110;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/Obsidious/Obsidious_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White * (1 - (npc.alpha / 255)), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void AI()
        {
            NPC parent = Main.npc[parentID];
            npc.TargetClosest(false);
            Player player = Main.player[npc.target];
            Obsidious.PushPlayer(npc);
            Vector2 arenaCenter = Vector2.Zero;
            int ID = NPC.FindFirstNPC(ModContent.NPCType<ObsidiousArenaCrystal>());
            if (ID >= 0)
            {
                arenaCenter = Main.npc[ID].Center;
            }
            if (arenaCenter != Vector2.Zero)
            {
                npc.GivenName = aiTimer3 + "";
                // mirror
                if (illusionType == 0)
                {
                    float xVal = arenaCenter.X - parent.Center.X;
                    float yVal = arenaCenter.Y - parent.Center.Y;
                    if (aiTimer3 == 0 || aiTimer3 == 2) npc.position.X = arenaCenter.X + xVal;
                    else npc.position.X = arenaCenter.X - xVal;
                    if (aiTimer3 == 0 || aiTimer3 == 1) npc.position.Y = arenaCenter.Y + yVal;
                    else npc.position.Y = arenaCenter.Y - yVal;
                    npc.position.X -= (float)(npc.width / 2);
                    npc.position.Y -= (float)(npc.height / 2);
                }
                else
                {
                    float arenaWidth = 1082.502f;
                    float arenaHeight = 794.0468f;
                    float ratio = arenaWidth / arenaHeight;


                    float xVal = arenaCenter.X - parent.Center.X;
                    float yVal = arenaCenter.Y - parent.Center.Y;

                    float xVal2 = yVal * ratio;
                    float yVal2 = xVal / ratio;


                    if (illusionNum == 0) npc.position.X = arenaCenter.X + xVal;
                    else if (illusionNum == 1) npc.position.Y = arenaCenter.Y - yVal2;
                    else if (illusionNum == 2) npc.position.Y = arenaCenter.Y + yVal2;

                    if (illusionNum == 0) npc.position.Y = arenaCenter.Y + yVal;
                    else if (illusionNum == 1) npc.position.X = arenaCenter.X + xVal2;
                    else if (illusionNum == 2) npc.position.X = arenaCenter.X - xVal2;
                    npc.position.X -= (float)(npc.width / 2);
                    npc.position.Y -= (float)(npc.height / 2);
                }
            }
            if (parent.ai[1] == 3)
            {
                if (npc.alpha > maxAlpha) npc.alpha -= 5;
                else npc.alpha = maxAlpha;
            }
            else
            {
                npc.alpha += 5;
                if (npc.alpha >= 255) npc.active = false;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.alpha > maxAlpha) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }          
    }
}
