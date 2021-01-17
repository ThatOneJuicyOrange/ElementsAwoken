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
    public class ObsidiousArenaManager : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Tiles/ObsidiousBrick";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][TileType<ObsidiousBrick>()] = true;

            //drop = ModContent.ItemType<SulfuricBricksItem>();
            AddMapEntry(new Color(74, 59, 97));
            soundType = 21;
            soundStyle = 6;

            minPick = 50;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
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
                    int type = NPCType<Obsidious>();
                    if (!NPC.AnyNPCs(type) /*&& !MyWorld.downedObsidious*/)
                    {
                        int worldX = i * 16 + 8;
                        int worldY = (j - 33) * 16 + 8;
                        int n = NPC.NewNPC(worldX, worldY, type);
                        Main.npc[n].Center = new Vector2(worldX, worldY);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                    if (!NPC.AnyNPCs(NPCType<SliderSpawner>()))
                    {
                        int worldX = i * 16;
                        int worldY = j * 16;
                        int n = NPC.NewNPC(worldX, worldY, NPCType<SliderSpawner>());
                        Main.npc[n].position = new Vector2(worldX, worldY);
                        Main.npc[n].ai[0] = Framing.GetTileSafely(i + 54, j - 2).type == TileType<Objects.ObsidiousDoor>() ? 0 : 1;
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                }
            }
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            /*Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            Vector2 pos = new Vector2((i + 6) * 16 + 8 - (int)Main.screenPosition.X, (j + 12) * 16 - (int)Main.screenPosition.Y) + zero;
            spriteBatch.Draw(mod.GetTexture("Tiles/ObsidiousWindowArch"), pos, null, Lighting.GetColor(i + 17,j + 13), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);*/
        }
    }
}