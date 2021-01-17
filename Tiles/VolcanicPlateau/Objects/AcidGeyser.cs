using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using ElementsAwoken.Projectiles.Environmental;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class AcidGeyser : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.LavaDeath = false;

            AddMapEntry(new Color(149, 173, 87));
            disableSmartCursor = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.addTile(Type);
			animationFrameHeight = 36;
            minPick = 150;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (ModContent.GetInstance<Config>().debugMode) Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<AcidGeyserItem>());
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile t = Framing.GetTileSafely(i, j);
            if (t.frameX == 0 && t.frameY == 0)
            {
                int num = Main.hardMode ? 300 : 1000;
                if (Main.rand.NextBool(num))
                {
                    bool existing = false;
                    for (int k = 0; k < Main.maxProjectiles; k++)
                    {
                        if (Main.projectile[k].type == ModContent.ProjectileType<AcidGeyserSpawn>() && Main.projectile[k].localAI[0] ==  i&& Main.projectile[k].localAI[1] == j)
                        {
                            existing = true;
                        }
                    }
                    if (!existing)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(new Vector2(i + 1, j) * 16, Vector2.Zero, ModContent.ProjectileType<AcidGeyserSpawn>(), Main.hardMode ? 34 : 10, 0, Main.myPlayer)];
                        proj.localAI[0] = i;
                        proj.localAI[1] = j;
                    }
                }
            }
        }
    }
}