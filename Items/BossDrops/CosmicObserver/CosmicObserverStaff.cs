using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{  
    public class CosmicObserverStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 35;
            item.mana = 10;
            item.knockBack = 3;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.summon = true;
            item.noMelee = true;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("CosmicObserver");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Observer Staff");
            Tooltip.SetDefault("Summons a miniature cosmic observer to fight for you");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CosmicShard", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
