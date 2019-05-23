using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Artifacts
{
    public class FrozenGauntlet : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 7;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Gauntlet");
            Tooltip.SetDefault("Inflicts chilled and frostburn on attack\nStars fall from the sky when injured\nIncreased invincibility time\nDamage increased by 15%");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 4));
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                    if (slot != i && player.armor[i].type == mod.ItemType("ElementalArcanum"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            modPlayer.frozenGauntlet = true;
            player.longInvince = true;
            player.starCloak = true;
            player.meleeDamage *= 1.15f;
            player.rangedDamage *= 1.15f;
            player.magicDamage *= 1.15f;
            player.minionDamage *= 1.15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlowingSlush", 1);
            recipe.AddIngredient(ItemID.FireGauntlet, 1);
            recipe.AddIngredient(ItemID.StarVeil, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
