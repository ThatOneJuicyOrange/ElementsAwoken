using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    public class Ultramarine : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 67;
            item.knockBack = 3.5f;

            item.noMelee = true;
            item.autoReuse = true;
            item.ranged = true;

            item.useAnimation = 28;
            item.useTime = 28;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;

            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("UltramarineBeam");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultramarine");
        }
    }
}
