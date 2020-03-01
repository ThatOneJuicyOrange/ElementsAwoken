using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class GleamOfAnnhialation : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 360;
            item.knockBack = 2;
            item.mana = 5;

            item.useStyle = 5;
            item.useTime = 11;
            item.useAnimation = 11;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("AzanaNanoBolt");
            item.shootSpeed = 14f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Virulent Gleam");
        }
    }
}
