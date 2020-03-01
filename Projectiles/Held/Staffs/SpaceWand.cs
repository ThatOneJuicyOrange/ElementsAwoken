using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Held
{
    public class SpaceWand : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Items/ItemSets/Meteor/SpaceWand"; } }

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
            DisplayName.SetDefault("Space Wand");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPos, null, lightColor, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            if (projectile.localAI[0] == 1)
            {
                Texture2D crossTex = mod.GetTexture("Projectiles/MeteorCross");
                Vector2 crossOrigin = new Vector2(crossTex.Width * 0.5f, crossTex.Height * 0.5f);

                spriteBatch.Draw(crossTex, new Vector2(Main.mouseX, Main.mouseY), null, Color.White, 0f, crossOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.ai[1] += 1f;
            if (Main.myPlayer == projectile.owner)
            {
                if ((player.channel && (player.HeldItem.mana > 0 && player.CheckMana(player.inventory[player.selectedItem].mana, false, false))) && !player.noItems && !player.CCed)
                {
                    if (projectile.ai[1] >= 40 && projectile.ai[0] == 0)
                    {
                        if (Collision.CanHitLine(projectile.position + projectile.velocity * 2, 2, 2, Main.MouseWorld, 2, 2))
                        {
                            CreateProj();
                            projectile.ai[0]++;
                            projectile.localAI[0] = 0;
                        }
                        else
                        {
                            projectile.localAI[0] = 1;
                        }
                    }
                }
                else
                {
                    for (int p = 0; p < Main.projectile.Length; p++)
                    {
                        if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].type == mod.ProjectileType("MeteoricFireball"))
                        {
                            Main.projectile[p].Kill();
                            break;
                        }
                    }
                    projectile.Kill();
                }
            }

            int soundDelay = (int)MathHelper.Lerp(12,0, projectile.ai[1] / 40);

            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = 2 + soundDelay;
                projectile.soundDelay *= 2;
                Main.PlaySound(SoundID.Item15, projectile.position);
            }

            Vector2 offset = projectile.velocity;
            offset.Normalize();
            offset *= Main.projectileTexture[projectile.type].Width / 3;

            Vector2 vector24 = player.RotatedRelativePoint(player.MountedCenter, true) + offset.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2));
            player.heldProj = projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            projectile.position.X = vector24.X - (float)(projectile.width / 2);
            projectile.position.Y = vector24.Y - (float)(projectile.height / 2);
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));


            if (projectile.ai[1] < 40)
            {
                Vector2 add = projectile.Center + projectile.velocity * 4f;
                int width = 16;
                Dust dust = Main.dust[Dust.NewDust(add - Vector2.One * (width / 2), width, width, 6)];
                dust.velocity = Main.rand.NextVector2Square(-3f, 3f);
                dust.noGravity = true;
                dust.customData = player;
            }


            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)(Math.PI / 4);
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }

            float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
            Vector2 vector3 = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
            if (player.gravDir == -1f)
            {
                value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
            }
            Vector2 vector4 = Vector2.Normalize(value2);
            if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
            {
                vector4 = -Vector2.UnitY;
            }
            vector4 *= scaleFactor;
            if (vector4.X != projectile.velocity.X || vector4.Y != projectile.velocity.Y)
            {
                projectile.netUpdate = true;
            }
            projectile.velocity = vector4;
        }
        private void CreateProj()
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, mod.ProjectileType("MeteoricFireball"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);

            for (int p = 0; p < 10; p++)
            {
                int width = 16;
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y) - Vector2.One * (width / 2), 16, 16, 6)];
                dust.velocity = Main.rand.NextVector2Square(-3f, 3f);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1f, 2f);
            }

            Main.PlaySound(SoundID.Item8, projectile.position);
        }
    }
}
 