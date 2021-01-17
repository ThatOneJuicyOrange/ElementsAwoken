using ElementsAwoken.NPCs.Bosses.Azana;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class BlueprintItem2 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 54;

            item.maxStack = 999;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarletine-007 Blueprint II");
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange += 2;
        }
        public override bool ItemSpace(Player player)
        {
            return true;
        }
        public override bool OnPickup(Player player)
        {
            if (MyWorld.mechBlueprints < 3) MyWorld.mechBlueprints++;
            if (Main.netMode == 2) NetMessage.SendData(MessageID.WorldData);
            return false;
        }
    }
}
