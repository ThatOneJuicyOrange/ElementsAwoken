using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class CorruptPenguinBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Corrupt Penguin");
            Description.SetDefault("Does bonus damage against the clothier");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("CorruptPenguin")] > 0)
            {
                modPlayer.corruptPenguin = true;
            }
            if (!modPlayer.corruptPenguin)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}