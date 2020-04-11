using ElementsAwoken.Buffs.PetBuffs;
using ElementsAwoken.Projectiles.Pets;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Pets
{
    public class ElderSignet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item2;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;
            item.value = Item.sellPrice(0, 5, 0, 0);

            item.noMelee = true;

            item.shoot = ProjectileType<AncientStellate>();
            item.buffType = BuffType<AncientStellateBuff>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elder Signet");
            Tooltip.SetDefault("Summons an ancient stellate to dance around you");
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
