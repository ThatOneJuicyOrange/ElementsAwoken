using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Events.RadiantRain.Enemies
{
    public class StellarStarfish : ModNPC
    {
        private float timer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 32;

            npc.aiStyle = 44;
            npc.lifeMax = 7600;
            npc.damage = 100;
            npc.defense = 40;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath7;

            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.knockBackResist = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Starfish");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 15000;
            npc.damage = 150;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 20000;
                npc.damage = 200;
                npc.defense = 65;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.NextBool()) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Radia>(), 1);
        }
        public override void AI()
        {
            FallThroughPlatforms();
            Player P = Main.player[npc.target];
            npc.rotation += npc.velocity.X * 0.02f;
            timer--;
            if (Math.Abs(P.Center.X - npc.Center.X) < 60 && timer <= 0 && P.Center.Y > npc.Center.Y && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int n = 0; n < 3; n++)
                {
                    float speed = 2f;
                    Vector2 vector4 = new Vector2(0, speed).RotatedByRandom(MathHelper.ToRadians(50));
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector4.X, vector4.Y, ProjectileType<RadiantStar>(), npc.damage / 3, 0f, Main.myPlayer, 0f, 0f);
                    timer = 60f;
                }
            }
            if (Math.Abs(P.Center.X - npc.Center.X) < 120 && Math.Abs(P.Center.Y - npc.Center.Y) < 300)  npc.velocity.Y -= 0.04f;
            if (!GetInstance<Config>().lowDust)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.velocity *= 0.1f;
                    dust.fadeIn = 0.8f;
                    dust.scale *= 0.2f;
                }
            }
        }
      
        private void FallThroughPlatforms()
        {
            Player P = Main.player[npc.target];
            Vector2 platform = npc.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.type] && npc.Bottom.Y < P.Bottom.Y && platformTile.active()) npc.position.Y += 0.3f;
        }
    }
}
