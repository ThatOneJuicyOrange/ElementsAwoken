using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Prompts
{
    public class DarkenedSkies : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Darkened Skies");
            Description.SetDefault("Lightning strikes from the sky\nStorms happens more frequently\nDefeat Regaroth to stop this effect");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
    }
}