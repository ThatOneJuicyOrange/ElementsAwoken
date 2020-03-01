using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class VoidKnight : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;

            npc.damage = 100;
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

            banner = npc.type;
            bannerItem = mod.ItemType("VoidKnightBanner");

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            npc.defense = 60;
            npc.damage = 200;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 3500;
                npc.defense = 75;
                npc.damage = 350;
            }
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
            if (npc.localAI[0]==0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int orbitalCount = 3;
                for (int l = 0; l < orbitalCount; l++)
                {
                    int distance = 360 / orbitalCount;
                    int orbital = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("VoidKnightOrb"), npc.damage / 2, 0f, Main.myPlayer, l * distance, npc.whoAmI);
                }
                npc.localAI[0]++;
            }
            //STOP CLUMPING FOOLS
            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (k != npc.whoAmI && other.type == npc.type && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                {
                    const float pushAway = 0.05f;
                    if (npc.position.X < other.position.X)
                    {
                        npc.velocity.X -= pushAway;
                    }
                    else
                    {
                        npc.velocity.X += pushAway;
                    }
                }
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5)); 
            }
        }
    }
}
