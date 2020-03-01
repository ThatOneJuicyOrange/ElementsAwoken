using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.BossSummons
{
    public class WastelandSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.maxStack = 20;

            item.rare = 2;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;

            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutated Scorpion");
            Tooltip.SetDefault("It writhes in your hand\nSummons Wasteland on use");
        }


        public override bool CanUseItem(Player player)
        {
            return 
            player.ZoneDesert && 
            !NPC.AnyNPCs(mod.NPCType("Wasteland"));
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Wasteland"));
            else NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Wasteland"), 0f, 0f, 0, 0, 0);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
