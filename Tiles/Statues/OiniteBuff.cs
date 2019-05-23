using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace ElementsAwoken.Tiles.Statues
{
    public class OiniteBuff : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(144, 148, 144));
            dustType = 11;
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("OiniteBuffStatue"));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.player[Main.myPlayer]; 
            if (closer && player.FindBuffIndex(mod.BuffType("StatueBuffBurst")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffGenihWat")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffRanipla")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffOrange")) == -1 && player.FindBuffIndex(mod.BuffType("StatueBuffAmadis")) == -1)
            {
                player.AddBuff(mod.BuffType("StatueBuffOinite"), 60, true);
            }
        }
    }
}