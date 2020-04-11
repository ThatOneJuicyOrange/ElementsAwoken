using ElementsAwoken.Items.BossDrops.Volcanox;
using ElementsAwoken.Items.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    [AutoloadEquip(EquipType.Legs)]
    public class PutridLeggings : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.defense = 13;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rotted Reaper Leggings");
            Tooltip.SetDefault("10% increased minion damage\nIncreases your max number of minions\nYou leave a toxic trail");
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.1f;
            player.maxMinions++;
            if (player.velocity.Y == 0 && (player.velocity.X < -1 || player.velocity.X > 1) && player.GetModPlayer<MyPlayer>().generalTimer % 3 == 0)
            {
                Projectile.NewProjectile(player.Bottom.X, player.Bottom.Y - 6, 0, -0.9f, ProjectileType<PutridTrail>(), 60, 0, player.whoAmI, 0.0f, 0.0f);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
