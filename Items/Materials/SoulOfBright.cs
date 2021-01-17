using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class SoulOfBright : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Bright");
            Tooltip.SetDefault("'The essence of infernal creatures'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 11));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
