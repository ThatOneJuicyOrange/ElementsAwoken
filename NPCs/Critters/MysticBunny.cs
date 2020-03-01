using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Critters 
{
    public class MysticBunny : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 20;

            npc.damage = 0;

            npc.defense = 11;
            npc.lifeMax = 20;

            animationType = NPCID.Bunny;
            npc.aiStyle = 7;
            aiType = NPCID.Bunny;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Bunny");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneHoly) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            spawnInfo.spawnTileY < Main.rockLayer &&
            !Main.snowMoon && 
            !Main.pumpkinMoon && 
            Main.dayTime 
            ? 0.3f : 0f;
        }
    }
}