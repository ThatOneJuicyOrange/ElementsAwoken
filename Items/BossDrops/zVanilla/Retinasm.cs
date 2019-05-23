using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class Retinasm : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Code2);
            item.useStyle = 5;
            item.damage = 58;
            item.width = 16;
            item.height = 16;
            item.rare = 4;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = 541;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("RetinasmP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Retinasm");
        }

    }
}
