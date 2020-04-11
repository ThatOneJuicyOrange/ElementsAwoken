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
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Items.Materials;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.Accessories
{
    public class VoidDiamond : ModItem
    {
        public int drainLifeTimer = 0;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Diamond");
            Tooltip.SetDefault("Sucks the life of enemies around you that have under 10000 life");
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && (player.armor[i].type == ItemType<LifeDiamond>() || player.armor[i].type == ItemType<BloodDiamond>()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            float maxDistance = 750f;
            float targetDist = maxDistance;
            NPC drainTarget = null;
            if (player.statLife < player.statLifeMax2 && player.ownedProjectileCounts[ProjectileType<HealProjVoid>()] < 15)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    float distance = Vector2.Distance(nPC.Center, player.Center);
                    if (distance < targetDist && nPC.CanBeChasedBy(this) && nPC.life <= 10000 && !nPC.SpawnedFromStatue)
                    {
                        targetDist = distance;
                        drainTarget = nPC;
                    }
                }
                if (drainTarget != null)
                {
                    drainTarget.AddBuff(BuffType<VariableLifeRegen>(), 20);
                    drainTarget.GetGlobalNPC<NPCsGLOBAL>().lifeDrainAmount = 50;
                    drainLifeTimer--;
                    if (drainLifeTimer <= 0)
                    {
                        drainTarget.GetGlobalNPC<NPCsGLOBAL>().lifeDrainAmount = 100;
                        if (Main.myPlayer == player.whoAmI)
                        {
                            float healAmount = Main.rand.Next(4, 7);
                            Projectile.NewProjectile(drainTarget.Center.X, drainTarget.Center.Y, 0f, 0f, ProjectileType<HealProjVoid>(), 0, 0f, Main.myPlayer, Main.myPlayer, healAmount); // ai 1 is how much it heals
                            drainLifeTimer = 17;
                        }
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<LifeDiamond>(), 1);
            recipe.AddIngredient(ItemType<VoiditeBar>(), 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
