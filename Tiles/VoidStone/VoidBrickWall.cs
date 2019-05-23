using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VoidStone
{
    public class VoidBrickWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("VoidBrickWall");
            AddMapEntry(new Color(51, 51, 51));
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(300) == 0)
            {
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 54, 0f, 0f, 0, default(Color), 1.0f);
            }
        }
    }
}