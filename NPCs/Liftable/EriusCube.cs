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

namespace ElementsAwoken.NPCs.Liftable
{
    public class EriusCube : HeldNPCBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 32;
            npc.height = 32;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suspicious Cube");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Lighting.AddLight(npc.Center, 0.75f, 0.95f, 0.5f);

            Texture2D bloomTex = ModContent.GetTexture("ElementsAwoken/Extra/Bloom");

            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive);
            Color color = Color.Lerp(new Color(205, 237, 116) * 0.4f, new Color(117, 217, 74) * 0.1f, (1 + (float)Math.Sin((float)MyWorld.generalTimer / 30)) / 2);
            spriteBatch.Draw(bloomTex, npc.Center - Main.screenPosition, bloomTex.Frame(), color, 0, bloomTex.Size() / 2, 0.75f, 0, 0);

            spriteBatch.End();
            spriteBatch.Begin();

            Texture2D texture = mod.GetTexture("NPCs/Liftable/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0.0f);
        }
    }
}
