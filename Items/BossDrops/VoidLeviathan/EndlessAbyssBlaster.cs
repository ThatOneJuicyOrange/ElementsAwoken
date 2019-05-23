using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class EndlessAbyssBlaster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 28;

            item.damage = 300;
            item.knockBack = 2;

            item.useTime = 90;
            item.useAnimation = 90;
            item.useStyle = 5;
            item.UseSound = SoundID.Item92;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = mod.ProjectileType("BlackholeCreator");
            item.shootSpeed = 20f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Singularity Mortar");
        }
    }
}
