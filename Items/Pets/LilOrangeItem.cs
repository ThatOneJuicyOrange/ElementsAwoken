using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Pets
{
    public class LilOrangeItem : ModItem
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

            item.rare = 11;
            item.value = Item.sellPrice(0, 1, 0, 0);

            item.noMelee = true;

            item.shoot = mod.ProjectileType("LilOrange");
            item.buffType = mod.BuffType("LilOrange");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lil' Orange");
            Tooltip.SetDefault("Summons a little Orange to follow you");
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
