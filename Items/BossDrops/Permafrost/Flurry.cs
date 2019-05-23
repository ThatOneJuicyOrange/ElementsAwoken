using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{  
    public class Flurry : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 42;
            item.summon = true;
            item.mana = 10;
            item.width = 26;
            item.height = 28;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 46, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("IcicleMinion");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flurry");
            Tooltip.SetDefault("Summons an icicle to fight for you");
        }
    }
}
