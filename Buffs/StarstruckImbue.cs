using ElementsAwoken;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class StarstruckImbue : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Weapon Imbue: Starstruck");
            Description.SetDefault("Melee attacks inflict Starstruck");
            Main.meleeBuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().starstruckImbue = true;
        }
    }
}