using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Ancients
{
    public class Chromacast : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 550;
            item.knockBack = 2;
            item.mana = 9;

            item.useStyle = 5;
            item.useTime = 18;
            item.useAnimation = 18;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 75, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("ChromacastBall");
            item.shootSpeed = 22f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chromacast");
            Tooltip.SetDefault("Deals more damage the closer you are to the enemy\nDont get too close to the explosion");
        }
    }
}
