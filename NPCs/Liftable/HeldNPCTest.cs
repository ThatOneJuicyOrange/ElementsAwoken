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
    public class HeldNPCTest : HeldNPCBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 32;
            npc.height = 32;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pick Up Test");
        }
       
    }
}
