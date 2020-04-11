using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Events.RadiantRain.Enemies
{
    public class RadiantMasterDeath : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 50;

            npc.immortal = true;
            npc.dontTakeDamage = true;

            npc.aiStyle = -1;
            npc.lifeMax = 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("rw death sound");
        }

        public override void AI()
        {
            npc.ai[0]++;
            if (npc.ai[0] > 650)
            {
                npc.active = false;
            }
        }
    }
}
