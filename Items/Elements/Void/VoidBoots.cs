using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Wings)]
    public class VoidBoots : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of the Void");
            Tooltip.SetDefault("Insane speed!\nGreater mobility on ice\nWater and lava walking\nInfinite immunity to lava\nAllows flight and slow fall\nAllows the ability to climb walls and dash\nGives a chance to dodge attacks");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //player.dash = 1;
            modPlayer.eaDash = 1;
            player.accRunSpeed = 21f;
            player.rocketBoots = 3;
            player.moveSpeed += 17f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;
            player.spikedBoots = 1;
            player.spikedBoots = 2;
            player.wingTimeMax = 220;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.65f;
            ascentWhenRising = 0.10f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 16f;
            acceleration *= 3f;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(player.position, player.width, player.height, 127, 0, 0, 0, default(Color)); // 55, 60, 127
                    Main.dust[dust].scale *= 2f;
                }
            }
            base.WingUpdate(player, inUse);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "AqueousWaders");
            recipe.AddRecipeGroup("ElementsAwoken:LunarWings");
            recipe.AddIngredient(ItemID.MasterNinjaGear, 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
