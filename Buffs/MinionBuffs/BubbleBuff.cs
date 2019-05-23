using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class BubbleBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bubble");
            Description.SetDefault("I like bubbles!");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("Bubble")] > 0)
            {
                modPlayer.bubble = true;
            }
            if (!modPlayer.bubble)
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