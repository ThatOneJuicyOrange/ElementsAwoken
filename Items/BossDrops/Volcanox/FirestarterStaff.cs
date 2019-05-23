using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Volcanox
{  
    public class FirestarterStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 250;
            item.summon = true;
            item.mana = 10;
            item.width = 26;
            item.height = 28;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 80, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("VolcanicTentacle");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firestarter Staff");
            Tooltip.SetDefault("Summons a Volcanic Tentacle to serve you");
        }
    }
}
