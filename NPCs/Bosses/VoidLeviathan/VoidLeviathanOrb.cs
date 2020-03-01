using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan
{
    [AutoloadBossHead]

    public class VoidLeviathanOrb : ModNPC
    {
        private float shootTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.aiStyle = -1;

            npc.damage = 0;
            npc.lifeMax = 30000;
            npc.defense =  90;
            npc.knockBackResist = 0f;

            npc.scale = 1.5f;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath56;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;

            NPCsGLOBAL.ImmuneAllEABuffs(npc);
            // all vanilla buffs
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void's Orb");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (MyWorld.awakenedMode)
            {
                npc.defense = 120;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
if (npc.life <= 0) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("VoidOrbDestroyed"), 0, 0f, Main.myPlayer);
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            if (!NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead"))) npc.active = false;

            shootTimer--;
            if (shootTimer <= 0 && Vector2.Distance(P.Center, npc.Center) < 600 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int projectileBaseDamage = 70;
                int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.8f);

                Projectile strike = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), mod.ProjectileType("VoidOrbProj"), projDamage, 0f, Main.myPlayer)];
                strike.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                shootTimer = 60;
            }

            if (npc.ai[2] == 0)
            {
                if (!ModContent.GetInstance<Config>().lowDust)
                {
                    int numDusts = 20;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.One * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                        Vector2 velocity = position - npc.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, DustID.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 6f;
                    }
                }
                npc.scale = 0.1f;
                npc.ai[2]++;
            }
            if (npc.scale < 1.5) npc.scale += 1f / 30f;
            else npc.scale = 1.5f;

        }
    }
}