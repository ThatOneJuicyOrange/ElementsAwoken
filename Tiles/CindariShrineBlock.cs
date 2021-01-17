using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.NPCs.VolcanicPlateau;

namespace ElementsAwoken.Tiles
{
    public class CindariShrineBlock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][TileID.ObsidianBrick] = true;

            /* Main.tileSolid[Type] = true;
             Main.tileFrameImportant[Type] = true;
             Main.tileBlockLight[Type] = true;
             e
             TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
             TileObjectData.newTile.LavaDeath = false;
             TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
             TileObjectData.addTile(Type);
             */
            AddMapEntry(new Color(244, 237, 39));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                Main.PlaySound(4, i * 16, j * 16, 60, 1, 0.4f);

                Vector2 pos = new Vector2(i * 16 + 8, j * 16 + 8);
                int numDusts = 36;
                for (int p = 0; p < numDusts; p++)
                {
                    Vector2 position = Vector2.One.RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                    Vector2 velocity = position - pos;
                    Vector2 spawnPos = position + velocity;
                    Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 6, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.velocity = Vector2.Normalize(velocity) * 6f * Main.rand.NextFloat(0.8f, 1.2f);
                }
                int cindari = NPC.FindFirstNPC(NPCType<CindariShrine>());
                if (cindari >= 0) Main.npc[cindari].active = false;
            }
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
                    if (!NPC.AnyNPCs(NPCType<CindariShrine>()) && !MyWorld.spokenToCindari)
                    {
                        i *= 16;
                        j *= 16;
                        int n = NPC.NewNPC(i + 25, j + 25, NPCType<CindariShrine>());
                        Main.npc[n].Center = new Vector2(i + 8, j + 8 - 60);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                }
            }
        }
        public override bool Slope(int i, int j)
        {
            return false;
        }
    }
}