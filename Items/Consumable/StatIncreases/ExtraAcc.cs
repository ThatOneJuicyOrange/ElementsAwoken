using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.StatIncreases
{
    public class ExtraAcc : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 18;
            item.height = 18;
            item.useStyle = 4;
            item.useTime = 30;
            item.UseSound = SoundID.Item4;
            item.useAnimation = 30;
            item.rare = 11;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.expert = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Fruit");
            Tooltip.SetDefault("The Demon Heart allowed you to resist more of the conflicting energy in accessories, this strange fruit embraces it\nAllows you to survive wearing a 7th accessory\nFighters of The Calamity need not apply");
        }
        public override bool CanUseItem(Player player)
        {
            bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            return !calamityEnabled && !player.GetModPlayer<MyPlayer>().extraAccSlot && player.extraAccessorySlots == 1 && player.extraAccessory && Main.expertMode; // 1 is 6th accesory slot, also make player.extraAccessory
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<MyPlayer>().extraAccSlot = true;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordantBar", 25);
            recipe.AddIngredient(null, "VoidAshes", 12);
            recipe.AddIngredient(null, "VoidEssence", 15);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        }
    }
}
