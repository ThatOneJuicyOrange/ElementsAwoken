using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Drakonite.Greater
{
    public class DragonCharger : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 86;

            npc.width = 66; 
            npc.height = 34;

            npc.defense = 12;
            npc.lifeMax = 600;
            npc.damage = 50;
            npc.knockBackResist = 0.5f;

            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;

            /*banner = npc.type;
            bannerItem = mod.ItemType("DragonChargerBanner");*/

            NPCID.Sets.TrailCacheLength[npc.type] = 6;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Charger");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.direction = Math.Sign(npc.velocity.X);
            npc.spriteDirection = npc.direction;
            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));

            npc.frameCounter += 1;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 3)
            {
                npc.frame.Y = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Texture2D tex = Main.npcTexture[npc.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                SpriteEffects spriteEffects = npc.direction != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                float alpha = 1 - ((float)k / (float)npc.oldPos.Length);
                Color color = Color.Lerp(npc.GetAlpha(lightColor), new Color(255, 51, 0), (float)k / (float)npc.oldPos.Length) * alpha;
                spritebatch.Draw(tex, drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);

            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.spawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.spawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneDungeon && NPC.downedPlantBoss ? 0.03f : 0f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("Dragonfire"), 100, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
                }
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DragonCharger" + i), npc.scale);
                }
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RefinedDrakonite"), Main.rand.Next(1, 2));
        }
    }
}