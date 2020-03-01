using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class EternitysEnd : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 64;

            item.damage = 70;
            item.knockBack = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 4;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("EternityArrow");
            item.shootSpeed = 26f;
            item.useAmmo = 40;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .60f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternity's End");
            Tooltip.SetDefault("Turns normal arrows into eternity arrows\nHas a chance to fire a laser\n60% chance not to consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int addPosition = Main.rand.Next(-20, 8);
            if (type == 1) // The normal arrow
            {
                type = mod.ProjectileType("EternityArrow");
            }
            Projectile.NewProjectile(position.X + addPosition, position.Y + addPosition, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            if (Main.rand.Next(10) == 0)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EternityBeam"), (int)(damage * 1.5f), knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Phantasm, 1);
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
