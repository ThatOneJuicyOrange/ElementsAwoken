using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    public class VioletEdge : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 62;
            item.mana = 6;
            item.knockBack = 2.25f;

            item.reuseDelay = 16;
            item.useAnimation = 18;
            item.useTime = 6;
            item.useStyle = 5;

            item.noMelee = true;
            item.magic = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;

            item.UseSound = SoundID.Item66;
            item.shoot = mod.ProjectileType("VioletEdgeBall");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Violet Edge");
        }
    }
}
