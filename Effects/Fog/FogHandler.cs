using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Effects.Fog
{
    public class FogHandler : ModWorld
    {
        ScreenFog encounterFog = new ScreenFog(false);
        ScreenFog sulphuricFog = new ScreenFog(false);

        public override void PostDrawTiles()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            encounterFog.Update(mod.GetTexture("Effects/Fog/Fog"), 1, ElementsAwoken.encounter == 2);
            encounterFog.Draw(mod.GetTexture("Effects/Fog/Fog"), 1, Color.White, true);

            sulphuricFog.Update(mod.GetTexture("Effects/Fog/FogYellow"), 1, MyWorld.plateauWeather == 1 && MyWorld.plateauWeatherTime > 0 && modPlayer.zonePlateau);
            sulphuricFog.Draw(mod.GetTexture("Effects/Fog/FogYellow"), 1, Color.White, true, 0.3f);
        }
    }
}