using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Mounts
{
    public class CrowBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Crow");
            Description.SetDefault("Gracefully soar through the skies");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(mod.MountType("CrowMount"), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
