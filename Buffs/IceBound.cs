using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class IceBound : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ice Bound");
            Description.SetDefault("Your bones and joints freeze");        
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>(mod).iceBound = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 67);
            Main.dust[num1].scale = 1.2f;
            Main.dust[num1].velocity /= 1f;
            Main.dust[num1].noGravity = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>(mod).iceBound = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, 67);
            Main.dust[num1].scale = 1.2f;
            Main.dust[num1].velocity /= 1f;
            Main.dust[num1].noGravity = true;
            player.velocity.Y = 0f;
            player.velocity.X = 0f;
        }

    }
}