using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireNeck : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of Inferno");
            Tooltip.SetDefault("Unleash the power of flames upon your enemies\nDouble tap down to create a small explosion that launches the player into the air\nMelee attacks have flames\n2% increased damage");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.fireAccCD--;
            modPlayer.fireAcc = true;
            player.magmaStone = true;
            player.allDamage *= 1.02f;
            if (player.velocity.Y != 0)
            {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6)];
                    dust.noGravity = true;
                    dust.scale = 2f;
                    dust.velocity *= 0.2f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool DrawBody()
        {
            return false;
        }
    }
}
