using ElementsAwoken.Buffs.PetBuffs;
using ElementsAwoken.Projectiles.Pets;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Pets
{
    public class EldritchKeepsake : ModItem
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

            item.rare = 9;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.noMelee = true;

            item.shoot = ProjectileType<CelestialWatcher>();
            item.buffType = BuffType<CelestialWatcherBuff>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Keepsake");
            Tooltip.SetDefault("'A relic composed of four lost souls'\nSummons a Fallen Stargazer to follow you around");
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
