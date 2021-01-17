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

namespace ElementsAwoken.NPCs.MovingPlatforms
{
    public class FloatyBoi : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 86;
            npc.height = 32;
            
            npc.aiStyle = -1;

            npc.lifeMax = 1;

            npc.knockBackResist = 0f;

            npc.GetGlobalNPC<VolcanicPlateau.PlateauNPCs>().tomeClickable = false;
            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.immortal = true;
            npc.friendly = true;
            npc.noGravity = true;

            npc.velocity.X = Main.rand.NextBool() ? -2 : 2;
            //npc.GetGlobalNPC<NPCsGLOBAL>().solidY = true;
            //npc.GetGlobalNPC<NPCsGLOBAL>().solidX = true;
            npc.GetGlobalNPC<NPCsGLOBAL>().platformNPC = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("floaty boi :) love him");
        }
    }
}
