using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.NPCs.Liftable
{
    public class FallenHarpyFly : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 20;

            npc.aiStyle = -1;

            npc.defense = 15;
            npc.lifeMax = 50;

            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath21;

            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Harpy");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter += 1;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 5) npc.frame.Y = 0;
        }
        public override void AI()
        {
            npc.ai[0]++;
            npc.velocity += new Vector2(-0.06f, -0.06f);
            if (npc.ai[0] >= 300)
            {
                npc.alpha += 5;
                if (npc.alpha >= 255) npc.active = false;
            }
        }
    }
}
