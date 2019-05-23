using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class Chlorobyte : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = -1;

            item.maxStack = 999;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorobyte");
            Tooltip.SetDefault("Dont let it escape...");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 13));
        }
        public override void UpdateInventory(Player player)
        {
            if (item.stack >= 5)
            {
                int chance = 500 - item.stack * 3;
                if (chance < 60)
                {
                    chance = 60;
                }
                if (Main.rand.Next(chance) == 0)
                {
                    item.stack--;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), mod.ProjectileType("EscapedChlorobyte"), 0, 0f, player.whoAmI, player.whoAmI, 0f);
                }
            }
        }
    }
}
