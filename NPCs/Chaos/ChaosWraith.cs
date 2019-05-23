using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Chaos
{
    public class ChaosWraith : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 48;

            npc.damage = 80;
            npc.defense = 20;
            npc.lifeMax = 2000;
            npc.knockBackResist = 0.5f;

            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;

            npc.aiStyle = 5;
            npc.noGravity = true;
            npc.noTileCollide = true;
            aiType = NPCID.Wraith;
            //animationType = NPCID.Wraith;

            /*banner = npc.type;
            bannerItem = mod.ItemType("ChaosWraithBanner");*/
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Wraith");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 5)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * 5)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }

            if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = 1;
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = npc.velocity.X * 0.1f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.spawnTileY < Main.rockLayer) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && !Main.dayTime && MyWorld.downedVoidLeviathan ? 0.065f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.1f, 0.1f);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 300, false);
            player.AddBuff(BuffID.BrokenArmor, 300, false);
            player.AddBuff(BuffID.WitheredArmor, 300, false);
            player.AddBuff(BuffID.WitheredWeapon, 300, false);
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChaoticFlare"), Main.rand.Next(3, 6));
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
    }
}