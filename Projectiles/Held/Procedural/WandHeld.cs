using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Held.Procedural
{
    public class WandHeld : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wand Held");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            string texLoc = "";
            if (projectile.ai[0] == 0) texLoc = "Normal";
            if (projectile.ai[0] == 1) texLoc = "Desert";
            if (projectile.ai[0] == 2) texLoc = "Fire";
            if (projectile.ai[0] == 3) texLoc = "Sky";
            if (projectile.ai[0] == 4) texLoc = "Frost";
            if (projectile.ai[0] == 5) texLoc = "Water";
            if (projectile.ai[0] == 6) texLoc = "Void";
            Texture2D tex = mod.GetTexture("Items/Weapons/Procedural/" + texLoc + projectile.ai[1]);
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPos, null, lightColor, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            string texLoc = "";
            if (projectile.ai[0] == 0) texLoc = "Normal";
            if (projectile.ai[0] == 1) texLoc = "Desert";
            if (projectile.ai[0] == 2) texLoc = "Fire";
            if (projectile.ai[0] == 3) texLoc = "Sky";
            if (projectile.ai[0] == 4) texLoc = "Frost";
            if (projectile.ai[0] == 5) texLoc = "Water";
            if (projectile.ai[0] == 6) texLoc = "Void";
            Texture2D tex = mod.GetTexture("Items/Weapons/Procedural/" + texLoc + projectile.ai[1]);

            projectile.localAI[0]++;
            if (projectile.localAI[0] > player.HeldItem.useAnimation) projectile.Kill();

            Vector2 offset = projectile.velocity;
            offset.Normalize(); // makes the value = 1
            offset *= tex.Width / 3;

            Vector2 vector24 = player.RotatedRelativePoint(player.MountedCenter, true) + offset.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2));
            projectile.direction = player.direction;
            player.heldProj = projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            projectile.position.X = vector24.X - (float)(projectile.width / 2);
            projectile.position.Y = vector24.Y - (float)(projectile.height / 2);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)(Math.PI / 4);
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
        }
    }
}
 