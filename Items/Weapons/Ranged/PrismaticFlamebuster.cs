using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class PrismaticFlamebuster : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 89;
            item.ranged = true;
            item.width = 42;
            item.height = 16;
            item.useTime = 3;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 10;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("PrismaticFire");
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Gel;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Flamebuster");
            Tooltip.SetDefault("Fires rainbow flames\n80% chance to not consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num6 = Main.rand.Next(2 , 3);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speedY + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(3))
                {
                    case 0: type = mod.ProjectileType("PrismaticFire"); break;
                    default: break;
                }
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .8f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddIngredient(ItemID.RainbowBrick, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
