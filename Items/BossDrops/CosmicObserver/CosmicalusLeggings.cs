using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    [AutoloadEquip(EquipType.Legs)]
    public class CosmicalusLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.defense = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmicalus Leggings");
            Tooltip.SetDefault("15% increased movement speed");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.15f;

            if (player.velocity.Y == 0f && player.velocity.X != 0)
            {
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 220, 0f, 0f, 100, default(Color), 1.4f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CosmicShard", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
