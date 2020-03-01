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

namespace ElementsAwoken.Items.Weapons.Summon
{
    public class EnchantedStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 62;

            item.damage = 36;
            item.knockBack = 2f;
            item.mana = 10;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 5;

            item.shoot = mod.ProjectileType("EnchantedTrio0");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Staff");
            Tooltip.SetDefault("Summons an enchanted sword, cursed hammer and crimson axe to fight for you");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
