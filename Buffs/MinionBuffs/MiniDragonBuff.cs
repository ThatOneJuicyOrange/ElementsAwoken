using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class MiniDragonBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mini Dragon");
            Description.SetDefault("Its cute and scary at the same time");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("MiniDragon")] > 0)
            {
                modPlayer.miniDragon = true;
            }
            if (!modPlayer.miniDragon)
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