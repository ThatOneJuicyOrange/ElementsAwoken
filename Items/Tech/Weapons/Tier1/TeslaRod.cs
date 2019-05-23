using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier1
{
    public class TeslaRod : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 15;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 2;

            item.magic = true;
            item.useTurn = true;
            item.autoReuse = false;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("TeslaSpark");
            item.shootSpeed = 7f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"), 1, -0.35f);
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tesla Rod");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "CopperWire", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
