using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Chamcham
{
    public class PinkShoe : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item2;

            item.rare = 4;
            item.value = Item.sellPrice(0, 4, 0, 0);

            item.noMelee = true;

            item.shoot = mod.ProjectileType("WyvernHead");
            item.buffType = mod.BuffType("WyvernPetBuff");

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Shoe");
            Tooltip.SetDefault("Summons a baby wyvern\nChamcham's donator item");
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ItemID.Silk, 1);
            recipe.AddIngredient(ItemID.PinkGel, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
