using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Regular
{
    public class DrakoniteBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 13;     
            item.melee = true;     
            item.width = 60;   
            item.height = 60;    
            item.useTime = 30;   
            item.useAnimation = 30;     
            item.useStyle = 1;          
            item.knockBack = 6;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;    
            item.UseSound = SoundID.Item1;  
            item.autoReuse = true; 
            item.shoot = mod.ProjectileType("DrakoniteBurst");
            item.shootSpeed = 4f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Blade");
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
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 100);
        }
    }
}
