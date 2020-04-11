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
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class PutridRipperTome : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;

            item.damage = 60;
            item.knockBack = 3.25f;
            item.mana = 10;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item113;
            item.shoot = mod.ProjectileType("PutridRipper");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Ripper Tome");
            Tooltip.SetDefault("Summons a putrid ripper to protect you\nEach putrid ripper's takes up 1.5 minion slots");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
