using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Map;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using ElementsAwoken.Tiles.Objects;
using System.Collections.Generic;

namespace ElementsAwoken.Tiles
{
    public class GlobalTiles : GlobalTile
    {
        public static List<int> quicksands = new List<int>();
        public static List<int> liquidBlockers = new List<int>();
        public override void SetDefaults()
        {
            Main.tileSpelunker[TileID.LunarOre] = true;
            TileID.Sets.Ore[TileID.LunarOre] = true;
            Main.tileValue[TileID.LunarOre] = 1000;
            Main.tileMerge[TileID.BoneBlock][TileType<VolcanicPlateau.ActiveIgneousRock>()] = true;
            Main.tileMerge[TileID.BoneBlock][TileType<VolcanicPlateau.IgneousRock>()] = true;
            Main.tileMerge[TileID.BoneBlock][TileType<VolcanicPlateau.MalignantFlesh>()] = true;
            Main.tileMerge[TileID.BoneBlock][TileType<VolcanicPlateau.ObjectSpawners.TheKeeperSpawner>()] = true;
            Main.tileMerge[TileID.Ash][TileType<VolcanicPlateau.ActiveIgneousRock>()] = true;
            Main.tileMerge[TileID.Ash][TileType<VolcanicPlateau.IgneousRock>()] = true;
            Main.tileMerge[TileID.Ash][TileType<VolcanicPlateau.MalignantFlesh>()] = true;
            Main.tileMerge[TileID.Ash][TileType<ObsidiousBrick>()] = true;
            Main.tileMerge[TileID.Cloud][TileType<Stellorite>()] = true;
            Main.tileMerge[TileID.Dirt][TileType<PinkyCaveManager>()] = true;
            if (!Main.dedServ)
            {
                Lang._mapLegendCache[MapHelper.TileToLookup(TileID.LunarOre, 0)] = Lang.GetItemName(ItemID.LunarOre);
            }
        }
        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            if (MyWorld.fastRandUpdate > 0)
            {
                ModTile t = TileLoader.GetTile(Main.tile[i,j].type);
                if (t != null)t.RandomUpdate(i, j);
            }
        }
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            Tile t = Framing.GetTileSafely(i, j);
            Tile tAbove = Framing.GetTileSafely(i, j - 1);
            if ((tAbove.type == TileType<OrderStatueTile>() ||
                tAbove.type == TileType<VolcanicPlateau.Objects.AcidGeyser>() ||
                tAbove.type == TileType<VolcanicPlateau.Objects.SulfurVent>() ||
                tAbove.type == TileType<VolcanoxShrine>()) && Main.tileSolid[type]) return false;
            Player player = Main.LocalPlayer;
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (player.position.Y > Main.maxTilesY * .25f * 16)
            {
                modPlayer.mineTileCooldown = modPlayer.mineTileCooldownMax; // 3 minutes of not mining resets the mining countdown
                //ElementsAwoken.DebugModeText("digging tile below surface");
            }   
            if (!MyWorld.downedMineBoss)
            {
                if (i >= EAWorldGen.mineBossArenaLoc.X && i <= EAWorldGen.mineBossArenaLoc.X + 100 && j >= EAWorldGen.mineBossArenaLoc.Y && j <= EAWorldGen.mineBossArenaLoc.Y + 18) return false;
            }
            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
        public override void RandomUpdate(int i, int j, int type)
        {
            if (EAWorldGen.generatedPlateaus && type == TileID.Obsidian)
            {
                if (i> EAWorldGen.plateauLoc.X + 510 && i < EAWorldGen.plateauLoc.X + 839 && j > Main.maxTilesY - 200)
                {
                    Main.tile[i, j].active(false);
                    WorldGen.SquareTileFrame(i,j, true);
                }
            }
        }
        // theres no way to find the min pick so shove it in manually
        public static int GetTileMinPick(int type)
        {
            if (type == 37)
            {
                return 50;
            }
            else if ((type == 22 || type == 204))
            {
                return 55;
            }
            else if (type == 25 || type == 203 || type == 117 || type == 404 || type == 56 || type == 58 || Main.tileDungeon[type])
            {
                return 65;
            }
            else if (type == 107 || type == 221)
            {
                return 100;
            }
            else if (type == 108 || type == 222)
            {
                return 110;
            }
            else if (type == 111 || type == 223)
            {
                return 150;
            }
            else if (type == 211)
            {
                return 200;
            }
            else if (type == 226 || type == 237 || type == 408)
            {
                return 210;
            }
            else
            {
                ModTile modTile = TileLoader.GetTile(type);
                if (modTile != null) return modTile.minPick;
            }
            return 0;
        }
    }
}