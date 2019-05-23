using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ScourgeFighter
{
    public class ScourgeFighterMachineGun : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 28;

            item.damage = 30;
            item.knockBack = 3.75f;

            item.noMelee = true;
            item.autoReuse = true;
            item.ranged = true;

            item.useTime = 2;
            item.reuseDelay = 10;
            item.useAnimation = 6;
            item.useStyle = 5;
            item.UseSound = SoundID.Item31;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;

            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = 97;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Machine Gun");
            Tooltip.SetDefault("Ripped off the remains of the Scourge Fighter");
        }
    }
}
