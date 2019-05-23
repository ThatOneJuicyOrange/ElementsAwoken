using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.Volcanox
{
    public class EmberBurst : ModItem
    {
        public override void SetDefaults()
        {       
            item.width = 66;
            item.damage = 210;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 19;
            item.useStyle = 5;
            item.useTime = 19;
            item.knockBack = 8.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 66;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 80, 0, 0);
            item.rare = 11;
            item.shoot = mod.ProjectileType("EmberBurstP");
            item.shootSpeed = 11f;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ember Burst");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX / 2, speedY / 2, mod.ProjectileType("FirelashFlames"), damage / 2, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if(Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
            }
        }
    }
}
