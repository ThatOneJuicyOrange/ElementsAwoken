using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    [AutoloadEquip(EquipType.Head)]
    public class EnergyWeaversHelm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.defense = 27;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Weaver's Helm");
            Tooltip.SetDefault("Damage increased the closer it is to midday\nCritical strike chance incresaed by 7");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 7;
            player.meleeCrit += 7;
            player.rangedCrit += 7;
            player.thrownCrit += 7;

           
            //midday 27000.0
            float damageBonus = 1f;
            if (Main.dayTime)
            {
                damageBonus = MathHelper.Lerp(1.2f, 1f, MathHelper.Distance((float)Main.time, 27000) / 27000);
            }

            player.meleeDamage *= damageBonus;
            player.magicDamage *= damageBonus;
            player.rangedDamage *= damageBonus;
            player.minionDamage *= damageBonus;
            player.thrownDamage *= damageBonus;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("EnergyWeaversBreastplate") && legs.type == mod.ItemType("EnergyWeaversLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "You charge up energy and release it at nearby enemies";
            player.GetModPlayer<MyPlayer>().energyWeaverArmor = true;
        }
    }
}
