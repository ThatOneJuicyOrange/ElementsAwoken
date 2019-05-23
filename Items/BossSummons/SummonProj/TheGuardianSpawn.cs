using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossSummons.SummonProj
{
    public class TheGuardianSpawn : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.penetrate = 1;
            projectile.timeLeft = 20;
            projectile.tileCollide = false;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian Spawn");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.ai[1]++;

            if (projectile.ai[1] >= 0)
            {
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                Vector2 spawnAt = player.Center + new Vector2(0f + 200, (float)player.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("TheGuardian"));
                //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TheGuardian"));
                projectile.ai[1] = -30;
            }
        }
    }
}