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

namespace ElementsAwoken.Tiles
{
    public class PowerCellEntity : ModTileEntity
    {
        public int energy = 0;
        public int maxEnergy = 300;
        public override void Update()
        {
            Player player = Main.LocalPlayer;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            Vector2 tileCenter = new Vector2(Position.X * 16, Position.Y * 16);
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }

            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 2, 2);
            Rectangle tileRect = new Rectangle(Position.X * 16, Position.Y * 16, 5 * 16, 5 * 16);
            /*for (int d = 0; d < 3; d++)
            {
                int dust = Dust.NewDust(mouse.TopLeft(), mouse.Width, mouse.Height, 57, 0f, 0f, 100);
                Main.dust[dust].velocity *= 0.01f;
                int dust2 = Dust.NewDust(tileRect.TopLeft(), tileRect.Width, tileRect.Height, 57, 0f, 0f, 100);
                Main.dust[dust2].velocity *= 0.01f;
            }*/
            if (Main.mouseRight && mouse.Intersects(tileRect))
            {
                if (modPlayer.energy > 0 && energy < maxEnergy)
                {
                    energy++;
                    modPlayer.energy--;
                    Main.PlaySound(SoundID.MenuTick);
                }
            }
        }
        public override TagCompound Save()
        {
            return new TagCompound {
                {"energy", energy},
            };
        }
        public override void Load(TagCompound tag)
        {
            energy = tag.GetInt("energy");
        }
        public override void NetSend(BinaryWriter writer, bool lightSend)
        {
            writer.Write(energy);
        }
        public override void NetReceive(BinaryReader reader, bool lightReceive)
        {
            energy = reader.ReadInt32();
        }
        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == TileType<PowerCell>() && tile.frameX == 0 && tile.frameY == 0;
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            //Main.NewText("i " + i + " j " + j + " t " + type + " s " + style + " d " + direction);
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j + 1, 2);
                NetMessage.SendData(87, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
    }
}