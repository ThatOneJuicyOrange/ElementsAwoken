using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Tiles.Objects
{
    public class GoldenDucky : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileTable[Type] = true;
            Main.tileLighted[Type] = false;

            disableSmartCursor = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            AddMapEntry(new Color(244, 237, 39));

            drop = mod.ItemType("GoldenDucky");

            TileObjectData.addTile(Type);
        }
    }
}