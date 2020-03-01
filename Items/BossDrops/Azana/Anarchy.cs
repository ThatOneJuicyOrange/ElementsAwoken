using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class Anarchy : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;

            item.damage = 200;
            item.knockBack = 2.5f;

            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.UseSound = SoundID.Item1;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("AnarchyP");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Contamination");
            Tooltip.SetDefault("Fires chaos bolts everywhere, while occasionally releasing chaos waves");
        }
    }
}
