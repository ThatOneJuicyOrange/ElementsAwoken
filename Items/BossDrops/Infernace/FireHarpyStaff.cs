using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Infernace
{  
    public class FireHarpyStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.summon = true;
            item.noMelee = true;

            item.damage = 27;
            item.mana = 10;
            item.knockBack = 3;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("FireHarpy");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Harpy Staff");
            Tooltip.SetDefault("Summons a fire harpy to fight for you");
        }
    }
}
