using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class Soulsword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 25;
            item.knockBack = 3;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 4;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soulsword");
            Tooltip.SetDefault("Releases souls from enemies on hit");
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("SoulswordSoul")] < 3)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, -1.5f, mod.ProjectileType("SoulswordSoul"), damage, knockback, Main.myPlayer);
            }
        }
    }
}
