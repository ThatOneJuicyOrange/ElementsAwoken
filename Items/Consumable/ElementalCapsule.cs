using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ElementsAwoken.Items.Testing;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.NPCs;
using ElementsAwoken.Items.Materials;

namespace ElementsAwoken.Items.Consumable
{
    public class ElementalCapsule : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.expert = true;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item119;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Capsule");
            Tooltip.SetDefault($"Forces bend around this capsule...\nCan only be used in Expert Mode\nOnly use if you are up for a challenge\nActivates [c/f442aa:Awakened Mode] and sanity [i:{ItemType<SanityChanger>()}]\n  -75% more enemy life, defence and damage\n  -New harder boss AI's and stats");
        }

        public override bool CanUseItem(Player player)
        {
            if (NPCsGLOBAL.AnyBoss())
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " couldn't withstand the power."), 3000, 1);
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode)
                {
                    Main.NewText("The forces of the world settle.", Color.DeepPink);
                    MyWorld.awakenedMode = false;
                }
                else
                {
                    Main.NewText("The forces of the world get twisted beyond imagination...", Color.DeepPink);
                    MyWorld.awakenedMode = true;
                }
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                return true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemType<Stardust>(), 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
