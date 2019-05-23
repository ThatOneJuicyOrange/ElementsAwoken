using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class Eyeball : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Eyeball");
            Description.SetDefault("The Celestials minions follow you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("EyeballMinion")] > 0)
            {
                modPlayer.eyeballMinion = true;
            }
            if (!modPlayer.eyeballMinion)
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