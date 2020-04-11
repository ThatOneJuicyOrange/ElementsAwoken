using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.CosmicObserver
{
    public class CosmicObserverHand : ModNPC
    {
        private int projectileBaseDamage = 20;

        public override void SetDefaults()
        {
            npc.lifeMax = 4500;
            npc.damage = 30;
            npc.defense = 25;
            npc.knockBackResist = 0f;

            npc.width = 50;
            npc.height = 54;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.netAlways = true;
            npc.noTileCollide = true;
            npc.npcSlots = 1f;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Observer Hand");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 60;
            npc.lifeMax = 6000;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 120;
                npc.lifeMax = 12000;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.localAI[1] == 120)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            if (npc.localAI[1] >= 300 || npc.localAI[1] < 120)
            {
                npc.frame.Y = 0;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CosmicObserverHand" + i), npc.scale);
                }
            }
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.5f, 0.5f, 0.5f);
            NPC parent = Main.npc[(int)npc.ai[1]];
            Player player = Main.player[parent.target];
            if (!parent.active)
            {
                npc.active = false;
            }
            npc.Center = parent.oldPos[4] + new Vector2(parent.width / 2, parent.height / 2) + new Vector2(50 * npc.ai[0], 40);
            npc.rotation = 0;
            npc.spriteDirection = (int)npc.ai[0];

            if (parent.ai[1] < 600 || MyWorld.awakenedMode)
            {
                npc.localAI[1]++;
                if (npc.localAI[1] == 120)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("ObserverSpell"), projectileBaseDamage, 0, Main.myPlayer, 0, npc.whoAmI);
                }
                if (npc.localAI[1] >= 300)
                {
                    npc.localAI[1] = 0;
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }   
}
