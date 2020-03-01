using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class PsychologistSanity : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Counselling Session");
            Description.SetDefault("Increases sanity regeneration");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            modPlayer.sanityRegen += 2;
            modPlayer.AddSanityRegen(2, "Counselling Session");
        }
    }
}   