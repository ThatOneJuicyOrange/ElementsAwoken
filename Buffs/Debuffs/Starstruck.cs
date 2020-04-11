using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.Other;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class Starstruck : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Starstruck");
            Description.SetDefault("Your will bends to your opponents\nEach hit by an enemy increases the damage the player takes");
            Main.debuff[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().starstruck = true;
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame)];
                dust.velocity.Y = Main.rand.NextFloat(-0.5f, -5f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = false;
                dust.fadeIn = 0.2f;
            }
            if (Main.rand.Next(10) == 0)
            {
                Projectile.NewProjectile(player.position.X + Main.rand.Next(player.width), player.position.Y + Main.rand.Next(player.height), Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-1, 1), ProjectileType<StarstruckP>(), 0, 0, Main.myPlayer, Main.rand.Next(0, 5));
            }
        }

    }
}