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

namespace ElementsAwoken.NPCs.Bosses.TheTempleKeepers
{
    [AutoloadBossHead]
    public class AncientWyrmHead : ModNPC
    {
        public int halfLife = 0;
        public int dayTime = 0;
        public bool despawn = false;
        public bool close = false;
        public bool chargePlayer = false;
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 28;

            npc.lifeMax = 150000;
            npc.damage = 150;
            npc.defense = 45;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit56;
            npc.DeathSound = SoundID.NPCDeath60;

            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.GoblinInvasion;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/EyeDragonTheme");

            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            bossBag = mod.ItemType("TempleKeepersBag");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Wyrm");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 200000;
            npc.damage = 200;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 250000;
                npc.damage = 250;
                npc.defense = 60;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            if (close)
            {
                npc.frame.Y = 1;
            }
            else
            {
                npc.frame.Y = 0;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(mod.NPCType("TheEye")))
            {
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
                else
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WyrmHeart"));

                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TemplesCrystal"));
                    }
                    if (choice == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GazeOfInferno"));
                    }
                    if (choice == 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheAllSeer"));
                    }
                    if (choice == 3)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WyrmClaw"));
                    }
                }
            }
            MyWorld.downedAncientWyrm = true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/TheTempleKeepers/Glow/AncientWyrmHead_Glow");
            Rectangle frame = new Rectangle(0, texture.Height * npc.frame.Y, texture.Width, texture.Height);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int num = texture.Height / 2;
            int yPos = num * npc.frame.Y;
            Rectangle sourceRectangle = new Rectangle(0, yPos, texture.Width, num);
            Vector2 origin = npc.spriteDirection == -1 ? (new Vector2(texture.Width * 0.5f + 10, num * 0.5f + 16)) : (new Vector2(texture.Width - 10, num + 16)); // to stop the sprite moving a lot when it turns
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int num = texture.Height / 2;
            int yPos = num * npc.frame.Y;
            Rectangle sourceRectangle = new Rectangle(0, yPos, texture.Width, num);
            Vector2 origin = npc.spriteDirection == -1 ? (new Vector2(texture.Width * 0.5f + 10, num * 0.5f + 16)) : (new Vector2(texture.Width - 10, num + 16)); // to stop the sprite moving a lot when it turns
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, sourceRectangle, drawColor, npc.rotation, origin, npc.scale, effects, 0);
            return false;
        }
        public override bool PreAI()
        {
            Player P = Main.player[npc.target];
            bool expertMode = Main.expertMode;
            if (Main.player[npc.target].dead || Main.dayTime)
            {
                despawn = true;
            }
            if (despawn)
            {
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                npc.ai[1]++;
                if (npc.ai[1] >= 300)
                {
                    npc.active = false;
                }
            }
            // breath

            if (Vector2.Distance(npc.Center, P.Center) <= 400)
            {
                close = true;
            }
            else
            {
                close = false;
            }
            if (close)
            {
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                if (Main.rand.Next(7) == 0)
                {
                    int damage = Main.expertMode ? 35 : 50; // reduce damage in expert mode because it doubles anyway
                    Projectile.NewProjectile(vector8.X + npc.velocity.X, vector8.Y + npc.velocity.Y, npc.velocity.X * 0.8f + Main.rand.NextFloat(-0.7f, 0.7f) * 3, npc.velocity.Y * 0.8f + Main.rand.NextFloat(-0.7f, 0.7f) * 3, mod.ProjectileType("WyrmBreath"), damage, 0f, 0);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.PlaySound(SoundID.DD2_BetsyFlameBreath, npc.position);
                    }
                }
            }
            //worm stuff
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (npc.ai[0] == 0)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;
                    int randomWormLength = Main.rand.Next(30, 40);
                    for (int i = 0; i < randomWormLength; i++)
                    {
                        int bodyType = mod.NPCType("AncientWyrmBody");
                        if (i % 2 == 0)
                        {
                            bodyType = mod.NPCType("AncientWyrmArms");
                        }
                        else
                        {
                            mod.NPCType("AncientWyrmBody");
                        }
                        latestNPC = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), bodyType, npc.whoAmI, 0, latestNPC);                   
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    }
                    NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("AncientWyrmTail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }

            int minTilePosX = (int)(npc.position.X / 16.0) - 1;
            int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
            int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;

            bool collision = false;
            // This is the initial check for collision with tiles.
            for (int i = minTilePosX; i < maxTilePosX; ++i)
            {
                for (int j = minTilePosY; j < maxTilePosY; ++j)
                {
                    if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
                        {
                            collision = true;
                            if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
                                WorldGen.KillTile(i, j, true, true, false);
                        }
                    }
                }
            }
            if (Vector2.Distance(npc.Center, P.Center) >= 500)
            {
                chargePlayer = true;
            }
            if (Vector2.Distance(npc.Center, P.Center) <= 350)           
            {
                chargePlayer = false;
            }
            float speed = 18f;
            if (!NPC.AnyNPCs(mod.NPCType("TheEye")))
            {
                speed = 25f;
            }
                float acceleration = 0.6f;

            Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
            float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
            npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            Vector2 vector168 = npc.Center;
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
            if (!collision)
            {
                npc.TargetClosest(true);
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (npc.velocity.Y > speed)
                    npc.velocity.Y = speed;
                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    else
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                }

                else if (npc.velocity.Y == speed)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                else if (npc.velocity.Y > 4.0)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X + acceleration * 0.9f;
                    else
                        npc.velocity.X = npc.velocity.X - acceleration * 0.9f;
                }
            }

            // Else we want to play some audio (soundDelay) and move towards our target.
            if (collision || chargePlayer)
            {
                if (npc.soundDelay == 0)
                {
                    float num1 = length / 40f;
                    if (num1 < 10.0)
                        num1 = 10f;
                    if (num1 > 20.0)
                        num1 = 20f;
                    npc.soundDelay = (int)num1;
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX = dirX * newSpeed;
                dirY = dirY * newSpeed;
                if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration * 2f;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                }
            }


            // Set the correct rotation for this NPC.
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            // Set the correct direction
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;

            }
            else
            {
                npc.spriteDirection = -1;
            }

            // Some netupdate stuff (multiplayer compatibility).
            if (collision)
            {
                if (npc.localAI[0] != 1)
                    npc.netUpdate = true;
                npc.localAI[0] = 1f;
            }
            else
            {
                if (npc.localAI[0] != 0.0)
                    npc.netUpdate = true;
                npc.localAI[0] = 0.0f;
            }
            if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                npc.netUpdate = true;

            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;   //this make the NPC Health Bar biger
            return null;
        }
    }
}
