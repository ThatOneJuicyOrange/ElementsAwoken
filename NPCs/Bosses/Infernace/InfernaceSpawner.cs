using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Infernace
{
    public class InfernaceSpawner : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Prompts/InfernaceGuardian"; } }
        private float visualsAI
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.damage = 10;

            npc.aiStyle = -1;

            npc.width = 26;
            npc.height = 50;
            npc.alpha = 255;
            npc.lifeMax = 5;
            npc.knockBackResist = 0f;

            npc.noGravity = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernace's Guardian");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D cast = mod.GetTexture("NPCs/Prompts/InfernaceGuadianCast");
            Vector2 castOrigin = new Vector2(cast.Width * 0.5f, cast.Height * 0.5f);
            Vector2 castAddition = npc.direction != 1 ? new Vector2(-2, 6) : new Vector2(2, 6);
            Vector2 castPos = npc.position - Main.screenPosition + castOrigin + new Vector2(0f, npc.gfxOffY) + castAddition;
            spriteBatch.Draw(cast, castPos, null, Color.White * (1- ((float)npc.alpha/255f)), visualsAI / 15f, castOrigin, npc.scale, spriteEffects, 0f);
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            visualsAI++;
            int infernaceID = NPC.FindFirstNPC(ModContent.NPCType<Infernace>());
            if (infernaceID >= 0)
            {
                NPC parent = Main.npc[infernaceID];
                Vector2 direction = parent.Center - npc.Center;
                npc.spriteDirection = Math.Sign(direction.X);
                npc.velocity.X = 0f;

                npc.Center = parent.Center + new Vector2(200 * npc.ai[0], 300);
                if (npc.alpha > 0)
                {
                    npc.alpha -= 255 / 20;
                }
                else
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6)];
                    Vector2 toTarget = new Vector2(parent.Center.X - npc.Center.X, parent.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    dust.velocity = toTarget * 28f;
                    dust.noGravity = true;
                    dust.fadeIn = 1.2f;
                    if (parent.alpha <= 0)
                    {
                        npc.active = false;
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust2 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6)];
                            dust2.noGravity = true;
                            dust2.scale = 1f;
                            dust2.velocity *= 0.1f;
                        }
                    }
                }
            }
            else npc.active = false;
        }
    }
}
