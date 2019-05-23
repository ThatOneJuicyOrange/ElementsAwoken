using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    public class Fireblast : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 86;
            item.mana = 6;

            item.reuseDelay = 16;
            item.useAnimation = 15;
            item.useTime = 5;
            item.useStyle = 5;

            item.noMelee = true;
            item.magic = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;
            item.knockBack = 2.25f;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item66;
            item.shoot = mod.ProjectileType("Fireblast");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireblast");
            Tooltip.SetDefault("Fires ancient cultist fireballs");
        }


    }
}
