using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Accessories
{
    public class EnergyGeode : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 5;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Geode");
            Tooltip.SetDefault("Mana increased by 60\nMagic damage increased by 10%\n8% increased movement speed\nImmunity to most debuffs");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 60;
            player.magicDamage *= 1.1f;
            player.moveSpeed *= 1.08f;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IllusiveCharm", 1);
            recipe.AddIngredient(ItemID.CrystalShard, 14);
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
