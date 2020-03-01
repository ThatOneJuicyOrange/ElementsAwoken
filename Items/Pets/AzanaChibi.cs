using ElementsAwoken.Projectiles.Thrown;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Pets
{
    public class AzanaChibi : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;

            item.noMelee = true;
            item.noUseGraphic = true;
            item.consumable = true;

            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = 1;
            item.useTime = 12;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 0, 0, 5);
            item.rare = 0;

            item.shoot = ModContent.ProjectileType<ChaosTomatoP>();
            item.shootSpeed = 4f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Tomato");
            Tooltip.SetDefault("now you have a slightly angry tomato, what of it?\nWill be replaced with a different item later");
        }
    }
}
