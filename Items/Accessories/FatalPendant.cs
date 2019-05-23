using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Accessories
{
    public class FatalPendant : ModItem
    {
        public int giveBuffCooldown = 0;
        public int buff = 0;

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 2;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fatal Pendant");
            Tooltip.SetDefault("Filled with the souls of tortured creatures\nGives the player buffs or debuffs randomly");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            giveBuffCooldown--;
            if (giveBuffCooldown <= 0)
            {
                int choice = Main.rand.Next(11);
                if (choice == 0)
                {
                    buff = BuffID.OnFire;
                }
                if (choice == 1)
                {
                    buff = BuffID.Poisoned;
                }
                if (choice == 2)
                {
                    buff = BuffID.Frostburn;
                }
                if (choice == 3)
                {
                    buff = BuffID.Dangersense;
                }
                if (choice == 4)
                {
                    buff = BuffID.Endurance;
                }
                if (choice == 5)
                {
                    buff = BuffID.Featherfall;
                }
                if (choice == 6)
                {
                    buff = BuffID.Heartreach;
                }
                if (choice == 7)
                {
                    buff = BuffID.Ironskin;
                }
                if (choice == 8)
                {
                    buff = BuffID.Rage;
                }
                if (choice == 9)
                {
                    buff = BuffID.Swiftness;
                }
                if (choice == 10)
                {
                    buff = BuffID.Wrath;
                }
                player.AddBuff(buff, Main.rand.Next(1000, 1600));
                giveBuffCooldown = 1500 + Main.rand.Next(0, 200);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddIngredient(ItemID.PlatinumBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
