using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    [AutoloadEquip(EquipType.Head)]
    public class CosmicalusVisor : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.defense = 11;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmicalus Visor");
            Tooltip.SetDefault("5% increased crit chance\nAdds an extra minion slot");
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;

            player.maxMinions += 1;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CosmicalusBreastplate") && legs.type == mod.ItemType("CosmicalusLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Summons a cosmic ring to shoot at nearby enemies";
            player.GetModPlayer<MyPlayer>(mod).cosmicalusArmor = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("CosmicalusRing")] <= 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("CosmicalusRing"), 0, 0f, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CosmicShard", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
