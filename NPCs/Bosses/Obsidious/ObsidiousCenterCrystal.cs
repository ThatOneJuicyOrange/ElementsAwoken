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
    public class ObsidiousCenterCrystal : ModNPC
    {
        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float smashCount
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shockwave
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float visuals
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 24;

            npc.aiStyle = -1;

            npc.scale *= 1.2f;
            npc.lifeMax = 100;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.netAlways = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Crystal");
        }
        public override void AI()
        {
            npc.rotation += npc.velocity.X * 0.2f;
            aiTimer++;
            if (aiTimer > 300)
            {
                npc.active = false;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White * (1 - (npc.alpha / 255)), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
