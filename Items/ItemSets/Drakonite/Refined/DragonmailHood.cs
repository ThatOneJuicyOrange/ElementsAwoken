using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Head)]
    public class DragonmailHood : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.defense = 9;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonmail Hood");
            Tooltip.SetDefault("Increases maximum mana by 80\nReduces mana usage by 15%\n7% increased magic damage\n5% increased magic critical strike chance");
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 80;
            player.magicCrit += 5;
            player.magicDamage *= 1.07f;
            player.manaCost *= 0.85f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DragonmailChestpiece") && legs.type == mod.ItemType("DragonmailLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Magic projectiles inflict Dragonfire\nMagic damage increases as life lowers";
            player.GetModPlayer<MyPlayer>().dragonmailHood = true;
            if (player.statLife <= (player.statLifeMax2 * 0.75f))
            {
                player.magicDamage *= 1.02f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.5f))
            {
                player.magicDamage *= 1.08f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.25f))
            {
                player.magicDamage *= 1.12f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.05f))
            {
                player.magicDamage *= 1.20f;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
