using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Sounds.Item
{
    public class WokeLaserCharge : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            // By creating a new instance, this ModSound allows for overlapping sounds. Non-ModSound behavior is to restart the sound, only permitting 1 instance.
            soundInstance = sound.CreateInstance();
            return soundInstance;
        }
    }
}
