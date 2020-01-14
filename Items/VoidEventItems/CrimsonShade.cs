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
            item.width = 50;
            item.height = 50; 
            
            item.damage = 69;
            item.mana = 6;
            item.knockBack = 2;

            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 5;

            Item.staff[item.type] = true;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("CrimsonShadeP");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Shade");
        }
    }
}
