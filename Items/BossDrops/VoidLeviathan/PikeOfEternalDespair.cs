using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    class PikeOfEternalDespair : ModItem
    {
        public override void SetDefaults()
        {       
            item.width = 66;
            item.height = 66;

            item.damage = 193;
            item.knockBack = 4f;

            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useAnimation = 19;
            item.useStyle = 5;
            item.useTime = 19;
            item.UseSound = SoundID.Item1;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = mod.ProjectileType("PikeOfEternalDespairP");
            item.shootSpeed = 11f;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pike of Eternal Despair");
        }
    }
}
