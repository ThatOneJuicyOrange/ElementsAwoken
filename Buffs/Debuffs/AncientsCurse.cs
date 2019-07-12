using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class AncientsCurse : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Ancients Curse");
            Description.SetDefault("");
        }
		public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("AncientGreen"))];
            dust.scale = Main.rand.NextFloat(0.5f,1.1f);
            dust.velocity *= 0.5f;
            dust.noGravity = true;

            npc.defense = (int)(npc.defense * 0.5f);
        }
    }
}