using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class DeathwatcherBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Deathwatcher");
            Description.SetDefault("Just looking at it gives you chills");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("Deathwatcher")] > 0)
            {
                modPlayer.deathwatcher = true;
            }
            if (!modPlayer.deathwatcher)
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