using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase1
{
    public class ReaverSlime : ModNPC
	{
        int shootTimer = 0;
        public override void SetDefaults()
		{
			npc.aiStyle = 1;
			npc.damage = 76;
			npc.width = 32; //324
			npc.height = 22; //216
			npc.defense = 20;
			npc.lifeMax = 1000;
			animationType = 1;
            npc.knockBackResist = 0.3f;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.alpha = 0;
			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            banner = npc.type;
            bannerItem = mod.ItemType("ReaverSlimeBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Slime");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.5f);
		}
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
        }
        public override void AI()
        {
            shootTimer--;
            Player P = Main.player[npc.target];
            if (Vector2.Distance(P.Center, npc.Center) < 100)
            {
                if (shootTimer <= 0)
                {
                    int numberProjectiles = 3;
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int type = mod.ProjectileType("ReaverCloud");
                    float speed = 5f;
                    float rotation = 1.7f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        float randSpeed = speed * (Main.rand.NextFloat(0.75f, 1f));
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * randSpeed) * -1), (float)((Math.Sin(rotation) * randSpeed) * -1)).RotatedByRandom(MathHelper.ToRadians(60));
                        Projectile.NewProjectile(vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, (int)(npc.damage * 0.75f), 0f, Main.myPlayer, 0f, 0f);
                    }
                    shootTimer = 15;
                }
            }
        }
        public override void NPCLoot()  //Npc drop
        {
            if (Main.rand.Next(6) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidJelly"), Main.rand.Next(1, 2)); //Item spawn
            }
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