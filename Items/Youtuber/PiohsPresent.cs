using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Youtuber
{
    public class PiohsPresent : ModItem
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
            item.mountType = mod.MountType("ElementalDragonBunny");

            item.GetGlobalItem<EATooltip>().youtuber = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pioh's Present");
            Tooltip.SetDefault("Summons a ridable Elemental Dragon Bunny\nBabeElena's Youtuber item");
        }
    }
}