using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Encounters.Fog
{
    public class FogHandler : ModWorld
    {
		ScreenFog encounterFog = new ScreenFog(false);
		
        public override void PostDrawTiles()
        {
            encounterFog.Update(mod.GetTexture("Encounters/Fog/Fog"));
            encounterFog.Draw(mod.GetTexture("Encounters/Fog/Fog"), false, Color.White, true);
        }
    }
}