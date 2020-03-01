using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    public class TheDestroyer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 34;

            item.damage = 60;
            item.knockBack = 3.75f;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item15;

            item.noMelee = true;
            item.autoReuse = false;
            item.ranged = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("TheDestroyerHeld");
            item.shootSpeed = 20f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Destroyer");
            Tooltip.SetDefault("Hold Left Click to charge the laser");
        }

    }
}
