using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class TheLeeched : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 13;
            item.magic = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 18;
            item.useTurn = true;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Leeched");
            Tooltip.SetDefault("Restores health on hit");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            player.statLife += 3;
            player.HealEffect(3);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddRecipeGroup("ElementsAwoken:SilverSword");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
