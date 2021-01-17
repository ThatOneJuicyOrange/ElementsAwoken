using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.QuestItems
{
    public class OrderMessage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.questItem = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Secret Message");
            Tooltip.SetDefault("Contains information about Order activities ");
        }
    }
}
