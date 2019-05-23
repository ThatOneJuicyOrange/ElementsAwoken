using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.VoidEventItems
{
    public class CrimsonShade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 69;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 6;
            item.useAnimation = 6;
            item.mana = 6;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 40, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item13;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("CrimsonShadeP");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Shade");
        }
    }
}
