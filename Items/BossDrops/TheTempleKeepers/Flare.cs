using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class Flare : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.accessory = true;
            item.expert = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare");
            Tooltip.SetDefault("Pressing the ability key will create a shield around the player that pushes enemies and projectiles away");
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (modPlayer.flareShieldCD > 0)
            {
                string text = "" + modPlayer.flareShieldCD / 60;
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Red, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.flare = true;
        }
    }
}
