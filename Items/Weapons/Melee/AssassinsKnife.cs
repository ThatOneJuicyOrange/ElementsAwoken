using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class AssassinsKnife : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.damage = 8;
            item.knockBack = 2f;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;
            item.useTurn = false;

            item.UseSound = SoundID.Item1;
            item.useStyle = 3;
            item.useAnimation = 5;
            item.useTime = 5;

            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 10, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Assassin's Knife");
            Tooltip.SetDefault("While holding the item, damage taken is increased by 2x");
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<MyPlayer>().damageTaken *= 2f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
            }
        }
    }
}
