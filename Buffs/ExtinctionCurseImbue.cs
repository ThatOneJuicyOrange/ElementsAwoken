using ElementsAwoken;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class ExtinctionCurseImbue : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Weapon Imbue: Extinction Curse");
            Description.SetDefault("Melee attacks inflict Extinction Curse");
            Main.meleeBuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>(mod).extinctionCurseImbue = true;
        }
    }
}