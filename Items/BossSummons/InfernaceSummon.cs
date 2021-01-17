using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ElementsAwoken.NPCs.Bosses.Infernace;

namespace ElementsAwoken.Items.BossSummons
{
    public class InfernaceSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 3;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charred Ring");
            Tooltip.SetDefault("It radiates a burning heat\nSummons Infernace on use");
        }


        public override bool CanUseItem(Player player)
        {
            return 
            player.ZoneUnderworldHeight && 
            !NPC.AnyNPCs(mod.NPCType("Infernace"));
        }
        public override bool UseItem(Player player)
        {
             Main.NewText("You arrive, at last.", Color.Orange.R, Color.Orange.G, Color.Orange.B);
             /*if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Infernace"));
             else NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Infernace"), 0f, 0f, 0, 0, 0); */

            int npcIndex = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 300, ModContent.NPCType<Infernace>(), 0, 0f, 0f, 0f, 0f, 255);
            Main.npc[npcIndex].ai[1] = -300;
            Main.npc[npcIndex].ai[3] = -1;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex, 0f, 0f, 0f, 0, 0, 0);

            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.Obsidian, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
