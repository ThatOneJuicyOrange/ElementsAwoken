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
            Tooltip.SetDefault("Sucks life of enemies around you that have under 10000 life");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            drainLifeTimer--;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            float maxDistance = 750f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int l = 0; l < 200; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= maxDistance && nPC.life <= 10000 && nPC.life > 30 && !nPC.SpawnedFromStatue)
                    {
                        nPC.AddBuff(mod.BuffType("LifeDrain"), 5);
                        nPC.GetGlobalNPC<NPCsGLOBAL>(mod).lifeDrainAmount = 100;
                        if (drainLifeTimer <= 0)
                        {
                            float healAmount = Main.rand.Next(4, 6);
                            Projectile.NewProjectile(nPC.Center.X, nPC.Center.Y, 0f, 0f, mod.ProjectileType("HealProjVoid"), 0, 0f, Main.myPlayer, Main.myPlayer, healAmount); // ai 1 is how much it heals
                            drainLifeTimer = 17;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LifeDiamond", 1);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
