using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Dusts
{
	public class AbilityDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }
        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            {
                Lighting.AddLight(dust.position, 0.3f * (dust.color.R / 255), 0.3f * (dust.color.G / 255), 0.3f * (dust.color.B / 255));
            }
            dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
            if (dust.customData is Player player)
            {
                Vector2 toTarget = new Vector2(player.Center.X - dust.position.X, player.Center.Y - dust.position.Y);
                toTarget.Normalize();
                float speed = MathHelper.Clamp(player.velocity.Length() * 1.2f,6,9999);
                dust.velocity = toTarget * speed;
                dust.scale = MathHelper.Lerp(0, 1.5f, MathHelper.Clamp(Vector2.Distance(player.Center, dust.position) / 100,0,1));
            }
            if (dust.scale <= 0.05) dust.active = false;
            dust.position += dust.velocity;
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
    => new Color(lightColor.R, lightColor.G, lightColor.B, 25);
    }
}