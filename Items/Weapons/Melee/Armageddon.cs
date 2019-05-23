using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Armageddon : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 67;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 12;
            item.useTurn = true;
            item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ArmageddonBlast1");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armageddon");
            Tooltip.SetDefault("Shoots out waves of destruction");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            switch (Main.rand.Next(3))
            {
                case 0: type = mod.ProjectileType("ArmageddonBlast1"); break;
                case 1: type = mod.ProjectileType("ArmageddonBlast2"); break;
                case 2: type = mod.ProjectileType("ArmageddonBlast3"); break;
            }
            int projectile = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer);
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.Frostburn, 200);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.CursedFlame, 6);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
