using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class Snowdrift : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 68;
            item.magic = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 14;
            item.useAnimation = 14;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 46, 0, 0);
            item.rare = 7;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SnowdriftBomb");
            item.shootSpeed = 14f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowdrift");
        }
    }
}
