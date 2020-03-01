using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Events;

namespace ElementsAwoken.Events.VoidEvent
{
    public class VoidEvent
    {
        public static Mod mod = ModLoader.GetMod("ElementsAwoken");
        public static List<int> phase1NPCs = new List<int>() {
            mod.NPCType("Immolator"),
            mod.NPCType("ReaverSlime"),
            mod.NPCType("VoidKnight"),
            mod.NPCType("VoidElemental"),
            mod.NPCType("AbyssSkull"),
            mod.NPCType("AbyssSkullette"),
            mod.NPCType("VoidFly"),
            mod.NPCType("AccursedFlier"),
            mod.NPCType("DimensionalHive"),
            mod.NPCType("ZergCaster")
        };

        public static List<int> phase2NPCs = new List<int>() {
            mod.NPCType("ShadeWyrmHead"),
            mod.NPCType("ShadeWyrmBody"),
            mod.NPCType("ShadeWyrmTail"),
            mod.NPCType("EtherealHunter"),
            mod.NPCType("VoidCrawler"),
            mod.NPCType("VoidGolem")
        };
        public static void StartInvasion()
        {
            //Set to no invasion
            if (Main.invasionType != 0 && Main.invasionSize == 0)
            {
                Main.invasionType = 0;
            }
            if (Main.invasionType == 0)
            {
                //Checks amount of players
                int num = 0;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active)
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    //Invasion setup
                    Main.invasionType = -1;
                    MyWorld.voidInvasionUp = true;
                    Main.invasionX = Main.maxTilesX;
                }
            }
        }

        public static void InvasionWarning()
        {
            string text = "";
            if (Main.invasionX == Main.spawnTileX)
            {
                text = "The dawn of the void has started!";
            }
            if (Main.dayTime)
            {
                text = "The void retreats back into the shadows...";
            }
            if (Main.netMode == 0)
            {
                Main.NewText(text, 182, 15, 15, false);
                return;
            }
            if (Main.netMode == 2)
            {
                //Sync with net
                //NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f, 0, 0, 0);
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(182, 15, 15));
            }
        }

        public static void UpdateInvasion()
        {
            if (MyWorld.voidInvasionUp)
            {
                if (Main.dayTime)
                {
                    InvasionWarning();
                    Main.invasionType = 0;
                    Main.invasionDelay = 0;
                    MyWorld.downedVoidEvent = true;
                    MyWorld.voidInvasionUp = false;
                    MyWorld.voidInvasionFinished = 200;
                }
                //Do not do the rest if invasion already at spawn
                if (Main.invasionX == Main.spawnTileX)
                {
                    return;
                }
                //Update when the invasion gets to Spawn
                float speed = 10;
                if (Main.invasionX > Main.spawnTileX)
                {
                    Main.invasionX -= speed;
                    if (Main.invasionX <= Main.spawnTileX)
                    {
                        Main.invasionX = Main.spawnTileX;
                        InvasionWarning();
                    }
                }
                else if (Main.invasionX < Main.spawnTileX)
                {
                    Main.invasionX += speed;
                    if (Main.invasionX >= Main.spawnTileX)
                    {
                        Main.invasionX = Main.spawnTileX;
                        InvasionWarning();
                    }
                }
            }
        }
    }
}