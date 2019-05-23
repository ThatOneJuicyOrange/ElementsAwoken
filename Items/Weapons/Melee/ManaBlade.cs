using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class ManaBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 5;
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
            DisplayName.SetDefault("Mana Blade");
            Tooltip.SetDefault("Consumes some mana and adds it to the damage");
        }
        public override bool UseItem(Player player)
        {
            int rand = Main.rand.Next(5, 15);
            player.statMana -= rand;

            int newDamage = 5 + rand;
            item.damage = newDamage;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddRecipeGroup("SilverSword");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
