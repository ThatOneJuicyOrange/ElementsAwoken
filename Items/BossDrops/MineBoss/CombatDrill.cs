using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.MineBoss
{
    public class CombatDrill : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;

            item.damage = 20;
            item.knockBack = 6;

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;

            item.melee = true;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.pick = 100;
            item.tileBoost--;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item23;
            item.shoot = ModContent.ProjectileType<Projectiles.Held.Drills.CombatDrillHeld>();
            item.shootSpeed = 52f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combat Drill");
            Tooltip.SetDefault("Left click to drill\nRight click to launch the drill");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.CombatDrillHead>()] >= 1)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                item.channel = false;
                Vector2 speed = new Vector2(speedX, speedY);
                speed.Normalize();
                speed *= 13;
                Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<Projectiles.CombatDrillHead>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f)];
                proj.localAI[0] = Projectile.NewProjectile(position.X, position.Y, speedX * 0.35f, speedY * 0.35f, ModContent.ProjectileType<Projectiles.Held.Drills.CombatDrillHeld2>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
                return false;
            }
            else
            {
                item.channel = true;
                return true;
            }
        }
    }
}