using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class DragonsBreath : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 27;
            item.ranged = true;
            item.width = 42;
            item.height = 16;
            item.useTime = 4;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DragonFire");
            item.shootSpeed = 5f;
            item.useAmmo = AmmoID.Gel;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Breath");
            Tooltip.SetDefault("75% chance to not consume ammo");
        }


        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .75f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num6 = Main.rand.Next(1, 2);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-20, 20) * 0.05f;
                float SpeedY = speedY + (float)Main.rand.Next(-20, 20) * 0.05f;
                switch (Main.rand.Next(3))
                {
                    case 0: type = mod.ProjectileType("DragonFire"); break;
                    default: break;
                }
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
