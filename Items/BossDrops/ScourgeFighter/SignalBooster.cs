using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ScourgeFighter
{
    public class SignalBooster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 40;
            item.mana = 6;
            item.knockBack = 6f;

            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = 5;

            item.noMelee = true;
            item.magic = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;

            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("SignalBoost");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Signal Booster");
        }
    }
}
