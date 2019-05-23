using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase1
{
    public class VoidKnight : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;

            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 1000;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.knockBackResist = 0.75f;
            npc.aiStyle = 3;

            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
            aiType = NPCID.Skeleton;
            animationType = NPCID.PossessedArmor;

            npc.buffImmune[24] = true;

            //banner = npc.type;
            //bannerItem = mod.ItemType("VoidKnightBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Knight");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (npc.localAI[1] == 0)
            {
                int orbitalCount = 3;
                for (int l = 0; l < orbitalCount; l++)
                {
                    int distance = 360 / orbitalCount;
                    int orbital = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("VoidKnightOrb"), npc.damage / 2, 0f, Main.myPlayer, l * distance, npc.whoAmI);
                }
                npc.localAI[1]++;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1); //Item spawn
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5)); //Item spawn
            }
        }
    }
}
