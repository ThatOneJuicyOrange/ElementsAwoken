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
            npc.npcSlots = 0.5f;
            npc.aiStyle = 14;
            npc.damage = 19;
            npc.width = 26; //324
            npc.height = 20; //216
            npc.defense = 5;
            npc.lifeMax = 34;
            npc.knockBackResist = 0.65f;
            animationType = 93;
            Main.npcFrameCount[npc.type] = 4;
            npc.value = Item.buyPrice(0, 0, 20, 0);
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
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && Main.evilTiles < 80 && Main.sandTiles < 80 && Main.dungeonTiles < 80 && !Main.hardMode ? 0.06f : 0f;
        }

        public override void NPCLoot()
        {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(3, 6)); //Item spawn
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (shootTimer > 0f)
            {
                shootTimer -= 1f;
            }
            if (Main.netMode != 1 && shootTimer == 0f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                float Speed = 8f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 6;
                int type = mod.ProjectileType("DragonBatFireball");
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                shootTimer = 120f;
            }          
        }
    }
}