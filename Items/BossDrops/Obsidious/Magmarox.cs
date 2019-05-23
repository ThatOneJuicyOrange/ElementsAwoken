using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    public class Magmarox : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 50;
            item.knockBack = 2;

            item.autoReuse = true;
            item.useTurn = true;
            item.melee = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("MagmaroxRock");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmarox");
        }
    }
}
