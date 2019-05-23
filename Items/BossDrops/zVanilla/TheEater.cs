using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class TheEater : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 15;
            item.channel = true;
            item.ranged = true;
            item.width = 54;
            item.height = 28;
            item.scale = 1.1f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.05f;
            item.value = Item.buyPrice(0, 1, 50, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item10;
            item.autoReuse = true;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("MiniEaterOfSouls");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Eater");
            Tooltip.SetDefault("Chomp Chomp");
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
