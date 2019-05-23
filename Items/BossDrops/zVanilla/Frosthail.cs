using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    public class Frosthail : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 75;
            item.mana = 6;
            item.knockBack = 2.25f;

            item.magic = true;
            item.noMelee = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item120;
            item.shoot = mod.ProjectileType("IceMist");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frosthail");
            Tooltip.SetDefault("Fires ice mist");
        }

    }
}
