using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.ObjectSpawners
{
    public class TheKeeperSpawner : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "Terraria/Tiles_" + TileID.BoneBlock;
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);
            Main.tileMerge[Type][TileID.BoneBlock] = true;

            drop = ItemID.BoneBlock;
            AddMapEntry(new Color(157, 157, 107));
            soundType = 21;
            soundStyle = 6;

            minPick = 0;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedKeeper) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!MyWorld.downedKeeper)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                var dist = (int)Vector2.Distance(player.Center / 16, new Vector2(i, j));

                if (dist <= 200)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int worldX = i * 16;
                        int worldY = (j - 8) * 16;
                        if (!NPC.AnyNPCs(NPCType<NPCs.VolcanicPlateau.Bosses.TheKeeper>()))
                        {
                            int n = NPC.NewNPC(worldX, worldY, NPCType<NPCs.VolcanicPlateau.Bosses.TheKeeper>());
                            Main.npc[n].Bottom = new Vector2(worldX, worldY);
                            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                        }
                    }
                }

                if (Main.rand.NextBool(60))
                {
                    for (int x = -24; x < 26; x++) // 24 to the right 26 to the left 1 in the middle
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            Tile t = Framing.GetTileSafely(i + x, j - y);
                            t.liquid = 0;
                            WorldGen.SquareTileFrame(i + x, j - y, false);
                        }
                    }
                }
            }
        }
    }
}