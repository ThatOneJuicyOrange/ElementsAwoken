using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class Stellorite : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][TileID.Cloud] = true;

            Main.tileSpelunker[Type] = true;
            TileID.Sets.Ore[Type] = true;
            Main.tileValue[Type] = 1500;

            drop = ModContent.ItemType<Items.ItemSets.Stellarium.Stellorite>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Stellorite");
            AddMapEntry(new Color(173, 237, 226), name);
            mineResist = 2f;
            //dustType = 6;
            minPick = 200;
            soundType = 21;
            soundStyle = 6;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0, 0, ModContent.ProjectileType<Projectiles.StelloriteCloudPlacer>(), 0, 0, Main.myPlayer, 0f, 0f);
        }
        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.NextBool(30))
            {
                if (EAUtils.FindNumTilesNearby(Type, i, j, 25) < 30) 
                {
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if ((x != i || j != y) && Main.tile[x, y].active() && Main.tile[x, y].type == TileID.Cloud)
                            {
                                if (true)
                                {
                                    Main.tile[x, y].type = Type;
                                    WorldGen.SquareTileFrame(x, y, true);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}