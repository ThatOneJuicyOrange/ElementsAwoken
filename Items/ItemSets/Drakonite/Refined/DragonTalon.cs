using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    public class DragonTalon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 68;
            item.knockBack = 6;

            item.melee = true;
            item.autoReuse = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("DragonTalonBall");
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Talon");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Dragonfire"), 200);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2 + Main.rand.Next(0, 1);
            for (int k = 0; k < numberProjectiles; k++)
            {
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                vector2.X = player.position.X;
                vector2.Y = player.Center.Y + Main.rand.Next(-15, 15);
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, mod.ProjectileType("DragonTalonBall"), damage, item.knockBack, Main.myPlayer, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
