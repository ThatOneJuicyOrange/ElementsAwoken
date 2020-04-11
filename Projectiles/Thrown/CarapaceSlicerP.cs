using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class CarapaceSlicerP : ModProjectile
    {
        public int stickID = -1;
        public float stickOffX = 0;
        public float stickOffY = 0;
        public float stickTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(stickID);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            stickID = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = -1;

            projectile.aiStyle = 3;
            projectile.timeLeft = 1600;
            aiType = 52;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carapace Slicer");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(0,projectile.position);
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            return false;
        }
        public override void AI()
        {
            if (stickID > -1)
            {
                stickTimer++;
                NPC stick = Main.npc[stickID];
                if (stick.active)
                {
                    projectile.Center = stick.Center - new Vector2(stickOffX,stickOffY);
                    projectile.gfxOffY = stick.gfxOffY;
                }
                else stickID = -2;
                if (stickTimer > 30) stickID = -2;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (stickID == -1)
            {
                stickID = target.whoAmI;
                stickOffX = target.Center.X - projectile.Center.X + projectile.velocity.X * 3;
                stickOffY = target.Center.Y - projectile.Center.Y + projectile.velocity.Y * 3;
            }
        }
    }
}