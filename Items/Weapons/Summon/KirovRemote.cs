using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Summon
{  
    public class KirovRemote : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 34;
            item.knockBack = 3;

            item.noMelee = true;
            item.summon = true;

            item.mana = 10;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            item.UseSound = SoundID.Item44;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.shoot = mod.ProjectileType("KirovAirship");
            item.shootSpeed = 7f;
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kirov Remote");
            Tooltip.SetDefault("Summons a mini Kirov Airship to defend you");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddRecipeGroup("IronBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
