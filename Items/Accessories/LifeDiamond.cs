using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Items.Accessories
{
    public class LifeDiamond : ModItem
    {
        public int drainLifeTimer = 20;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Diamond");
            Tooltip.SetDefault("Sucks the life of enemies around you that have under 3000 life");
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType("VoidDiamond") && player.armor[i].type == mod.ItemType("BloodDiamond"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            float maxDistance = 500f;
            float targetDist = maxDistance;
            NPC drainTarget = null;
            if (player.statLife < player.statLifeMax2 && player.ownedProjectileCounts[mod.ProjectileType("HealProjLife")] < 15)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    float distance = Vector2.Distance(nPC.Center, player.Center);
                    if (distance < targetDist && nPC.CanBeChasedBy(this) && nPC.life <= 3000 && !nPC.SpawnedFromStatue)
                    {
                        targetDist = distance;
                        drainTarget = nPC;
                    }
                }
                if (drainTarget != null)
                {
                    drainLifeTimer--;
                    if (drainLifeTimer <= 0)
                    {
                        drainTarget.GetGlobalNPC<NPCsGLOBAL>().lifeDrainAmount = 50; if (Main.myPlayer == player.whoAmI)
                        {
                            float healAmount = Main.rand.Next(2, 4);
                            Projectile.NewProjectile(drainTarget.Center.X, drainTarget.Center.Y, 0f, 0f, mod.ProjectileType("HealProjLife"), 0, 0f, Main.myPlayer, Main.myPlayer, healAmount); // ai 1 is how much it heals
                            drainLifeTimer = 30;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BloodDiamond", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
