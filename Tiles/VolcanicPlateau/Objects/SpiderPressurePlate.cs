using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Terraria.Enums;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class SpiderPressurePlate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Origin = new Point16(1, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(46, 51, 25));
            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Rectangle pressure = new Rectangle(i * 16, j * 16 + 8, 48, 16);
            NPC cube = null;
            if (Main.tile[i, j].frameX == 0)
            {
                for (int p = 0; p < Main.maxNPCs; p++)
                {
                    if (Main.npc[p].type == ModContent.NPCType<NPCs.Liftable.EriusCube>()) cube = Main.npc[p];
                    if (cube != null && Main.tile[i, j].frameY == 0)
                    {
                        if (pressure.Contains((int)cube.Bottom.X, (int)cube.Bottom.Y))
                        {
                            Main.PlaySound(SoundID.MenuTick, new Vector2(i, j) * 16);
                            Main.tile[i, j].frameY = 18;
                            int npc = NPC.FindFirstNPC(ModContent.NPCType<SpiderDoor>());
                            if (npc >= 0) Main.npc[npc].ai[0] = 1;
                        }
                    }
                }
            }
            if (Main.tile[i, j].frameY == 18)
            {
                int npc = NPC.FindFirstNPC(ModContent.NPCType<SpiderDoor>());
                int erius = NPC.FindFirstNPC(ModContent.NPCType<NPCs.VolcanicPlateau.Sulfur.Erius>());
                if (npc >= 0)
                {
                    if (cube != null)
                    {
                        if (!pressure.Contains((int)cube.Bottom.X, (int)cube.Bottom.Y) && Main.npc[npc].ai[0] == 0) Main.tile[i, j].frameY = 0;
                    }
                }
                if (erius >= 0)
                {
                    if (Main.npc[erius].ai[1] != 0 && Main.tile[i, j].frameX == 0)
                    {
                        if (cube != null)
                        {
                            if (pressure.Contains((int)cube.Bottom.X, (int)cube.Bottom.Y))
                            {
                                cube.velocity.Y = -3f;
                                cube.velocity.X = -4f;
                            }
                        }
                    }
                }
            }
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedErius && !ModContent.GetInstance<Config>().debugMode) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
    }
}