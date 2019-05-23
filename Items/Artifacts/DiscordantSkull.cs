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
    public class DiscordantSkull : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Skull");
            Tooltip.SetDefault("30% increased pick speed\n4% increased damage\nReduces damage taken by 5%");
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
            player.pickSpeed -= 0.3f;
            player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;
            player.endurance += 0.05f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordantAmber", 1);
            recipe.AddIngredient(ItemID.SiltBlock, 100);
            recipe.AddIngredient(ItemID.CobaltShield, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
