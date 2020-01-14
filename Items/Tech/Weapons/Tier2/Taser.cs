using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier2
{
    public class Taser : ModItem
    {
        public float charge = 0;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 7;
            item.knockBack = 1.5f;
            item.GetGlobalItem<ItemEnergy>().energy = 3;

            item.useAnimation = 16;
            item.useTime = 8;
            item.useStyle = 5;

            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 2;

            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("TaserLightning");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Taser");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"), 1, -0.35f);

            int velocity = (int)player.velocity.X;
            if (velocity < 0)
            {
                velocity *= -1;
            }

            Vector2 vector94 = new Vector2(speedX, speedY);
            float ai = (float)Main.rand.Next(100);
            Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 4f;
            Projectile.NewProjectile(position.X, position.Y, vector95.X, vector95.Y, mod.ProjectileType("TaserLightning"), damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);

            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ElementsAwoken:EvilBar", 8);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
