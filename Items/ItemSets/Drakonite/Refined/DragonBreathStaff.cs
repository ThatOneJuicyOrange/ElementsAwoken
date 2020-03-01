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

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    public class DragonBreathStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 50;
            item.knockBack = 1.25f;
            item.mana = 20;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item113;
            item.shoot = mod.ProjectileType("MiniDragon");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Breath Staff");
            Tooltip.SetDefault("Summons a mini dragon to protect you");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
