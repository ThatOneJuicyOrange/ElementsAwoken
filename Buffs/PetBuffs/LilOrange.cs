using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class LilOrange : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lil Orange");
            Description.SetDefault("A little orange follows you closely");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("LilOrange")] > 0)
            {
                modPlayer.lilOrange = true;
            }
            if (!modPlayer.lilOrange)
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