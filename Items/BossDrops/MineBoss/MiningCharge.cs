using System;
using System.Collections.Generic;
using ElementsAwoken.Items.ItemSets.ScarletSteel;
using ElementsAwoken.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.MineBoss
{
    public class MiningCharge : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;
            item.noUseGraphic = true;

            item.knockBack = 8f;
            item.damage = 60;
            item.maxStack = 999;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 1;

            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 0, 1, 0);

            item.shoot = ProjectileType<MiningChargeP>();
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Charge");
            Tooltip.SetDefault("A small explosion that will not destroy tiles\nReleases an explosion of deadly sparks");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<ScarletSteel>(), 1);
            recipe.AddIngredient(ItemID.Grenade, 33);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 33);
            recipe.AddRecipe();
        }
    }
}
