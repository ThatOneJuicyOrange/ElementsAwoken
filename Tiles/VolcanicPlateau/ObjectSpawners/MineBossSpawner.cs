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
    public class MineBossSpawner : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ElementsAwoken/Tiles/VolcanicPlateau/IgneousRock";
            return true;
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);

            drop = ItemType<IgneousRockItem>();
            AddMapEntry(new Color(33, 41, 55));
            soundType = 21;
            soundStyle = 6;

            minPick = 200;
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
                    if (!NPC.AnyNPCs(NPCType<NPCs.VolcanicPlateau.Bosses.MineBoss>()) && !MyWorld.downedMineBoss)
                    {
                        int n = NPC.NewNPC(worldX, worldY, NPCType<NPCs.VolcanicPlateau.Bosses.MineBoss>());
                        Main.npc[n].Bottom = new Vector2(worldX + 68, worldY - 6);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                    if (!NPC.AnyNPCs(NPCType<MineBossDoor>()) && !MyWorld.downedMineBoss)
                    {
                        int n = NPC.NewNPC(worldX + 49 * 16, worldY - 10 * 16, NPCType<MineBossDoor>());
                        Main.npc[n].Left = new Vector2(worldX + 49 * 16, worldY - 10 * 16);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                }
            }
        }
    }
}