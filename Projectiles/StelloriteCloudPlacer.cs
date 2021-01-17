using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class StelloriteCloudPlacer : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.timeLeft = 600;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellorite Cloud Placer");
        }
        public override void AI()
        {
            WorldGen.PlaceTile((int)projectile.position.X / 16, (int)projectile.position.Y / 16, TileID.Cloud);
            if (Main.netMode == NetmodeID.Server)NetMessage.SendData(MessageID.TileChange, -1, -1, null, 14, (int)projectile.position.X / 16, (int)projectile.position.Y / 16);
            projectile.Kill();
        }
    }
}