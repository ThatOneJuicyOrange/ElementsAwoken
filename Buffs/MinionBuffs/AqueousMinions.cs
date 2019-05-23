using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class AqueousMinions : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Aqueous's Minions");
            Description.SetDefault("The minions of Aqueous defend you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            /*if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly1")] > 0)
            {
                modPlayer.aqueousMinions = true;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly2")] > 0)
            {
                modPlayer.aqueousMinions = true;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly3")] > 0)
            {
                modPlayer.aqueousMinions = true;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("AqueousMinionFriendly4")] > 0)
            {
                modPlayer.aqueousMinions = true;
            }*/
            if (!modPlayer.aqueousMinions)
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