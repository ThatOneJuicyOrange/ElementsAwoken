using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class EpicGun : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 5000;
            item.knockBack = 3.5f;

            item.useAnimation = 13;
            item.useTime = 13;
            item.useStyle = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item12;
            item.shootSpeed = 50f;
            item.shoot = 10;
            item.GetGlobalItem<EATooltip>().testing = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Epic Gun");
            Tooltip.SetDefault("fortnite once a fortnight");
        }
    }
}
