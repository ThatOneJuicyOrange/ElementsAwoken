using System;
using System.Collections.Generic;
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Items.Tech.Weapons.Tier2;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier6
{
    public class ParticleAccelerator : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26; 
            
            item.damage = 70;
            item.knockBack = 3.5f;

            item.useAnimation = 40;
            item.useTime = 40;
            item.useStyle = 5;
            item.UseSound = SoundID.Item96;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;

            item.shootSpeed = 10f;
            item.shoot = ProjectileType<Particles>();
            item.GetGlobalItem<ItemEnergy>().energy = 8;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Particle Accelerator");
            Tooltip.SetDefault("Shoots a cloud of particles");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-26, -2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
