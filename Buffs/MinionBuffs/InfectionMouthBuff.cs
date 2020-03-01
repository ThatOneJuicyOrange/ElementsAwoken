using ElementsAwoken.Projectiles.Minions;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class InfectionMouthBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infection Mouth");
            Description.SetDefault("Dont inhale the particles");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[ModContent.ProjectileType<InfectionMouthMinion>()] > 0)
            {
                modPlayer.azanaMinions = true;
            }
            if (!modPlayer.azanaMinions)
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