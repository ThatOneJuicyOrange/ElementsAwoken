using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    [AutoloadEquip(EquipType.Body)]
    public class EnergyWeaversBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.defense = 15;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Weaver's Breastplate");
            Tooltip.SetDefault("Wing mobility is greatly increased\n20% decreased mana usage");
        }
        public override void UpdateEquip(Player player)
        {
            player.manaCost *= 0.8f;

            player.wingTimeMax = (int)(player.wingTimeMax * 1.1f);
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed *= 1.1f;
            acceleration *= 1.1f;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling *= 1.1f;
            ascentWhenRising *= 1.5f;
            maxCanAscendMultiplier *= 1.5f;
            maxAscentMultiplier *= 1.1f;
            constantAscend *= 1.1f;
        }
    }
}
