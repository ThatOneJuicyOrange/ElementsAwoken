using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Tiles.Crafting;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class LightMine : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 30;
            item.width = 28;

            item.damage = 350;
            item.knockBack = 3.5f;

            item.noMelee = true;
            item.autoReuse = true;
            item.magic = true;

            item.mana = 5;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.UseSound = SoundID.Item9;
            item.shoot = ProjectileType<RadiantStarMine>();
            item.shootSpeed = 1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cerise Curtains");
            Tooltip.SetDefault("Homing radiant stars appear around you");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(position + Main.rand.NextVector2Square(-400,400), Main.rand.NextVector2Square(-3, 3), type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Weapons.Magic.Tomes.FrostMine>());
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
