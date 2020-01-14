using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Drakonite.Lesser
{
    public class DragonBat : ModNPC
    {
        public float shootTimer = 180f;

        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 20; 
            
            npc.aiStyle = 14;
            animationType = 93;

            npc.damage = 19;
            npc.defense = 5;
            npc.lifeMax = 34;
            npc.knockBackResist = 0.7f;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.npcSlots = 0.5f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;

            npc.buffImmune[24] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonBatBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Bat");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.spawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.spawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && Main.evilTiles < 80 && Main.sandTiles < 80 && Main.dungeonTiles < 80 && !Main.hardMode ? 0.06f : 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode) player.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 300 : 120, false);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            shootTimer -= 1f;
            if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer == 0f && Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height))
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                float Speed = 6f;
                int damage = 6;
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("DragonBatFireball"), damage, 0f, 0);
                shootTimer = 120f;
            }          
        }
    }
}