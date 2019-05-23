using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Wings)]

    public class BubblePack : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Pack");
            Tooltip.SetDefault("Allows flight and slow fall\nPropel yourself with killer bubbles");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 162;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 12f;
            acceleration *= 3f;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int projectile1 = Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileID.FlaironBubble, 20, 5f, player.whoAmI, 0f, 0f);
                    Main.projectile[projectile1].timeLeft = 40;
                }
            }
            base.WingUpdate(player, inUse);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
