using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class AcidTapEntity : ModTileEntity
    {
        public int acidAmount = 0;
        public int acidMax = 30;
        public int succTimer = 0;
        public int delay = (3600 * 10) / 30; // 10 minutes to fill
        public override void Update()
        {
            if (acidAmount < acidMax)
            {
                succTimer++;
                if (succTimer > delay)
                {
                    acidAmount++;
                    succTimer = 0;
                }
            }
        }
        public override TagCompound Save()
        {
            return new TagCompound {
                {"acidAmount", acidAmount},
                {"succTimer", succTimer},
            };
        }
        public override void Load(TagCompound tag)
        {
            acidAmount = tag.GetInt("acidAmount");
            succTimer = tag.GetInt("succTimer");
        }
        public override void NetSend(BinaryWriter writer, bool lightSend)
        {
            writer.Write(acidAmount);
            writer.Write(succTimer);
        }
        public override void NetReceive(BinaryReader reader, bool lightReceive)
        {
            acidAmount = reader.ReadInt32();
            succTimer = reader.ReadInt32();
        }
        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == TileType<AcidTap>() && tile.frameX == 0 && tile.frameY == 0;
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            //Main.NewText("i " + i + " j " + j + " t " + type + " s " + style + " d " + direction);
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 1);
                NetMessage.SendData(87, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
    }
}