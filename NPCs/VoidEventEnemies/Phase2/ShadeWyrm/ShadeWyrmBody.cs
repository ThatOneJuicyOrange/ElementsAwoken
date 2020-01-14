using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase2.ShadeWyrm
{
    public class ShadeWyrmBody : ModNPC
    {
        public int dayTime = 0;
        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 32;

            npc.damage = 50;
            npc.defense = 40;
            npc.lifeMax = 100000; // doesnt matter unless in head
            npc.knockBackResist = 0.0f;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;

            npc.npcSlots = 0f;

            // all EA modded buffs (unless i forget to add new ones)
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("AncientDecay")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("DragonFire")] = true;
            npc.buffImmune[mod.BuffType("Discord")] = true;
            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Wyrm");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 75;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool PreAI()
        {
            bool expertMode = Main.expertMode;
            Player P = Main.player[npc.target];
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
            npc.ai[2]++;
            npc.ai[0]++;
            if (npc.ai[2] > 1150f)
            {
                npc.ai[2] = 0f;
            }
            if (npc.ai[0] > 500f)
            {
                npc.ai[0] = 0f;
            }
            //gore
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ShadeWyrmBody"), 1f);
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 50;
                npc.height = 50;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
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
                npc.velocity = Vector2.Zero;
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;

            }

            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;       //this make that the npc does not have a health bar
        }
    }
}
