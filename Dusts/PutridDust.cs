using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Dusts
{
	public class PutridDust : ModDust
	{
        private int[] timer = new int[Main.maxDust];
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            timer[dust.dustIndex] = 0;
        }
        public override bool Update(Dust dust)
        {
            dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
            if (dust.customData is Vector2 position)
            {
                timer[dust.dustIndex]++;
                if (timer[dust.dustIndex] < 30) dust.velocity *= 0.95f;
                else
                {
                    Vector2 toTarget = new Vector2(position.X - dust.position.X, position.Y - dust.position.Y);
                    toTarget.Normalize();

                    dust.velocity += toTarget * 0.2f;
                    dust.scale = MathHelper.Clamp(Vector2.Distance(dust.position, position) / 80f, 0, 1.2f);
                }
                if (timer[dust.dustIndex] > 60 && Vector2.Distance(position, dust.position) < 16) dust.active = false;

                dust.position += dust.velocity;
            }
            return false;
        }
    }
}