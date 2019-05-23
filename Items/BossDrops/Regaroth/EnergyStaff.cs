using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{  
    public class EnergyStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 42;
            item.mana = 10;
            item.knockBack = 3;

            item.summon = true;
            item.noMelee = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("EnergySpirit");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Staff");
            Tooltip.SetDefault("Summons an energy spirit to fight for you");
        }
    }
}
