using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{
    public class Stinger : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 21;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 3;
            item.reuseDelay = 16;
            item.useAnimation = 9;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 2;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WastelandStingerFriendly");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stinger");
        }
    }
}
