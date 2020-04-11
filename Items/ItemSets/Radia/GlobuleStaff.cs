using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Tiles.Crafting;
using ElementsAwoken.Projectiles.Whips;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.Minions;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class GlobuleStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 320;
            item.mana = 10;
            item.knockBack = 5;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.noMelee = true;
            item.summon = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.UseSound = SoundID.Item44;
            item.shoot = ProjectileType<GlobuleMinion>();
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Crook");
            Tooltip.SetDefault("Summons a rare Starlight Globule variant to fight for you\nStarlight Globules take 2 minion slots");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
