using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Armor.Vanity.TOJO
{
    [AutoloadEquip(EquipType.Body)]
    public class OrangesBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 36;
            item.rare = 9;
            item.vanity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ThatOneJuicyOrange's Breastplate");
        }
    }
}
