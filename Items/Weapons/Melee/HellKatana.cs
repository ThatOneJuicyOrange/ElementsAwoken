using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class HellKatana : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 58; 
            
            item.damage = 32;
            item.knockBack = 5;

            item.UseSound = SoundID.Item1;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;


            item.value = Item.buyPrice(0, 2, 20, 0);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Katana");
            Tooltip.SetDefault("Pure hell");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 130);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddIngredient(ItemID.Katana, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {

                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.2f;
        }
    }
}
