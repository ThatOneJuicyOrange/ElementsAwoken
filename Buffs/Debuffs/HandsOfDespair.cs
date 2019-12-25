using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class HandsOfDespair : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hands of Despair");
            Description.SetDefault("They grasp at your limbs, pulling you down");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().handsOfDespair = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().handsOfDespair = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("HandsOfDespair")] == 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("HandsOfDespair"), 0, 0f, player.whoAmI, 0f, player.whoAmI);
            }
        }
    }
}