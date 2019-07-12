using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Wasteland
{
    public class WastelandMinion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 26;

            npc.aiStyle = 3;
            npc.damage = 35;

            npc.defense = 12;
            npc.lifeMax = 50;
            npc.knockBackResist = 0.3f;
            animationType = 257;
            aiType = NPCID.AnomuraFungus;

            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutated Scorpion");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void AI()
        {
            npc.velocity *= 0.95f;
        }
    }
}