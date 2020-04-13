using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{  
    public class ChitinStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            
            item.damage = 18;
            item.mana = 10;
            item.knockBack = 3;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.noMelee = true;
            item.summon = true;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 2;

            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("ScorpionMinion");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mother's Magic");
            Tooltip.SetDefault("Summons a scorpion to fight for you");
        }
    }
}
