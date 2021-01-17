using System;
using System.IO;
using ElementsAwoken.NPCs.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau.Lake;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class LorekeepersTome : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.useStyle = 4;

            item.useTurn = true;
            item.consumable = false;

            item.useAnimation = 24;
            item.useTime = 24;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 6;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lorekeeper's Tome");
            Tooltip.SetDefault("Click on a plateau inhabitant to learn the story\nA gift bestowed from the fallen Keeper");
        }
        public override bool UseItem(Player player)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.Hitbox.Contains(Main.MouseWorld.ToPoint()) && nPC.GetGlobalNPC<PlateauNPCs>().tomeClickable)
                {
                    MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                    string text = nPC.GetGlobalNPC<PlateauNPCs>().tomeText;
                    if (text != "")
                    {
                        modPlayer.tomeTex = Main.npcTexture[nPC.type];
                        if (nPC.type == ModContent.NPCType<TheViper>() || nPC.type == ModContent.NPCType<TheViperHead>()) modPlayer.tomeTex = mod.GetTexture("NPCs/VolcanicPlateau/Lake/TheViperFull");
                         modPlayer.tomeTexHeight = Main.npcTexture[nPC.type].Height / Main.npcFrameCount[nPC.type];
                    }
                    else
                    {
                        modPlayer.tomeTex = null;
                        text = "The tome does not have this creature documented.";
                    }
                    modPlayer.tomeUI = true;

                    modPlayer.tomeText = text;
                    Main.npcChatText = "";
                    Main.PlaySound(10, -1, -1, 0);
                    break;
                }
            }
                return true;
        }
    }
}
