using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{  
    public class EyeballStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 52;
            item.knockBack = 3;
            item.mana = 10;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.summon = true;
            item.noMelee = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("EyeballMinion");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Staff");
            Tooltip.SetDefault("Summons a celestial watcher to serve you");
        }
    }
}
