using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossSummons.SummonProj
{
    public class StorytellerSpawner : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.penetrate = 1;
            projectile.timeLeft = 20;
            projectile.tileCollide = false;
            projectile.hostile = true;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Storyteller")) return true;
                return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.ai[1]++;

            if (projectile.ai[1] >= 0)
            {
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                /*NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Izaris"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Kirvein"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Krecheus"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Xernon"));
                NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Izaris"), 0f, 0f, 0, 0, 0);
                 NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Kirvein"), 0f, 0f, 0, 0, 0);
                 NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Krecheus"), 0f, 0f, 0, 0, 0);
                 NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Xernon"), 0f, 0f, 0, 0, 0);*/
                int iz = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Izaris"));
                int ki = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Kirvein"));
                int kr = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Krecheus"));
                int xe = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Xernon"));
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, iz, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, ki, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, kr, 0f, 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, xe, 0f, 0f, 0f, 0, 0, 0);
                Main.npc[iz].netUpdate = true;
                Main.npc[ki].netUpdate = true;
                Main.npc[kr].netUpdate = true;
                Main.npc[xe].netUpdate = true;
                projectile.ai[1] = -30;
            }
        }
    }
}