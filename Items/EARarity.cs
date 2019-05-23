using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;

namespace ElementsAwoken.Items
{
    public class EARarity : GlobalItem
    {
        private bool rarityStored = false;

        private int initialRarity = 0;

        public int rare = 0;

        public int previousPrefix;

        public EARarity()
        {
            rare = 0;
            initialRarity = 0;
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
            EARarity myClone = (EARarity)base.Clone(item, itemClone);
            myClone.rare = rare;
            myClone.initialRarity = initialRarity;
            myClone.previousPrefix = previousPrefix;
            return myClone;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            EARarity modItem = item.GetGlobalItem<EARarity>();
            if (!item.expert)
            {
                TooltipLine tooltipLine = tooltips.Single(i => i.Name == "ItemName" && i.mod == "Terraria");

                switch (modItem.rare)
                {
                    case 12:
                        tooltipLine.overrideColor = EAColors.RarityMagenta; // void leviathan
                        break;
                    case 13:
                        tooltipLine.overrideColor = EAColors.RarityDarkRed; // azana
                        break;
                    case 14:
                        tooltipLine.overrideColor = EAColors.RarityDarkBlue; // elemental
                        break;
                    case 15:
                        tooltipLine.overrideColor = EAColors.RarityBrightGreen; // ancient
                        break;
                }
            }
            //Main.NewText("item.rare = " + item.rare);
            //Main.NewText("modItem.rare = " + modItem.rare);
        }
        public override void SetDefaults(Item item)
        {
            EARarity modItem = item.GetGlobalItem<EARarity>();
            if (modItem.rare > 11)
            {
                item.rare = 11; // to make sure any items using rarities to do stuff dont completely break
            }
            if (modItem.rare < 11)
            {
                modItem.rare = item.rare; // doesnt include rarities for some reason
            }
        }
        public override void UpdateInventory(Item item, Player player)
        {
            EARarity modItem = item.GetGlobalItem<EARarity>();
            if (!rarityStored)
            {
                initialRarity = modItem.rare;
                rarityStored = true;
            }
            //initialRarity = item.SetDefaults(item.type).rare;
            modItem.rare = initialRarity;
            int prefix = item.prefix;
            float damageBoost = 1f;
            float knockbackBoost = 1f;
            float speedBoost = 1f;
            float sizeBoost = 1f;
            float shootspeedBoost = 1f;
            float manaCostBoost = 1f;
            switch (prefix)
            {
                case 1:
                    sizeBoost = 1.12f;
                    break;
                case 2:
                    sizeBoost = 1.18f;
                    break;
                case 3:
                    damageBoost = 1.05f;
                    sizeBoost = 1.05f;
                    break;
                case 4:
                    damageBoost = 1.1f;
                    sizeBoost = 1.1f;
                    knockbackBoost = 1.1f;
                    break;
                case 5:
                    damageBoost = 1.15f;
                    break;
                case 6:
                    damageBoost = 1.1f;
                    break;
                case 81:
                    knockbackBoost = 1.15f;
                    damageBoost = 1.15f;
                    speedBoost = 0.9f;
                    sizeBoost = 1.1f;
                    break;
                case 7:
                    sizeBoost = 0.82f;
                    break;
                case 8:
                    knockbackBoost = 0.85f;
                    damageBoost = 0.85f;
                    sizeBoost = 0.87f;
                    break;
                case 9:
                    sizeBoost = 0.9f;
                    break;
                case 10:
                    damageBoost = 0.85f;
                    break;
                case 11:
                    speedBoost = 1.1f;
                    knockbackBoost = 0.9f;
                    sizeBoost = 0.9f;
                    break;
                case 12:
                    knockbackBoost = 1.1f;
                    damageBoost = 1.05f;
                    sizeBoost = 1.1f;
                    speedBoost = 1.15f;
                    break;
                case 13:
                    knockbackBoost = 0.8f;
                    damageBoost = 0.9f;
                    sizeBoost = 1.1f;
                    break;
                case 14:
                    knockbackBoost = 1.15f;
                    speedBoost = 1.1f;
                    break;
                case 15:
                    knockbackBoost = 0.9f;
                    speedBoost = 0.85f;
                    break;
                case 16:
                    damageBoost = 1.1f;
                    break;
                case 17:
                    speedBoost = 0.85f;
                    shootspeedBoost = 1.1f;
                    break;
                case 18:
                    speedBoost = 0.9f;
                    shootspeedBoost = 1.15f;
                    break;
                case 19:
                    knockbackBoost = 1.15f;
                    shootspeedBoost = 1.05f;
                    break;
                case 20:
                    knockbackBoost = 1.05f;
                    shootspeedBoost = 1.05f;
                    damageBoost = 1.1f;
                    speedBoost = 0.95f;
                    break;
                case 21:
                    knockbackBoost = 1.15f;
                    damageBoost = 1.1f;
                    break;
                case 82:
                    knockbackBoost = 1.15f;
                    damageBoost = 1.15f;
                    speedBoost = 0.9f;
                    shootspeedBoost = 1.1f;
                    break;
                case 22:
                    knockbackBoost = 0.9f;
                    shootspeedBoost = 0.9f;
                    damageBoost = 0.85f;
                    break;
                case 23:
                    speedBoost = 1.15f;
                    shootspeedBoost = 0.9f;
                    break;
                case 24:
                    speedBoost = 1.1f;
                    knockbackBoost = 0.8f;
                    break;
                case 25:
                    speedBoost = 1.1f;
                    damageBoost = 1.15f;
                    break;
                case 58:
                    speedBoost = 0.85f;
                    damageBoost = 0.85f;
                    break;
                case 26:
                    manaCostBoost = 0.85f;
                    damageBoost = 1.1f;
                    break;
                case 27:
                    manaCostBoost = 0.85f;
                    break;
                case 28:
                    manaCostBoost = 0.85f;
                    damageBoost = 1.15f;
                    knockbackBoost = 1.05f;
                    break;
                case 83:
                    knockbackBoost = 1.15f;
                    damageBoost = 1.15f;
                    speedBoost = 0.9f;
                    manaCostBoost = 0.9f;
                    break;
                case 29:
                    manaCostBoost = 1.1f;
                    break;
                case 30:
                    manaCostBoost = 1.2f;
                    damageBoost = 0.9f;
                    break;
                case 31:
                    knockbackBoost = 0.9f;
                    damageBoost = 0.9f;
                    break;
                case 32:
                    manaCostBoost = 1.15f;
                    damageBoost = 1.1f;
                    break;
                case 33:
                    manaCostBoost = 1.1f;
                    knockbackBoost = 1.1f;
                    speedBoost = 0.9f;
                    break;
                case 34:
                    manaCostBoost = 0.9f;
                    knockbackBoost = 1.1f;
                    speedBoost = 1.1f;
                    damageBoost = 1.1f;
                    break;
                case 35:
                    manaCostBoost = 1.2f;
                    damageBoost = 1.15f;
                    knockbackBoost = 1.15f;
                    break;
                case 52:
                    manaCostBoost = 0.9f;
                    damageBoost = 0.9f;
                    speedBoost = 0.9f;
                    break;
                case 37:
                    damageBoost = 1.1f;
                    knockbackBoost = 1.1f;
                    break;
                case 38:
                    knockbackBoost = 1.15f;
                    break;
                case 53:
                    damageBoost = 1.1f;
                    break;
                case 54:
                    knockbackBoost = 1.15f;
                    break;
                case 55:
                    knockbackBoost = 1.15f;
                    damageBoost = 1.05f;
                    break;
                case 59:
                    knockbackBoost = 1.15f;
                    damageBoost = 1.15f;
                    break;
                case 60:
                    damageBoost = 1.15f;
                    break;
                case 39:
                    damageBoost = 0.7f;
                    knockbackBoost = 0.8f;
                    break;
                case 40:
                    damageBoost = 0.85f;
                    break;
                case 56:
                    knockbackBoost = 0.8f;
                    break;
                case 41:
                    knockbackBoost = 0.85f;
                    damageBoost = 0.9f;
                    break;
                case 57:
                    knockbackBoost = 0.9f;
                    damageBoost = 1.18f;
                    break;
                case 42:
                    speedBoost = 0.9f;
                    break;
                case 43:
                    damageBoost = 1.1f;
                    speedBoost = 0.9f;
                    break;
                case 44:
                    speedBoost = 0.9f;
                    break;
                case 45:
                    speedBoost = 0.95f;
                    break;
                case 46:
                    speedBoost = 0.94f;
                    damageBoost = 1.07f;
                    break;
                case 47:
                    speedBoost = 1.15f;
                    break;
                case 48:
                    speedBoost = 1.2f;
                    break;
                case 49:
                    speedBoost = 1.08f;
                    break;
                case 50:
                    damageBoost = 0.8f;
                    speedBoost = 1.15f;
                    break;
                case 51:
                    knockbackBoost = 0.9f;
                    speedBoost = 0.9f;
                    damageBoost = 1.05f;
                    break;
            }
            float prefixValue = 1f * damageBoost * (2f - speedBoost) * (2f - manaCostBoost) * sizeBoost * knockbackBoost * shootspeedBoost * (1f + (float)item.crit * 0.02f);
            //if (previousPrefix != prefix) // stops the custom rarity from raising
            {
                previousPrefix = item.prefix;
                if (prefix == 62 || prefix == 69 || prefix == 73 || prefix == 77)
                {
                    prefixValue *= 1.05f;
                }
                if (prefix == 63 || prefix == 70 || prefix == 74 || prefix == 78 || prefix == 67)
                {
                    prefixValue *= 1.1f;
                }
                if (prefix == 64 || prefix == 71 || prefix == 75 || prefix == 79 || prefix == 66)
                {
                    prefixValue *= 1.15f;
                }
                if (prefix == 65 || prefix == 72 || prefix == 76 || prefix == 80 || prefix == 68)
                {
                    prefixValue *= 1.2f;
                }
                if ((double)prefixValue >= 1.2)
                {
                    modItem.rare += 2;
                }
                else if ((double)prefixValue >= 1.05)
                {
                    modItem.rare++;
                }
                else if ((double)prefixValue <= 0.8)
                {
                    modItem.rare -= 2;
                }
                else if ((double)prefixValue <= 0.95)
                {
                    modItem.rare--;
                }
                if (modItem.rare <= 11)
                {
                    item.rare = modItem.rare;
                }
                if (modItem.rare > 11)
                {
                    item.rare = 11;
                }
                if (modItem.rare > 15)
                {
                    modItem.rare = 15;
                }
            }
        }
    }
}