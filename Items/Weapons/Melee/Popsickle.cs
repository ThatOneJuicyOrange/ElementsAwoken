using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Popsickle : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 78;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 20;
            item.useTurn = true;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item71;
            item.shoot = mod.ProjectileType("BubbleRed");
            item.shootSpeed = 24f;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Popsickle");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2 + Main.rand.Next(2); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (Main.rand.Next(4))
                {
                    case 0: type = mod.ProjectileType("BubbleBlue"); break;
                    case 1: type = mod.ProjectileType("BubbleRed"); break;
                    case 2: type = mod.ProjectileType("BubblePurple"); break;
                    case 3: type = mod.ProjectileType("BubbleGreen"); break;
                    default: break;
                }
                float rand = Main.rand.Next(2, 4);
                rand = rand * 4;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X / rand, perturbedSpeed.Y / rand, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(7) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 63, 0, 0, 200, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                Main.dust[dust].scale *= 1f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BubbleGun, 1);
            recipe.AddIngredient(ItemID.DeathSickle, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
