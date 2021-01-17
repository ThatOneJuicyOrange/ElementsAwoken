using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Effects.Fog
{
    public class SulphuricFogBgStyle : ModSurfaceBgStyle
    {
		ScreenFog fog = new ScreenFog(true);
        public override bool ChooseBgStyle()
        {
            if (Main.gameMenu) return false;
            MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            return MyWorld.plateauWeather == 1 && MyWorld.plateauWeatherTime > 0 && modPlayer.zonePlateau;
        }
        
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
		
		public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            fog.Update(mod.GetTexture("Effects/Fog/FogYellow"), -1, MyWorld.plateauWeather == 1 && MyWorld.plateauWeatherTime > 0 && modPlayer.zonePlateau);
            fog.Draw(mod.GetTexture("Effects/Fog/FogYellow"), -1, new Color(200, 200, 120));
			return true;
		}
    }
}
