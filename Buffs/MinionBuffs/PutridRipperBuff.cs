using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class PutridRipperBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Putrid Ripper");
            Description.SetDefault("Claws like knives, spit like acid");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("PutridRipper")] > 0)
            {
                modPlayer.putridRipper = true;
            }
            if (!modPlayer.putridRipper)
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