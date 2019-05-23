using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.GameContent.Events;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class SandstormStone : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.maxStack = 1;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 3;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item66;
            item.consumable = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Sandstorm Stone");
      Tooltip.SetDefault("Toggles the sandstorm\nOnly usable inside of the desert");
    }


        public override bool CanUseItem(Player player)
        {
            if (player.ZoneDesert)
            {
                return true;
            }
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (Sandstorm.Happening == true)
            {
                Main.NewText("The sand storm settles...", 227, 200, 93, false);
                Sandstorm.Happening = false;
                Sandstorm.TimeLeft = 0;
                SandstormStuff();
                return true;
            }
            else if (Sandstorm.Happening == false)
            {
                Main.NewText("The desert winds pick up!", 227, 200, 93, false);
                Sandstorm.Happening = true;
                Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
                SandstormStuff();
                return true;
            }
            return false;
        }

        public static void SandstormStuff()
        {
            Sandstorm.IntendedSeverity = !Sandstorm.Happening ? (Main.rand.Next(3) != 0 ? Main.rand.NextFloat() * 0.3f : 0.0f) : 0.4f + Main.rand.NextFloat();
            if (Main.netMode == 1)
                return;
            //NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("SandGroup", 25);
            recipe.AddRecipeGroup("SandstoneGroup", 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
