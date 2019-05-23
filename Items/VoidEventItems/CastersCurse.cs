using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.VoidEventItems
{
    public class CastersCurse : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 118;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 12;
            item.useAnimation = 12;
            item.mana = 6;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 40, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item13;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("CastersCurseBoltBase");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Caster's Curse");
        }
    }
}
