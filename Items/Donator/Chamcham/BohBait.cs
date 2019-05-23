using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Chamcham
{
    public class BohBait : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.damage = 0;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item2;

            item.rare = 2;
            item.value = Item.sellPrice(0, 4, 0, 0);

            item.noMelee = true;

            item.shoot = mod.ProjectileType("ChamchamRat");
            item.buffType = mod.BuffType("ChamchamRatBuff");

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boh Bait");
            Tooltip.SetDefault("Chamcham's donator item");
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
            recipe.AddIngredient(ItemID.Feather, 5);
            recipe.AddIngredient(ItemID.FishingSeaweed, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
