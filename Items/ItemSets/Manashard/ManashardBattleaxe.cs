using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Manashard
{
    public class ManashardBattleaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 60;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 28;
            item.useTurn = true;
            item.useAnimation = 28;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manashard Battleaxe");
            Tooltip.SetDefault("");
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 27);
                int numberProjectiles = 4;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-6, 6));
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, value15.X, value15.Y, mod.ProjectileType("Manashatter"), item.damage / 2, 2f, player.whoAmI, 0f, 0f);
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Manashard", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
