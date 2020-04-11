using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Head)]
    public class AwokenWoodHelmet : ModItem
    {
        private int shootTimer = 0;
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;

            item.defense = 14;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awoken Wooden Helmet");
            Tooltip.SetDefault("10% increased damage\nIncreases maximum mana by 50\nSomething so simple has become so powerful... As have you\nLantard's donator item");
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage *= 1.1f;
            player.statManaMax2 += 50;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<AwokenWoodBreastplate>() && legs.type == ItemType<AwokenWoodGreaves>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.05f , 0.5f, 0.2f);
            player.setBonus = "Greatly increases life regeneration after being hit\nShoots leaves at nearby enemies\nEnemies are less likely to target you\nDouble tap down to activate 'Life's Aura' which heals your team mates and friendly NPCs when inside";
            player.aggro -= 400;
            player.GetModPlayer<MyPlayer>().awokenWood = true;

            shootTimer--;
            float maxDistance = 500f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int l = 0; l < Main.maxNPCs; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.CanBeChasedBy(this) && Vector2.Distance(player.Center, nPC.Center) <= maxDistance && Collision.CanHit(player.Center, player.width, player.height, nPC.Center, nPC.width, nPC.height))
                    {
                        if (shootTimer <= 0)
                        {
                            float Speed = 12f;
                            float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            Projectile.NewProjectile(player.Center, speed, ProjectileID.Leaf, 200, 0f, Main.myPlayer, 0f, 0f);
                            shootTimer = 30;
                            break;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodHelmet);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
