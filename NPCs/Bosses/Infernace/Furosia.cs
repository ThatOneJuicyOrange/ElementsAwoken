using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        private int projectileBaseDamage = 35;
        private const int tpDuration = 40;

        private float telePosX = 0;
        private float telePosY = 0;

        public float dashAI = 0;
        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float shootTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float tpAlphaChangeTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float tpDir
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(telePosX);
            writer.Write(telePosY);
            writer.Write(dashAI);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            telePosX = reader.ReadSingle();
            telePosY = reader.ReadSingle();
            dashAI = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            npc.width = 188;
            npc.height = 190;

            npc.aiStyle = -1;

            npc.lifeMax = 2000;
            npc.damage = 15;
            npc.defense = 6;
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
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Furosia");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 30;
            npc.lifeMax = 3000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 5000;
                npc.damage = 45;
                npc.defense = 10;
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
            if (!P.active || P.dead) npc.TargetClosest(true);
            npc.direction = Math.Sign(P.Center.X - npc.Center.X);
            npc.spriteDirection = npc.direction;
            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.4f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            if (!NPC.AnyNPCs(mod.NPCType("Infernace")))npc.active = false;

            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            if (tpAlphaChangeTimer > 0)
            {
                tpAlphaChangeTimer--;
                if (tpAlphaChangeTimer > (int)(tpDuration / 2))
                {
                    npc.alpha += 26;
                }
                if (tpAlphaChangeTimer == (int)(tpDuration / 2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.position.X = telePosX - npc.width / 2;
                    npc.position.Y = telePosY - npc.height / 2;
                    npc.netUpdate = true;
                }
                if (tpAlphaChangeTimer < (int)(tpDuration / 2))
                {
                    npc.alpha -= 26;
                    if (npc.alpha <= 0)
                    {
                        tpAlphaChangeTimer = 0;
                    }
                }
            }

            aiTimer++;
            if (aiTimer < 300)
            {
                dashAI++;
                if (dashAI == 30)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    float dashSpeed = Main.expertMode ? 7 : 5;
                    if (MyWorld.awakenedMode) dashSpeed = 9;
                    Dash(P, dashSpeed);
                }
                if(dashAI > 70) npc.velocity *= 0.96f;
                if (dashAI >= 120) dashAI = 0;
            }
            else
            {
                npc.velocity =  Vector2.Zero;
                if (aiTimer == 300)
                {
                    tpDir = Main.rand.Next(2) == 0 ? -1 : 1;
                    Teleport(P.Center.X + 600 * tpDir, P.Center.Y);
                }
                if ((aiTimer == 380 || (aiTimer == 390 && Main.expertMode) || (aiTimer == 400 && MyWorld.awakenedMode)) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float projSpeed = Main.expertMode ? 14 : 10;
                    if (MyWorld.awakenedMode) projSpeed = 18;

                    int damage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : (int)(projectileBaseDamage);
                    if (MyWorld.awakenedMode) damage = (int)(projectileBaseDamage * 2f);

                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -tpDir * projSpeed, 0, mod.ProjectileType("FurosiaSpike"), damage, 0f, Main.myPlayer)];
                    proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                if (aiTimer == 420)
                {
                    Teleport(P.Center.X + Main.rand.Next(400, 400), P.Center.Y - 200);
                    aiTimer = 0;
                }
            }
        }
        private void Dash(Player P,float dashSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity = toTarget * dashSpeed;
        }
        private void Shoot(Player P, float speed, int damage,int type)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 30, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
        }
        private void Teleport(float posX, float posY)
        {
            tpAlphaChangeTimer = tpDuration;
            telePosX = posX;
            telePosY = posY;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
