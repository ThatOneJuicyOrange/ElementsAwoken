using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Donator.Aegida
{
    [AutoloadEquip(EquipType.Head)]
    public class MechMask : ModItem
    {
        private float pulsate = 0;
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 11;

            item.defense = 18;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Mask");
            Tooltip.SetDefault("19% increased ranged damage\n9% increased ranged critical strike chance\nAn experimental helmet made by the scientists for its elite rangers\nAegida's donator item");
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.19f;
            player.rangedCrit += 9;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MechBreastplate") && legs.type == mod.ItemType("MechLeggings");
        }
        public override void UpdateArmorSet(Player player)
        {
            pulsate++;
            float pulsateValue = 0.6f + (float)Math.Sin(pulsate / 30f) / 2;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.2f * pulsateValue, 0.2f * pulsateValue, 0.8f * pulsateValue);
            player.setBonus = "10% increased damage\n35% increased movement speed\n17% increased chance not to consume ammo\nWhen you get hit, you release electrical arcs";
            player.allDamage *= 1.1f;
            player.moveSpeed *= 1.35f;
            player.GetModPlayer<MyPlayer>().saveAmmo += 17;
            player.GetModPlayer<MyPlayer>().mechArmor = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexHelmet);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 3);
            recipe.AddIngredient(ItemType<VolcanicStone>(), 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
