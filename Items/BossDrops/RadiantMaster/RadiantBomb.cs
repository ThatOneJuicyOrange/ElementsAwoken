using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.RadiantMaster
{
    public class RadiantBomb : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.damage = 380;
            item.knockBack = 3f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.shoot = ProjectileType<RadiantBombP>();
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master's Vortex");
        }
    }
}
