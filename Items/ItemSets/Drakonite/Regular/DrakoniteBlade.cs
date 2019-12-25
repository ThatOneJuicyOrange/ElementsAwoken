using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Regular
{
    public class DrakoniteBlade : ModItem
    {
        public int CD = 0;
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 52; 
            
            item.damage = 13;
            item.knockBack = 6;

            item.useTime = 30;   
            item.useAnimation = 30;     
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.melee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;    

            //item.shoot = mod.ProjectileType("DrakoniteBurst");
            //item.shootSpeed = 4f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Blade");
            Tooltip.SetDefault("Unleash the power of dragons upon hitting an enemy");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Drakonite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void UpdateInventory(Player player)
        {
            CD--;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            if (player.ownedProjectileCounts[mod.ProjectileType("DragonBladeBlast")] < 1 && CD <= 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y + 4, 0f, 0f, mod.ProjectileType("DragonBladeBlast"), 0, 0, player.whoAmI, 0.0f, 0.0f);
                Main.PlaySound(SoundID.Item62, player.Center);
                CD = 30;
            }
        }
    }
}
