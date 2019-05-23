using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan.Minions
{
    public class VoidProbe : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 5;
            npc.damage = 50;
            npc.width = 30; //324
            npc.height = 26; //216
            npc.defense = 30;
            npc.lifeMax = 500;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Probe");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1000;
            npc.damage = 75;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 100);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.ai[0]++;
            if (npc.ai[0] == 70)
            {
                float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 20;  //projectile damage
                int type = mod.ProjectileType("VoidBolt");  //put your projectile
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                npc.ai[0] = 0;
            }
            if (!NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead")))
            {
                npc.active = false;
            }
        }
    }
}