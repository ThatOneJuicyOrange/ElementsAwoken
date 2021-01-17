using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Dusts
{
	public class PlateauDustTileCollide : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            ElementsAwoken.dustTimer[dust.dustIndex] = 0;
        }
        public override bool Update(Dust dust)
        {
            if (Framing.GetTileSafely((int)dust.position.X / 16, (int)dust.position.Y / 16).active()) dust.active = false;
            if (!dust.noGravity) dust.velocity.Y += 0.05f;
            if (MyWorld.plateauWeather == 2) dust.velocity.Y -= 0.2f;
            ElementsAwoken.dustTimer[dust.dustIndex]++;
            if (ElementsAwoken.dustTimer[dust.dustIndex] < 120)
            {
                dust.scale += 0.01f;
            }
            else
            {
                dust.scale -= 0.01f;
                if (dust.scale < 0.1f) dust.active = false;
            }
            dust.position += dust.velocity;
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
    => new Color(255, 255, 255, 25);
    }
}