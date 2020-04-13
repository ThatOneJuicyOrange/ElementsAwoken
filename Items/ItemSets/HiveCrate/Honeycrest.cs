using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.HiveCrate
{
    public class Honeycrest : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            
            item.damage = 25;
            item.knockBack = 2;

            item.useTime = 24;
            item.useAnimation = 12;
            item.useStyle = 3;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 1;

            item.autoReuse = true;
            item.melee = true;
            item.useTurn = true;

            item.UseSound = SoundID.Item1;
            item.shootSpeed = 6f;
            item.shoot = ModContent.ProjectileType<Projectiles.HoneycrestStinger>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeycrest");
            Tooltip.SetDefault("Fires a stinger\nHitting enemies with the sword its self gives the player 'Honey'");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, player.direction * item.shootSpeed, 0, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.AddBuff(BuffID.Honey, 300);
        }
    }
}
