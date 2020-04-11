using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Carapace
{
    public class CarapaceShiv : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;

            item.damage = 14;
            item.knockBack = 1;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.UseSound = SoundID.Item1;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 0, 1, 50);
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carapace Shiv");
            Tooltip.SetDefault("Right Click leap towards the cursor");
        }

        public override bool AltFunctionUse(Player player)
        {
            if (player.velocity.Y == 0)
            {
                return true;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 toMouse = Main.MouseWorld - player.Center;
                toMouse.Normalize();
                toMouse *= 10f;
                if ((toMouse.X > 0 && player.velocity.X < 5) || (toMouse.X < 0 && player.velocity.X > -5)) player.velocity += new Vector2(toMouse.X, toMouse.Y);
            }
            else
            {

            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
