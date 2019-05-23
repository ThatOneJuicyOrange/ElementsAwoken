using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ElementsAwoken.Structures;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Testing
{
    public class ExpertsCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = false;
            item.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Expert's Crystal");
            Tooltip.SetDefault("2000 iq only");
        }
        public override bool UseItem(Player player)
        {
            if (Main.expertMode)
            {
                Main.expertMode = false;
                Main.NewText("Expert mode disabled", Color.White.R, Color.White.G, Color.White.B);
            }
            else
            {
                Main.expertMode = true;
                Main.NewText("Expert mode enabled", Color.White.R, Color.White.G, Color.White.B);
            }
            return true;
        }
    }
}
