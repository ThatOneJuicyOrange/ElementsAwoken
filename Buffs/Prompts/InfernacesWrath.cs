using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Prompts
{
    public class InfernacesWrath : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infernace's Wrath");
            Description.SetDefault("Meteors rain from the sky\nWhen approaching hell the player may catch fire\nDefeat Infernace to stop this effect");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 0.95f;
            int rand = 5000;
            if (player.Center.Y / 16 > Main.maxTilesY * 0.35)
            {
                rand = 4000;
            }
            if (player.Center.Y / 16 > Main.maxTilesY * 0.6)
            {
                rand = 2500;
            }
            if (player.Center.Y / 16 > Main.maxTilesY * 0.8)
            {
                rand = 2000;
            }
            if (player.ZoneUnderworldHeight)
            {
                rand = 1200;
            }
            if (Main.rand.Next(rand) == 0 && player.Center.Y / 16 > Main.maxTilesY * 0.225)
            {
                player.AddBuff(BuffID.OnFire, Main.rand.Next(120, 300));
            }
        }
    }
}