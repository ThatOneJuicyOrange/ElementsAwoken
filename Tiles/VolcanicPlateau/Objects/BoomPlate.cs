/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using ElementsAwoken.Items.Placeable;
using Terraria.Enums;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class BoomPlate : ModTile
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
            for (int p = 0; p < Main.maxProjectiles; p++)
            {
                Projectile proj = Main.projectile[p];
                int maxDist = 30;
                if (proj.type == ProjectileID.Grenade || proj.type == ProjectileID.BouncyGrenade || proj.type == ProjectileID.StickyGrenade) maxDist = 30;
                else if (proj.type == ProjectileID.Bomb || proj.type == ProjectileID.BombFish || proj.type == ProjectileID.StickyBomb || proj.type == ProjectileID.BouncyBomb) maxDist = 64;
                else if (proj.type == ProjectileID.Dynamite || proj.type == ProjectileID.BouncyDynamite || proj.type == ProjectileID.StickyDynamite) maxDist = 96;
                else break;
                Vector2 center = new Vector2(i * 16 + 8, j * 16 + 8);
                if (proj.timeLeft <= 5 && proj.active && Vector2.Distance(center,proj.Center) < maxDist)
                {
                    Main.tile[i, j].frameY = 18;
                    int npc = NPC.FindFirstNPC(ModContent.NPCType<SpiderDoor>());
                    if (npc >= 0) Main.npc[npc].ai[0] = 1;
                }
            }
            if (Main.tile[i, j].frameY == 18)
            {
                int npc = NPC.FindFirstNPC(ModContent.NPCType<SpiderDoor>());
                if (npc >= 0)
                {
                    if (Main.npc[npc].ai[0] == 0) Main.tile[i, j].frameY = 0;
                }
            }
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedErius && !ModContent.GetInstance<Config>().debugMode) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
    }
}*/