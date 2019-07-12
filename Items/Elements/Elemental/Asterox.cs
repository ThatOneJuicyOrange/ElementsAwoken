using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    [AutoloadEquip(EquipType.Shield)]

    public class Asterox : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.accessory = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asterox");
            Tooltip.SetDefault("Grants immunity to knockback.\n12 defense\nIncreased armor penetration and life regen\nCreates a damaging shield around the player\n20% increased movement speed");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.statDefense += 12;
            player.armorPenetration += 5;
            player.lifeRegen += 3;
            player.moveSpeed *= 1.2f;
            player.panic = true;
            if(player.ownedProjectileCounts[mod.ProjectileType("AsteroxShieldBase")] < 1)
            {
                Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, mod.ProjectileType("AsteroxShieldBase"), 50, 10f, player.whoAmI, 0.0f, 0.0f);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
