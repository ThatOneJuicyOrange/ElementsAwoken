using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class WarriorSlice : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 36;

            projectile.aiStyle = -1;

            projectile.alpha = 255;
            projectile.timeLeft = 15;

            projectile.hostile = true;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Warrior");
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)projectile.ai[0]];
            projectile.Center = parent.Center + new Vector2(30 * parent.direction, 0);
        }
    }
}