using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    public class CelestialCrown : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.damage = 0;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item2;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.noMelee = true;

            item.shoot = mod.ProjectileType("RoyalEye");
            item.buffType = mod.BuffType("RoyalEyeBuff");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Crown");
            Tooltip.SetDefault("Summons a royal eyeball");
        }


        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
