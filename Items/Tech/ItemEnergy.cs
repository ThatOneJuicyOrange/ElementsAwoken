using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;

namespace ElementsAwoken.Items
{
    public class ItemEnergy : GlobalItem
    {
        public int energy = 0;
        public ItemEnergy()
        {
            energy = 0;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ItemEnergy myClone = (ItemEnergy)base.Clone(item, itemClone);
            myClone.energy = energy;
            return myClone;
        }
        public override bool CanUseItem(Item item, Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (modPlayer.energy < energy)
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (energy > 0)
            {
                modPlayer.energy -= energy;

            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            ItemEnergy modItem = item.GetGlobalItem<ItemEnergy>();
            if (modItem.energy > 0)
            {
                TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Energy", "Uses " + energy + " energy");
                tooltips.Add(tip);
            }
        }
    }
}