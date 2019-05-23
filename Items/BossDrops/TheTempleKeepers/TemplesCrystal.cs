using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class TemplesCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 28;

            item.ranged = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.damage = 86;
            item.knockBack = 4;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("TempleBeam");
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple's Crystal");
        }
    }
}
