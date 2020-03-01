using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Neck)]
    public class DarknersScarf : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 11;

            item.damage = 40;
            item.knockBack = 4f;

            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;

            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.UseSound = SoundID.Item1;

            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = false;
            item.noMelee = true;
            item.vanity = true;
            item.accessory = true;

            item.shoot = mod.ProjectileType("DarknersScarfP");
            item.shootSpeed = 15f;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkner's Scarf");
            Tooltip.SetDefault("Whoever had this before you didn’t like fighting, but they did anything to help\nCan also be worn as a vanity\nLantard's donator item");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 25);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, ai3);
            return false;
        }
    }
}
