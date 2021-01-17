using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using ElementsAwoken.Tiles.VolcanicPlateau.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.VolcanicPlateau.ObjectSpawners
{
    public class SpiderCubeSpawner : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Tiles/VolcanicPlateau/SulfuricBricks";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);

            drop = ModContent.ItemType<SulfuricBricksItem>();
            AddMapEntry(new Color(46, 51, 25));
            soundType = 21;
            soundStyle = 6;

            minPick = 50;
        }
        public override bool CanExplode(int i, int j)
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
                    int worldX = i * 16;
                    int worldY = j * 16;
                    if (!NPC.AnyNPCs(NPCType<NPCs.Liftable.EriusCube>()) && !MyWorld.downedErius)
                    {
                        int n = NPC.NewNPC(worldX + 448, worldY - 608, NPCType<NPCs.Liftable.EriusCube>());
                        Main.npc[n].Bottom = new Vector2(worldX + 8, worldY);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                }
            }
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                int npc = NPC.FindFirstNPC(NPCType<SpiderDoor>());
                if (npc >= 0) Main.npc[npc].active = false;
            }
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!MyWorld.downedErius) return false;
            return base.CanKillTile(i, j, ref blockDamaged);
        }
    }
}