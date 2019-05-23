using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Obsidious
{
    public class TerreneScepter : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 40;
            item.knockBack = 7.5f;
            item.mana = 15;

            item.noMelee = true;
            item.summon = true;
            item.sentry = true;

            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;

            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("TerreneMortar");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrene Scepter");
            Tooltip.SetDefault("Summons a terrene mortar to defend you");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int num154 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, type, damage, knockBack, Main.myPlayer, 0f, 0f);
            player.UpdateMaxTurrets();
            return false;
        }
    }
}