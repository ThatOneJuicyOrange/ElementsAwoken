using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class HappyCloudBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Happy Little Cloud");
            Description.SetDefault("Bob Ross");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("HappyCloud")] > 0)
            {
                modPlayer.happyCloud = true;
            }
            if (!modPlayer.happyCloud)
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