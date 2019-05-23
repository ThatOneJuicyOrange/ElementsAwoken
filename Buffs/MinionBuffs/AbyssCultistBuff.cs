using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class AbyssCultistBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Abyss Cultist");
            Description.SetDefault("Praise the dark one...");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("AbyssCultist")] > 0)
            {
                modPlayer.abyssCultist = true;
            }
            if (!modPlayer.abyssCultist)
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