using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using ReLogic.Utilities;

namespace ElementsAwoken.Projectiles.Other
{
    public class AbilityGiver : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ability");
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            projectile.ai[1]++;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if ((modPlayer.canSandstormA && projectile.ai[0] == 0) ||
                    (modPlayer.canTimeA && projectile.ai[0] == 1) ||
                    (modPlayer.canRainA && projectile.ai[0] == 2) ||
                    (modPlayer.canWispA && projectile.ai[0] == 3)) projectile.Kill();
                else
                {

                    if (projectile.ai[0] < 2)
                    {
                        int dustType = 75;
                        if (projectile.ai[0] == 1) dustType = Main.rand.NextBool() ? 135 : DustID.PinkFlame;
                        for (int l = 0; l < 5; l++)
                        {
                            Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                            Dust dust = Dust.NewDustPerfect(position, dustType, Vector2.Zero);
                            dust.velocity.Y = Main.rand.NextFloat(-10, -5);
                            dust.noGravity = true;
                            dust.fadeIn = 0.9f;
                        }
                    }
                    if (projectile.ai[1] >= 90)
                    {
                        ActiveSound activeSound = Main.GetActiveSound(SlotId.FromFloat(projectile.localAI[1]));
                        if (projectile.soundDelay == 0)
                        {
                            projectile.soundDelay = -1;
                            projectile.localAI[1] = Main.PlayTrackedSound(SoundID.DD2_EtherianPortalIdleLoop, projectile.Center).ToFloat();
                        }
                        if (activeSound != null)
                        {
                            activeSound.Position = projectile.Center;
                            activeSound.Volume = 9;
                        }


                        Color dustColor = default;
                        if (projectile.ai[0] == 0) dustColor = new Color(179, 255, 0);
                        else if (projectile.ai[0] == 1) dustColor = Main.rand.NextBool() ? new Color(0, 255, 255) : new Color(255, 0, 255);
                        else if (projectile.ai[0] == 2) dustColor = new Color(0, 255, 255);
                        else if (projectile.ai[0] == 3) dustColor = new Color(255, 60, 0);

                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType<Dusts.AbilityDust>(), newColor: dustColor)];
                        dust.noGravity = true;
                        dust.customData = player;

                        if (projectile.ai[1] >= 270)
                        {
                            int numDusts = 56;
                            int dustType = 75;
                            if (projectile.ai[0] == 2) dustType = 111;
                            else if (projectile.ai[0] == 3) dustType = 6;
                            for (int i = 0; i < numDusts; i++)
                            {
                                if (projectile.ai[0] == 1) dustType = Main.rand.NextBool() ? 135 : DustID.PinkFlame;
                                Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                                Vector2 velocity = position - player.Center;
                                Dust dust4 = Main.dust[Dust.NewDust(position + velocity, 0, 0, dustType, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f)];
                                dust4.noGravity = true;
                                dust4.noLight = true;
                                dust4.velocity = Vector2.Normalize(velocity) * 6f;
                                dust4.fadeIn = 1f;
                            }
                            Main.PlaySound(SoundID.DD2_DarkMageHealImpact, player.position);

                            modPlayer.aboveHeadTimer = modPlayer.aboveHeadDuration;
                            string hotkey;
                            if (projectile.ai[0] == 0)
                            {
                                modPlayer.canSandstormA = true;

                                var list = ElementsAwoken.sandstormA.GetAssignedKeys();
                                if (list.Count > 0) hotkey = list[0];
                                else hotkey = "<Key Unbound>";
                                modPlayer.aboveHeadText = "Hold " + hotkey + " to channel a sandstorm";
                            }
                            else if (projectile.ai[0] == 1)
                            {
                                modPlayer.canTimeA = true;

                                var list = ElementsAwoken.timeA.GetAssignedKeys();
                                if (list.Count > 0) hotkey = list[0];
                                else hotkey = "<Key Unbound>";
                                modPlayer.aboveHeadText = "Hold " + hotkey + " to fast forward time";
                            }
                            else if (projectile.ai[0] == 2)
                            {
                                modPlayer.canRainA = true;

                                var list = ElementsAwoken.rainA.GetAssignedKeys();
                                if (list.Count > 0) hotkey = list[0];
                                else hotkey = "<Key Unbound>";
                                modPlayer.aboveHeadText = "Hold " + hotkey + " to channel the rain";
                            }
                            else if (projectile.ai[0] == 3)
                            {
                                modPlayer.canWispA = true;

                                var list = ElementsAwoken.wispA.GetAssignedKeys();
                                if (list.Count > 0) hotkey = list[0];
                                else hotkey = "<Key Unbound>";
                                modPlayer.aboveHeadText = "Hold " + hotkey + " to transform into a wisp";
                            }
                            if (activeSound != null)
                            {
                                activeSound.Stop();
                            }
                            projectile.Kill();
                        }
                    }
                }
            }
            else
            {

            }
        }
    }
}