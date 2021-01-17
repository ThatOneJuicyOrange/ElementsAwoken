using ElementsAwoken.Items.Placeable.Tiles;
using ElementsAwoken.NPCs.Bosses.Obsidious;
using ElementsAwoken.NPCs.Bosses.Obsidious.Beneath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles
{
    public class PinkyCaveManager : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "Terraria/Tiles_" + TileID.Dirt;
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][TileID.Dirt] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            drop = ItemID.DirtBlock;
            AddMapEntry(new Color(151, 107, 75));
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedGiantPinky) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            var dist = (int)Vector2.Distance(player.Center / 16, new Vector2(i, j));
            if (dist <= 200)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int type = NPCType<NPCs.Random.GiantPinky>();
                    if (!NPC.AnyNPCs(type) && !MyWorld.downedGiantPinky)
                    {
                        int worldX = i * 16 + 8;
                        int worldY = (j - 2) * 16;
                        int n = NPC.NewNPC(worldX, worldY, type);
                        Main.npc[n].Bottom = new Vector2(worldX, worldY);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                }
            }
        }
    }
}