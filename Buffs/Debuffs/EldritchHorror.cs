using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class EldritchHorror : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Eldritch Horror");
            Description.SetDefault("Your mind cannot comprehend its power...\nSanity is drained");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            modPlayer.sanityRegen = -5;
        }
    }
}