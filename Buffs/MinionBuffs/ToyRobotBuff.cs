using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class ToyRobotBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Toy Robot");
            Description.SetDefault("It winds itself up!");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("ToyRobot")] > 0)
            {
                modPlayer.toyRobot = true;
            }
            if (!modPlayer.toyRobot)
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