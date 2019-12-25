using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class Corroding : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Corroding");
            Description.SetDefault("It feels like liquid fire.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().corroding = true;
            if (Main.rand.Next(10) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 74, 0f, 0f, 150)];
                dust.velocity.X = Main.rand.NextFloat(-0.2f, 0.2f);
                dust.velocity.Y = Main.rand.NextFloat(0.2f, 1f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = false;
                dust.fadeIn = 1f;
            }
        }

    }
}