using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Testing
{
    public class WispTest : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;

            item.UseSound = SoundID.Item79;
            item.noMelee = true;
            item.mountType = mod.MountType("WispForm");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("wispt");
            Tooltip.SetDefault("jobli");
        }
    }
}