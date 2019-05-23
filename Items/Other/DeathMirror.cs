using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class DeathMirror : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.UseSound = SoundID.Item6;
            item.useStyle = 4;

            item.useTurn = true;
            item.consumable = false;

            item.useAnimation = 17;
            item.useTime = 17;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 6;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell's Reflection");
            Tooltip.SetDefault("Teleports the player to the last death postition\nMust teleport back within 30 seconds");
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);

            return player.lastDeathTime > DateTime.MinValue && modPlayer.hellsReflectionTimer > 0;
        }
        public override bool UseItem(Player player)
        {
            player.Teleport(new Vector2(player.lastDeathPostion.X, player.lastDeathPostion.Y - player.height));
            return true;
        }
        public override void HoldItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            if (modPlayer.hellsReflectionTimer > 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int num5 = Dust.NewDust(player.position, player.width, player.height, 60, 0f, 0f, 200, default(Color), 0.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.75f;
                    Main.dust[num5].fadeIn = 1.3f;
                    Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector.Normalize();
                    vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[num5].velocity = vector;
                    vector.Normalize();
                    vector *= 34f;
                    Main.dust[num5].position = player.Center - vector;
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MagicMirror, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
