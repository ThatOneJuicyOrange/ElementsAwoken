using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier5
{
    public class PrototypeX51 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 34;
            item.knockBack = 2;
            item.GetGlobalItem<ItemEnergy>().energy = 15;

            item.noMelee = true;
            item.autoReuse = true;
            item.magic = true;

            item.useTime = 42;
            item.useAnimation = 42;
            item.useStyle = 5;
            item.UseSound = SoundID.Item15;

            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 6;

            item.mana = 9;
            item.shoot = ProjectileID.ElectrosphereMissile;
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prototype X-51");
            Tooltip.SetDefault("Fires an electrosphere");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage = 19;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 1);
            recipe.AddIngredient(null, "Transistor", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 1);
            recipe.AddIngredient(null, "Transistor", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
