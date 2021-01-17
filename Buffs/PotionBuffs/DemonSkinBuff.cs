using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class DemonSkinBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Demon's Giving");
            Description.SetDefault("Increased defense by 3, 10% increased movement and melee speed");
        }
        public override void Update(Player player, ref int buffIndex)
        {                                             //
            //player.AddBuff(mod.BuffType("DemonSkinBuff"), 1); //this is an example of how to add your own buff
            player.statDefense += 3;
            player.meleeSpeed += 0.1f;
            player.moveSpeed += 0.1f;
        }
    }
}