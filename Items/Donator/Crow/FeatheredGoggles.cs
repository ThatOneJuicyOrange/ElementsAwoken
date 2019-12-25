using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Crow
{
    public class FeatheredGoggles : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item79;
            item.noMelee = true;
            item.mountType = mod.MountType("CrowMount");

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feathered Goggles");
            Tooltip.SetDefault("Summons a ridable crow mount\nCrow's donator item");
        }
    }
}