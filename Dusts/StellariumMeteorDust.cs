using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Dusts
{
	public class StellariumMeteorDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }
        public override bool Update(Dust dust)
        {
            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
    => new Color(255, 255, 255, 25);
    }
}