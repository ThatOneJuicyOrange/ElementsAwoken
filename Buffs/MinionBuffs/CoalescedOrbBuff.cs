using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class CoalescedOrbBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Coalesced Orb");
            Description.SetDefault("Matter combined to make one staring eye");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("CoalescedOrb")] > 0)
            {
                modPlayer.coalescedOrb = true;
            }
            if (!modPlayer.coalescedOrb)
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