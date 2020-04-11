using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Puff
{
    [AutoloadEquip(EquipType.Head)]
    public class ComfyHood : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 0, 2, 0);
            item.rare = 1;
            item.defense = 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comfy Hood");
            Tooltip.SetDefault("Increases maximum minions by 1");
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ComfyShirt") && legs.type == mod.ItemType("ComfyPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Puffs no longer deal damage to you";
            player.npcTypeNoAggro[mod.NPCType("Puff")] = true;
            player.npcTypeNoAggro[mod.NPCType("SpikedPuff")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Puffball", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
