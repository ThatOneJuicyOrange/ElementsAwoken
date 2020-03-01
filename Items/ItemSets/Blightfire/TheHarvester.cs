using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Blightfire
{
    public class TheHarvester : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 220;
            item.knockBack = 1;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.UseSound = SoundID.Item1;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 11;

            item.shoot = mod.ProjectileType("HarvesterScythe");
            item.shootSpeed = 14f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Harvester");
            Tooltip.SetDefault("Throws a life-stealing scythe");
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74)];
            dust.velocity *= 0.2f;
            dust.noGravity = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 180);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blightfire", 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
