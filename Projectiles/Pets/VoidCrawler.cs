using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Pets
{
    public class VoidCrawler : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Penguin);
            projectile.height = 20;
            projectile.width = 34;
            aiType = ProjectileID.Penguin;
            Main.projFrames[projectile.type] = 4;
            Main.projPet[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Crawler");
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.penguin = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.voidCrawler = false;
            }
            if (modPlayer.voidCrawler)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}