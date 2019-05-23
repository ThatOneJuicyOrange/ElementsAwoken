using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class SkeletronFist : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 30;
            item.width = 30;
            item.height = 10;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 2;
            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 7.5f;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("SkeletronFistP");
            item.shootSpeed = 15.1f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skeletron Fist");
            Tooltip.SetDefault("Punch with the force of Skeletron");
        }

    }
}
