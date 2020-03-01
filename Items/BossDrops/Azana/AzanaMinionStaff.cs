using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using ElementsAwoken.Projectiles.Minions;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class AzanaMinionStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 62;

            item.damage = 400;
            item.knockBack = 2f;
            item.mana = 10;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;

            item.summon = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.UseSound = SoundID.Item44;
            item.shoot = ModContent.ProjectileType<InfectionMouthMinion>();
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vow of Malaise");
            Tooltip.SetDefault("Summons an infection mouth to fight with you");
        }
    }
}
