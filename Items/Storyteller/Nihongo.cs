using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Storyteller
{
    public class Nihongo : ModItem
    {
        public override void SetDefaults()
        {       
            item.damage = 42;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.useTime = 18;
            item.knockBack = 8.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 60;
            item.width = 60;
            item.maxStack = 1;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.shoot = mod.ProjectileType("NihongoP");
            item.shootSpeed = 9f;
            item.rare = 4;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nihongo");
            Tooltip.SetDefault("The ancient spear of Japan");
        }
    }
}
