using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase2
{
    public class EtherealHunter : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 48;

            npc.damage = 120;
            npc.defense = 40;
            npc.lifeMax = 5000;
            npc.knockBackResist = 0.25f;

            npc.value = Item.buyPrice(0, 0, 20, 0);

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;

            npc.aiStyle = 22;
            npc.noGravity = true;
            npc.noTileCollide = true;

            aiType = NPCID.FloatyGross;

            banner = npc.type;
            bannerItem = mod.ItemType("EtherealHunterBanner");

            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 0;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000;
            npc.defense = 50;
            npc.damage = 150;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 15000;
                npc.defense = 65;
                npc.damage = 200;
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, Main.npcTexture[npc.type].Height * 0.5f);
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = npc.frame.Size() * 0.5f;
            Color color = npc.GetAlpha(drawColor);
            spritebatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, color, npc.rotation, origin, npc.scale, spriteEffects, 0);
            Vector2 addition = npc.direction != 1 ? new Vector2(-17, -16) : new Vector2(30, -16);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY) + addition;
                spritebatch.Draw(mod.GetTexture("Events/VoidEvent/Enemies/Phase2/EtherealHunter_Eyes"), drawPos, null, Color.White, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
            }
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ethereal Hunter");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void FindFrame(int frameHeight)
        {
            //npc.spriteDirection = -npc.direction;
            npc.frameCounter += 1;
            if (npc.frameCounter > 10)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 3)
            {
                npc.frame.Y = 0;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.1f, 0.1f, 0.5f);
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);

            if (Main.rand.Next(500) == 0)
            {
                int sound = 0;
                switch (Main.rand.Next(2))
                {
                    case 0: sound = 41; break;
                    case 1: sound = 42; break;
                    default: break;
                }
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, sound, 1f, 0f); // 29: 5 , 39, 40, 41, 42
            }
            // move up or down to players y
            float speed = 0.06f;
            Vector2 vector75 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float playerY = P.position.Y + (P.height / 2) - vector75.Y;
            if (Math.Abs(npc.Center.X - P.Center.X) <= 400) // 200 is 400 pix idk 
            {
                if (npc.velocity.Y < playerY)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    if (npc.velocity.Y < 0f && playerY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        return;
                    }
                }
                else if (npc.velocity.Y > playerY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    if (npc.velocity.Y > 0f && playerY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        return;
                    }
                }
            }
            //STOP CLUMPING FOOLS
            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (k != npc.whoAmI && other.type == npc.type && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                {
                    const float pushAway = 0.05f;
                    if (npc.position.X < other.position.X)
                    {
                        npc.velocity.X -= pushAway;
                    }
                    else
                    {
                        npc.velocity.X += pushAway;
                    }
                    if (npc.position.Y < other.position.Y)
                    {
                        npc.velocity.Y -= pushAway;
                    }
                    else
                    {
                        npc.velocity.Y += pushAway;
                    }
                }
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 180, false);
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Darkstone"), Main.rand.Next(3, 5));
            }
        }
    }
}