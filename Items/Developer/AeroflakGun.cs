using System;
using System.Collections.Generic;
using ElementsAwoken.Items.BossDrops.Azana;
using ElementsAwoken.Items.ItemSets.Drakonite.Refined;
using ElementsAwoken.Items.ItemSets.Drakonite.Regular;
using ElementsAwoken.Items.ItemSets.Mortemite;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Items.ItemSets.Stellarium;
using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Items.Tech.Weapons.Tier6;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Developer
{
    public class AeroflakGun : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26; 
            
            item.damage = 900;
            item.knockBack = 3.5f;

            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.GetGlobalItem<EATooltip>().developer = true;

            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;

            item.shootSpeed = 12f;
            item.shoot = ProjectileType<AeroflakP>();
            item.GetGlobalItem<ItemEnergy>().energy = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyperlight Sentinel");
            Tooltip.SetDefault("Wait a minute, tiers are back?\nExplodes when in range of a flying enemy\nHitting multiple flying enemies in a short period of time will deal extra damage\nMayhemm's developer weapon");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item91, (int)player.position.X, (int)player.position.Y); 
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 120f;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            float damageScale = MathHelper.Lerp(1, 2, MathHelper.Clamp((float)modPlayer.aeroflakHits / 10,0,1));
            Projectile.NewProjectile(position.X, position.Y , speedX, speedY, type, (int)(damage * damageScale), knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-26, -2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Railgun>(), 1);
            recipe.AddIngredient(ItemType<LRM>(), 1);
            recipe.AddIngredient(ItemType<Drakonite>(), 8);
            recipe.AddIngredient(ItemType<RefinedDrakonite>(), 8);
            recipe.AddIngredient(ItemType<MortemiteDust>(), 8);
            recipe.AddIngredient(ItemType<StellariumBar>(), 8);
            recipe.AddIngredient(ItemType<Radia>(), 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
