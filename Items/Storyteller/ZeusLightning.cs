using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class ZeusLightning : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 155;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;
            item.mana = 20;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ZeusLightningBolt");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zeus' Thunder Bolt");
        }
    }
}
