using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Storyteller
{
    public class PoseidonsTrident : ModItem
    {
        public override void SetDefaults()
        {       
            item.damage = 66;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.useTime = 10;
            item.knockBack = 8.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 60;
            item.width = 60;
            item.maxStack = 1;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.shoot = mod.ProjectileType("PoseidonsTridentP");
            item.shootSpeed = 8f;
            item.rare = 8;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poseidon's Trident");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 409, damage, knockBack, player.whoAmI, 0.0f, 0.0f)];
            proj.magic = false;
            proj.melee = true;
            return true;
        }
    }
}
