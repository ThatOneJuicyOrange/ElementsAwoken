using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class DrakoniteSkinBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Drakonite Skin");
            Description.SetDefault("Immune to lava and Incineration\nSafely protects against the Volcanic Plateau lava\nAllows the player to swim through lava faster\n50% increased fire resistance");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[24] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Incineration>()] = true;
            if (player.lavaWet) player.ignoreWater = true;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.SetFireResistance(0.5f);
        }
    }
}