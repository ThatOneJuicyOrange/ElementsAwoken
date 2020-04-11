using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Lantard
{
    public class SufferWithMe : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 45, 0, 0);

            item.accessory = true;

            item.defense = 5;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suffer With Me");
            Tooltip.SetDefault("You took a part of me, so I'll take a part of you\nGrants immunity to Chaotic Necrosis\nAttacks have a 25% to inflict Chaotic Necrosis\nIncreases armor penetration by 10\nLantard's donator item");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.sufferWithMe = true;

            player.buffImmune[mod.BuffType("ChaosBurn")] = true;

            player.armorPenetration += 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SharktoothShackle", 1);
            recipe.AddIngredient(null, "EntropicCoating", 1);
            recipe.AddIngredient(null, "DiscordantBar", 10);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
