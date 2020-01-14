using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheTempleKeepers
{    
    public class AncientWyrmTail : ModNPC
    {
        public int dayTime = 0;
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 32;

            npc.damage = 50;
            npc.defense = 45;
            npc.lifeMax = 100000;
            npc.knockBackResist = 0.0f;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath20;

            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
            npc.behindTiles = true;

            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Wyrm");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 75;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 90;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/TheTempleKeepers/Glow/AncientWyrmTail_Glow");
            Rectangle frame = new Rectangle(0, texture.Height * npc.frame.Y, texture.Width, texture.Height);
            Vector2 origin = npc.spriteDirection == -1 ? (new Vector2(texture.Width * 0.5f, texture.Height * 0.5f)) : (new Vector2(texture.Width, texture.Height)); // to stop the sprite moving a lot when it turns
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = npc.spriteDirection == -1 ? (new Vector2(texture.Width * 0.5f, texture.Height * 0.5f)) : (new Vector2(texture.Width, texture.Height)); // to stop the sprite moving a lot when it turns
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, effects, 0);
            return false;
        }
        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead)
                npc.timeLeft = 50;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    //NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }
            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;      //this make that the npc does not have a health bar
        }
    }
}
