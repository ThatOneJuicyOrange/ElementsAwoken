using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class Mjolnir : ModItem
    {

        public override void SetDefaults()
        {
            item.height = 32;
            item.width = 32;

            item.damage = 46;
            item.knockBack = 10f;

            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.thrown = true;

            item.useAnimation = 22;
            item.useTime = 22;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("MjolnirP");
            item.shootSpeed = 28f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mjolnir");
            Tooltip.SetDefault("Shoots lightning out at enemies");
        }
    }
}
