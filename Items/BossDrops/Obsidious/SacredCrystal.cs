using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    public class SacredCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;
            item.expert = true;

            item.UseSound = SoundID.Item79;
            item.noMelee = true;
            item.mountType = mod.MountType("SacredCrystalMount");
            item.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 200);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Crystal");
            Tooltip.SetDefault("Greed drives even the strongest minds to madness");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 200);
        }
    }
}