using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ScourgeFighter
{
    public class ScourgeDrive : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.expert = true;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Drive");
            Tooltip.SetDefault("Greatly increases movement speeds\nWhen the player is going above 60mph:\nYou explode on hit\nDamage is increased by 25%");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 4));
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0, 0, 0, default(Color))];
                dust.noGravity = true;
            }
            player.accRunSpeed *= 1.5f;
            player.GetModPlayer<MyPlayer>(mod).scourgeDrive = true;
        }
    }
}
