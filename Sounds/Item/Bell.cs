﻿using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Sounds.Item
{
    public class Bell : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            return soundInstance;
        }
    }
}
