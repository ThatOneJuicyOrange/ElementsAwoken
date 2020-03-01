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
    public class AncientsSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
            item.maxStack = 20;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resurrection Sigil");
            Tooltip.SetDefault("Summons The Ancients on use");
        }

        public override bool CanUseItem(Player player)
        {
            return
            !NPC.AnyNPCs(mod.NPCType("Izaris")) &&
            !NPC.AnyNPCs(mod.NPCType("Kirvein")) &&
            !NPC.AnyNPCs(mod.NPCType("Krecheus")) &&
            !NPC.AnyNPCs(mod.NPCType("Xernon"))&& 
            !NPC.AnyNPCs(mod.NPCType("AncientAmalgam"));
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            { 
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Izaris"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Kirvein"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Krecheus"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Xernon"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("ShardBase"));
            }
            else 
            {
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Izaris"), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Kirvein"), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Krecheus"), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Xernon"), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("ShardBase"), 0f, 0f, 0, 0, 0);
            }
            if (!MyWorld.downedAncients)
            {
                if (MyWorld.ancientSummons == 0)
                {
                    Main.NewText("I've waited centuries for this!", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 1)
                {
                    Main.NewText("Back for more?", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 2)
                {
                    Main.NewText("I could do this all day!", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 3)
                {
                    Main.NewText("You should have gone for the head...", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 4)
                {
                    Main.NewText("Really? Still trying?", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 5)
                {
                    Main.NewText("Give up already.", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons > 5 && MyWorld.ancientSummons < 10)
                {
                    Main.NewText("Are you seriously gonna keep dying?", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 10)
                {
                    Main.NewText("As much fun slaying you is, there are better things to do with both our lives.", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 20)
                {
                    Main.NewText("Lunatic.", new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 100)
                {
                    Main.NewText("I just don't think you are cut out to do this... Go find some other hobby.", new Color(3, 188, 127));
                }
            }
            else
            {
                //if (MyWorld.ancientKills > 0)
                {
                    Main.NewText("You bring me back to this awful land... Why?", new Color(3, 188, 127));
                }
            }
           // Projectile.NewProjectile(player.Center.X, player.Center.Y - 300, 0f, 0f, mod.ProjectileType("ShardBase"), 0, 0f, player.whoAmI);

            MyWorld.ancientSummons++;
            
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaoticFlare", 2);
            recipe.AddIngredient(null, "CrystalAmalgamate", 1);
            recipe.AddIngredient(null, "DiscordantBar", 4);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
