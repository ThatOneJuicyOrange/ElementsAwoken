using ElementsAwoken.NPCs.Bosses.Azana;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class ObsidiousDoorKey : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/Obsidious/Beneath/SliderKey"; } }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.maxStack = 999;
            item.GetGlobalItem<EATooltip>().unobtainable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Door Key");
        }
    }
}
