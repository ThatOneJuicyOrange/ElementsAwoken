using ElementsAwoken.Buffs.Cooldowns;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{
    public class ToyBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;
            
            item.damage = 27;
            item.knockBack = 6;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 16;   
            item.useAnimation = 16;     
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 0, 75, 0);
            item.rare = 3;   
            
            item.UseSound = SoundID.Item1;  
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Blade");
            Tooltip.SetDefault("The blade has a chance to break into pieces upon hitting an enemy\nThese pieces stay on the ground dealing damage\nDon't worry, your blade magically restores itself");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(BuffType<BrokenToyBlade>()) != -1) return false;
            return base.CanUseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.NextBool(12))
            {
                Main.PlaySound(SoundID.Item37, player.Center);
                for (int i = 0; i < 2; i++)
                {
                    Projectile brick = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-2, 2), ProjectileType<LegoBrickFriendly>(), item.damage, 0, player.whoAmI)];
                    brick.penetrate = -1;
                    brick.timeLeft = 450;
                }
                player.itemAnimation = 0;
                player.AddBuff(BuffType<BrokenToyBlade>(), 60);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
