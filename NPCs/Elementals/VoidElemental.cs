using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Elementals
{
    public class VoidElemental : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 48;
            npc.damage = 80; //change
            npc.defense = 40; //change
            npc.lifeMax = 1100; //change
            npc.value = Item.buyPrice(0, 2, 0, 0); //change
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 5;
            npc.noGravity = true;
            npc.noTileCollide = true;
            aiType = NPCID.Wraith;
            animationType = NPCID.Wraith;
            banner = npc.type;
            bannerItem = mod.ItemType("VoidElementalBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Elemental");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            /*int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));*/

            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && MyWorld.downedVoidEvent && !Main.dayTime ? 0.04f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.3f, 0.0f);

            //STOP CLUMPING FOOLS (dotv event)
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
            player.AddBuff(BuffID.Slow, 300, false);
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1); //Item spawn
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
    }
}