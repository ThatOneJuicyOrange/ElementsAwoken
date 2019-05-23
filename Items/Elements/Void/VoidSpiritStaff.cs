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

namespace ElementsAwoken.Items.Elements.Void
{
    public class VoidSpiritStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 146;
            item.mana = 10;
            item.width = 50;
            item.height = 50;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 1.25f;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.useAnimation = 25;
            item.UseSound = SoundID.Item113;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AbyssCultist");
            item.shootSpeed = 10f;
            item.summon = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyss Cultist Staff");
            Tooltip.SetDefault("Summons a Abyss Cultist to protect you\nCultists take 5 minion slots");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
