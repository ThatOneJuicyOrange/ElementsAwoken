using System;
using ElementsAwoken.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

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

            item.damage = 34;
            item.mana = 10;
            item.knockBack = 3;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;

            item.shoot = ProjectileType<HearthMinion>();
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scepter of Warmth");
            Tooltip.SetDefault("Summons a hearth to defend you\nHearth's take 2 minion slots");
        }
    }
}
