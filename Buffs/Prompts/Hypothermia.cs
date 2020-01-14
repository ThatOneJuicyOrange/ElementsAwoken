using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Prompts
{
    public class Hypothermia : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hypothermia");
            Description.SetDefault("The temperature plummets\nMovement speed reduced by 5% and applys random ice debuffs\nFrequent hailstorms\nBeing within 7 blocks of a campfire or lava removes this effect\nDefeat Permafrost to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 0.95f;
        }
    }
}