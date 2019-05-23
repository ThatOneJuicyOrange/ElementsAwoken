using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Pets
{
    public class DynastyWhistle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.damage = 0;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Whistle");

            item.rare = 11;
            item.value = Item.sellPrice(0, 0, 5, 0);

            item.noMelee = true;

            item.shoot = mod.ProjectileType("TurboDoge");
            item.buffType = mod.BuffType("TurboDogeBuff");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynasty Whistle");
            Tooltip.SetDefault("Summons a Turbo Doge to follow you");
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
            recipe.AddIngredient(ItemID.DynastyWood, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
