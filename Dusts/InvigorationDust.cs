using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Dusts
{
	public class InvigorationDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            ElementsAwoken.dustTimer[dust.dustIndex] = 0;
        }
        public override bool Update(Dust dust)
        {
            if (!dust.noLight) Lighting.AddLight(dust.position, 0.15f, 0.15f, 0.3f);
            if (Main.tile[(int)dust.position.X / 16, (int)dust.position.Y / 16].active()) dust.active = false;
            if (!dust.noGravity) dust.velocity.Y += 0.05f;
            ElementsAwoken.dustTimer[dust.dustIndex]++;
            if (ElementsAwoken.dustTimer[dust.dustIndex] < 15)
            {
                dust.scale += 0.02f;
            }
            else
            {
                dust.scale -= 0.02f;
                if (dust.scale < 0.1f) dust.active = false;
            }
            float curve = 0.02f;
            dust.velocity += dust.dustIndex % 2 == 0 ? dust.dustIndex % 3 == 0 ? new Vector2(0, curve) : new Vector2(0, -curve) : dust.dustIndex % 3 == 0 ? new Vector2(curve, 0) : new Vector2(-curve, 0);
            dust.position += dust.velocity;
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
    => new Color(255, 255, 255, 25);
    }
}