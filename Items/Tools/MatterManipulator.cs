using ElementsAwoken.Items.BossDrops.Azana;
using ElementsAwoken.Items.Chaos;
using ElementsAwoken.Tiles.Crafting;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tools
{
    public class MatterManipulator : ModItem
    {
        private int rightCD = 0;
        public override bool CloneNewInstances => true;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;

            item.damage = 40;
            item.knockBack = 6;

            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = 5;

            item.melee = true;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.pick = 300;
            item.axe = 65;
            item.tileBoost += 30;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("MatterManipulator");
            item.shootSpeed = 40f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Matter Manipulator");
            Tooltip.SetDefault("Right Click to change the area dig size");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (rightCD <= 0)
                {
                    ItemsGlobal modItem = item.GetGlobalItem<ItemsGlobal>();
                    if (modItem.miningRadius == 0)
                    {
                        modItem.miningRadius = 1;
                        CombatText.NewText(player.getRect(), new Color(0, 150, 191), "3x3", true, false);
                    }
                    else if (modItem.miningRadius == 1)
                    {
                        modItem.miningRadius = 2;
                        CombatText.NewText(player.getRect(), new Color(0, 80, 138), "5x5", true, false);
                    }
                    else if (modItem.miningRadius == 2)
                    {
                        modItem.miningRadius = 0;
                        CombatText.NewText(player.getRect(), new Color(133, 229, 255), "1x1", true, false);
                    }
                    rightCD = 60;
                }
                return false;
            }
           
            return base.CanUseItem(player);
        }
        public override void UpdateInventory(Player player)
        {
            rightCD--;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<DiscordantBar>(), 15);
            recipe.AddIngredient(ItemType<ChaoticFlare>(), 8);
            recipe.AddIngredient(ItemID.LaserDrill, 1);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}