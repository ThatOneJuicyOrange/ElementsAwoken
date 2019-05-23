using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Shield)]

    public class DesertShield : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 36;
            item.width = 24;
            item.height = 28;
            item.rare = 3;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.accessory = true;
            item.defense = 4;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield of The Desert");
            Tooltip.SetDefault("Allows the player to perform a dash\nDashing throws a sand tornado the way you dashed");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.dash = 2;
            if (player.eocDash == 14 && player.ownedProjectileCounts[mod.ProjectileType("Shieldnado")] <= 0)
            {
                int speed = 7;
                if (player.direction == -1)
                {
                    speed = -7;
                }
                Projectile.NewProjectile(player.Center.X, player.Center.Y, speed, Main.rand.NextFloat(-1.5f, 1.5f), mod.ProjectileType("Shieldnado"), 10, 5f, player.whoAmI);
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("SandGroup", 25);
            recipe.AddRecipeGroup("SandstoneGroup", 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
