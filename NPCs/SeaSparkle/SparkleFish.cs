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

namespace ElementsAwoken.NPCs.SeaSparkle
{
    public class SparkleFish : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 18;

            npc.aiStyle = -1;

            npc.defense = 5;
            npc.lifeMax = 50;
            npc.damage = 0;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.dontTakeDamageFromHostiles = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkle Fish");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Color color = Color.White;
            if (npc.ai[0] == 0)
            {
                color = Color.Lerp(new Color(146, 240, 235, 100), new Color(79, 111, 227, 100), (float)(1 + Math.Sin(npc.ai[2] / 30f)) / 2f);
            }
            Texture2D texture = mod.GetTexture("NPCs/SeaSparkle/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            /*for (int k = 0; k < 7; k++)
            {
                Vector2 newPos = npc.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1));
                Main.spriteBatch.Draw(texture, newPos - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, -1), frame, color, npc.rotation, origin, 1f, effects, 0f);
            }*/
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, -1), frame, color, npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }


        public override void AI()
        {
            if (npc.direction == 0) npc.direction = 1;
            npc.spriteDirection = npc.direction;
            EAUtils.PushOtherEntities(npc, pushStrength: 0.05f);
            npc.ai[2]++;

            Vector2 target = npc.Center;
            /*if (GetInstance<Config>().debugMode)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.Center, 2, 2, DustID.PinkFlame)];
                dust.noGravity = true;
                dust = Main.dust[Dust.NewDust(target, 2, 2, DustID.PinkFlame)];
                dust.noGravity = true;
            }*/
            npc.ai[1]++;
            if (npc.ai[1] > 600 || npc.ai[2] == 0 || Math.Abs(npc.Center.X - npc.ai[2]) < 20 || Math.Abs(npc.Center.Y - npc.ai[3]) < 20)
            {
                npc.ai[1] = 0;
                //FindLocation();
            }

            if (npc.lavaWet)
            {
                npc.direction = Math.Sign(target.X - npc.Center.X);
                npc.directionY = Math.Sign(target.Y - npc.Center.Y);

                if (npc.velocity.X > 0f && npc.direction < 0) npc.velocity.X = npc.velocity.X * 0.95f;
                if (npc.velocity.X < 0f && npc.direction > 0) npc.velocity.X = npc.velocity.X * 0.95f;
                if (npc.velocity.Y > 0f && npc.directionY < 0) npc.velocity.Y = npc.velocity.Y * 0.95f;
                if (npc.velocity.Y < 0f && npc.directionY > 0) npc.velocity.Y = npc.velocity.Y * 0.95f;
                float spdX = 0.009f;
                float spdY = 0.007f;
                npc.velocity.X = npc.velocity.X + (float)npc.direction * spdX;
                npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * spdY;

                float maxVelX = 12;
                float maxVelY = 8;
                if (npc.velocity.X > maxVelX)
                {
                    npc.velocity.X = maxVelX;
                }
                if (npc.velocity.X < -maxVelX)
                {
                    npc.velocity.X = -maxVelX;
                }
                if (npc.velocity.Y > maxVelY)
                {
                    npc.velocity.Y = maxVelY;
                }
                if (npc.velocity.Y < -maxVelY)
                {
                    npc.velocity.Y = -maxVelY;
                }
            }
            if (!npc.wet)
            {
                npc.velocity.Y += 0.16f;
            }
            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
    }
}
