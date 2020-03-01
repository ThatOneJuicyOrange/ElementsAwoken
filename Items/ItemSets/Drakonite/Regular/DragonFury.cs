using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Regular
{
    public class DragonFury : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            
            item.damage = 16;
            item.mana = 5;
            item.knockBack = 2;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = false;

            item.UseSound = SoundID.Item20;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("DrakoniteFireball");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fury");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Drakonite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
