using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Buildmonger
{
    public class BioReactor : ModItem
    {
        public float charge = 0;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 19;
            item.knockBack = 1.5f;

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = 5;
            item.UseSound = SoundID.Item34;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 2;

            item.shootSpeed = 20f;
            item.useAmmo = ItemID.JungleSpores;
            item.shoot = mod.ProjectileType("BioLightning");

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bio Reactor");
            Tooltip.SetDefault("Uses jungle spores as ammunition\nThe Buildmonger's donator item");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"), 1, -0.6f);

            int velocity = (int)player.velocity.X;
            if (velocity < 0)
            {
                velocity *= -1;
            }

            Vector2 vector94 = new Vector2(speedX, speedY);
            float ai = (float)Main.rand.Next(100);
            Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(8)) * 4f;
            Projectile.NewProjectile(position.X, position.Y, vector95.X, vector95.Y, mod.ProjectileType("BioLightning"), damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);

            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("EvilBar", 8);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Stinger, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
