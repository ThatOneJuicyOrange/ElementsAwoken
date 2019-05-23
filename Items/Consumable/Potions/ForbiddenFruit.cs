using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class ForbiddenFruit : ModItem
    {
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.width = 20;
            item.height = 28;
            item.value = 2000;
            item.rare = 2;
            item.buffType = mod.BuffType("DemonSkinBuff");
            item.buffTime = 3600;
            //item.potion = true;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Fruit");
            Tooltip.SetDefault("Gives all potion buffs for a minute\nDunno why this is still in the game, it has never had a crafting recipe");
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.AmmoReservation, 3600, true);
            player.AddBuff(BuffID.Archery, 3600, true);
            player.AddBuff(BuffID.Endurance, 3600, true);
            player.AddBuff(BuffID.Heartreach, 3600, true);
            player.AddBuff(BuffID.Inferno, 3600, true);
            player.AddBuff(BuffID.Ironskin, 3600, true);
            player.AddBuff(BuffID.Lifeforce, 3600, true);
            player.AddBuff(BuffID.MagicPower, 3600, true);
            player.AddBuff(BuffID.ManaRegeneration, 3600, true);
            player.AddBuff(BuffID.Rage, 3600, true);
            player.AddBuff(BuffID.Regeneration, 3600, true);
            player.AddBuff(BuffID.ManaRegeneration, 3600, true);
            player.AddBuff(BuffID.Summoning, 3600, true);
            player.AddBuff(BuffID.Swiftness, 3600, true);
            player.AddBuff(BuffID.Thorns, 3600, true);
            player.AddBuff(BuffID.Titan, 3600, true);
            player.AddBuff(BuffID.Wrath, 3600, true);
            return true;
        }
    }
}
