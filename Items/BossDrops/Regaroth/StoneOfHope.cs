using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    public class StoneOfHope : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.accessory = true;
            item.expert = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone of Hope");
            Tooltip.SetDefault("The relic of Regaroth\nThe lower your health the higher defense and damage");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= (player.statLifeMax2 * 0.9f) && player.statLife > (player.statLifeMax2 * 0.8f))
            {
                player.meleeDamage *= 1.01f;
                player.thrownDamage *= 1.01f;
                player.rangedDamage *= 1.01f;
                player.magicDamage *= 1.01f;
                player.minionDamage *= 1.01f;
                player.statDefense += 2;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.7f))
            {
                player.meleeDamage *= 1.02f;
                player.thrownDamage *= 1.02f;
                player.rangedDamage *= 1.02f;
                player.magicDamage *= 1.02f;
                player.minionDamage *= 1.02f;
                player.statDefense += 4;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.7f) && player.statLife > (player.statLifeMax2 * 0.6f))
            {
                player.meleeDamage *= 1.04f;
                player.thrownDamage *= 1.04f;
                player.rangedDamage *= 1.04f;
                player.magicDamage *= 1.04f;
                player.minionDamage *= 1.04f;
                player.statDefense += 6;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.5f))
            {
                player.meleeDamage *= 1.06f;
                player.thrownDamage *= 1.06f;
                player.rangedDamage *= 1.06f;
                player.magicDamage *= 1.06f;
                player.minionDamage *= 1.06f;
                player.statDefense += 8;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.5f) && player.statLife > (player.statLifeMax2 * 0.4f))
            {
                player.meleeDamage *= 1.09f;
                player.thrownDamage *= 1.09f;
                player.rangedDamage *= 1.09f;
                player.magicDamage *= 1.09f;
                player.minionDamage *= 1.09f;
                player.statDefense += 10;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.3f))
            {
                player.meleeDamage *= 1.12f;
                player.thrownDamage *= 1.12f;
                player.rangedDamage *= 1.12f;
                player.magicDamage *= 1.12f;
                player.minionDamage *= 1.12f;
                player.statDefense += 12;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.3f) && player.statLife > (player.statLifeMax2 * 0.2f))
            {
                player.meleeDamage *= 1.16f;
                player.thrownDamage *= 1.16f;
                player.rangedDamage *= 1.16f;
                player.magicDamage *= 1.16f;
                player.minionDamage *= 1.16f;
                player.statDefense += 15;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.2f) && player.statLife > (player.statLifeMax2 * 0.1f))
            {
                player.meleeDamage *= 1.2f;
                player.thrownDamage *= 1.2f;
                player.rangedDamage *= 1.2f;
                player.magicDamage *= 1.2f;
                player.minionDamage *= 1.2f;
                player.statDefense += 17;
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.1f))
            {
                player.meleeDamage *= 1.25f;
                player.thrownDamage *= 1.25f;
                player.rangedDamage *= 1.25f;
                player.magicDamage *= 1.25f;
                player.minionDamage *= 1.25f;
                player.statDefense += 20;
            }
        }

    }
}
