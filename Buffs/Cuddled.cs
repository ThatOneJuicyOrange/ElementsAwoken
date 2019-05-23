using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class Cuddled : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            DisplayName.SetDefault("Cuddled");
            Description.SetDefault("You feel warm inside, melee attacks are decreased by 20% but life regen is increased");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 0.8f;
            player.lifeRegen = +2;
        }
    }
}