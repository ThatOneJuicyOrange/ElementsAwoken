using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class IcicleBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Icicle");
            Description.SetDefault("It never melts");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("IcicleMinion")] > 0)
            {
                modPlayer.icicleMinion = true;
            }
            if (!modPlayer.icicleMinion)
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