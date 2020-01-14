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
            item.width = 70;
            item.height = 70;
            
            item.damage = 67;
            item.knockBack = 5;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
            item.useAnimation = 18;
            item.useTime = 25;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("ArmageddonBlade");
            item.shootSpeed = 18f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armageddon");
            Tooltip.SetDefault("Shoots out blades of destruction");
        }
        public override bool OnlyShootOnSwing => true;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer,Main.rand.Next(3));
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
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddIngredient(ItemID.CursedFlame, 12);
            recipe.AddIngredient(ItemID.FrostCore, 3);
            recipe.AddIngredient(ItemID.LivingFireBlock, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
