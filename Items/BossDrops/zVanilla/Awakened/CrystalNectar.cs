using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.BossDrops.zVanilla.Awakened
{
    public class CrystalNectar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.rare = 3;
            item.value = Item.sellPrice(0, 3, 0, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;

            item.color = Color.Yellow;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Nectar");
            Tooltip.SetDefault("Causes bees to become friendly\nBeing in honey speed up the player's swimming speed and doubles the honey duration\nIncreases the strength of friendly bees");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.honeyWet)
            {
                player.ignoreWater = true;
                player.runAcceleration *= 2;
                player.moveSpeed *= 1.2f;
                player.accRunSpeed *= 1.2f;
                player.jumpSpeedBoost += 2f;
                player.AddBuff(BuffID.Honey, 3600);
            }
            player.npcTypeNoAggro[NPCID.Bee] = true;
            player.npcTypeNoAggro[NPCID.BeeSmall] = true;
            player.strongBees = true;
        }
    }
}
