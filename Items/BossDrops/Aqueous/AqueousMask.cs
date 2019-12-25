using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Aqueous
{
    public class AqueousMask : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous' Crystal");
            Tooltip.SetDefault("Summons the 4 minions of Aqueous to defend you\nYou leave behind a trail of bubbles");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //bubbles
            if (((double)player.velocity.X > 0 || (double)player.velocity.Y > 0 || (double)player.velocity.X < -0.1 || (double)player.velocity.Y < -0.1))
            {
                if (Main.rand.Next(3) == 0)
                {
                    int projectile1 = Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, 30, 5f, player.whoAmI, 0f, 0f);
                    Main.projectile[projectile1].timeLeft = 40;
                }
            }
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.aqueousMinions = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("AqueousMinions")) == -1)
                {
                    player.AddBuff(mod.BuffType("AqueousMinions"), 2, true);
                }
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly1")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("AqueousMinionFriendly1"), 60, 2f, Main.myPlayer, 0f, 0f);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly2")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("AqueousMinionFriendly2"), 60, 2f, Main.myPlayer, 0f, 0f);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly3")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("AqueousMinionFriendly3"), 60, 2f, Main.myPlayer, 0f, 0f);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly4")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("AqueousMinionFriendly4"), 60, 2f, Main.myPlayer, 0f, 0f);
            }
        }
    }
}
