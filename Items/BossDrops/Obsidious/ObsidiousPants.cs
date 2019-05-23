using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    [AutoloadEquip(EquipType.Legs)]
    public class ObsidiousPants : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 36;
            item.rare = 1;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Pants");
        }
    }
}
