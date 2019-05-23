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

namespace ElementsAwoken.Items.Youtuber
{
    public class PedguinsStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 15;
            item.mana = 10;
            item.knockBack = 1.25f;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 2;

            item.shoot = mod.ProjectileType("CorruptPenguin");
            item.shootSpeed = 10f;

            item.GetGlobalItem<EATooltip>().youtuber = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pedguin's Staff");
            Tooltip.SetDefault("Summons a corrupt penguin\nPedguin's magic allows it to fly!\nPedguins's Youtuber item");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PenguinFeather", 4);
            recipe.AddIngredient(ItemID.BorealWood, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
