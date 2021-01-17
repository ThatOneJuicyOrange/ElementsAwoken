using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Dusts
{
	public class WastelandDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
            dust.scale = 1.2f;
            dust.noGravity = true;
            dust.alpha = 30;
            dust.velocity /= 2f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;
            float lightScale = 0.2f;
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.6f * lightScale, 0.9f * lightScale, 0.45f * lightScale);
            dust.scale -= 0.03f;
            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
=> new Color(lightColor.R, lightColor.G, lightColor.B, 25);
    }
}