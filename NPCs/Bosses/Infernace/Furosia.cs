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

namespace ElementsAwoken.NPCs.Bosses.Infernace
{
    [AutoloadBossHead]
    public class Furosia : ModNPC
    {
        int projectileBaseDamage = 35;
        public override void SetDefaults()
        {
            npc.width = 188;
            npc.height = 190;

            npc.lifeMax = 5000;
            npc.damage = 15;
            npc.defense = 10;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.scale = 1f;
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Furosia");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 30;
            npc.lifeMax = 7500;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 10000;
                npc.damage = 45;
                npc.defense = 15;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 4)
            {
                npc.frame.Y = 0;
            }

            //harpy rotation
            npc.rotation = npc.velocity.X * 0.1f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 180, false);
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.spriteDirection = npc.direction;
            // despawn if no players
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.ai[0] = 0;
            }
            if (!NPC.AnyNPCs(mod.NPCType("Infernace")))
            {
                npc.active = false;
            }

            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.4f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            Move(P, 4f);
            npc.ai[1]--;
            if (npc.ai[1] <= 0)
            {
                Fireball(P, 9.5f, projectileBaseDamage);
                npc.ai[1] = 50f;
            }
        }
        private void Move(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity = toTarget * moveSpeed;
        }
        private void Fireball(Player P, float speed, int damage)
        {
            Vector2 npcCenter = new Vector2(npc.Center.X, npc.Center.Y);
            int type = mod.ProjectileType("FurosiaFireball");
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
            float rotation = (float)Math.Atan2(npcCenter.Y - P.Center.Y, npcCenter.X - P.Center.X);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 30, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
