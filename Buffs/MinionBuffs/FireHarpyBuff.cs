using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class FireHarpyBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fire Harpy");
            Description.SetDefault("It serves you willingly");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("FireHarpy")] > 0)
            {
                modPlayer.fireHarpy = true;
            }
            if (!modPlayer.fireHarpy)
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