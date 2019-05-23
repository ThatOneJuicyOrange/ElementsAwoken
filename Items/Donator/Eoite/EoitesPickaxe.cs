using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Eoite
{
    public class EoitesPickaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 60;

            item.damage = 85;
            item.knockBack = 4.5f;

            item.useTime = 5;
            item.useAnimation = 12;
            item.useStyle = 1;

            item.useTurn = true;
            item.autoReuse = true;
            item.melee = true;

            item.pick = 240;
            item.tileBoost += 5;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item1;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meaningful Weeb");
            Tooltip.SetDefault("Swing it like you mean it");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 6);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 100);
        }
    }
}
