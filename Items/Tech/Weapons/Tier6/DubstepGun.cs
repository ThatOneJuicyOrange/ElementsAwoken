using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Items.Tech.Weapons.Tier6
{
    public class DubstepGun : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 34;

            item.damage = 110;
            item.knockBack = 3.75f;
            item.GetGlobalItem<ItemEnergy>().energy = 3;

            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.UseSound = SoundID.Item15;

            item.noMelee = true;
            item.autoReuse = false;
            item.ranged = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;

            item.shoot = mod.ProjectileType("DubstepGunHeld");
            item.shootSpeed = 20f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dubstep Gun");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BassBooster", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "CopperWire", 15);
            recipe.AddIngredient(null, "SiliconBoard", 1);
            recipe.AddIngredient(null, "Microcontroller", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
