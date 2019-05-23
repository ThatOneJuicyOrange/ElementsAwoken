using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class Fireweaver : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;

            item.damage = 36;
            item.mana = 18;
            item.knockBack = 5;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("FireweaverProj");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireweaver");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.LavaBucket, 2);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
