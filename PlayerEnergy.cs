using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;
using System.Linq;
using Terraria.ModLoader.IO;
using ReLogic.Graphics;
using Terraria.Graphics.Effects;
using ElementsAwoken.NPCs;

namespace ElementsAwoken
{
    public class PlayerEnergy : ModPlayer
    {

        public int energy = 0;
        public int batteryEnergy = 0; // updated by the battieres
        public int maxEnergy = 0; // updated by the battieres

        public bool soulConverter;
        public bool kineticConverter;
        public override void ResetEffects()
        {
            batteryEnergy = 0;

            soulConverter = false;
            kineticConverter = false;
        }

        public override void PostUpdateMiscEffects()
        {
            maxEnergy = batteryEnergy;
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
            if (energy < 0)
            {
                energy = 0;
            }
        }
        public override TagCompound Save()
        {
            return new TagCompound {
                {"energy", energy},
            };
        }
        public override void Load(TagCompound tag)
        {
            energy = tag.GetInt("energy");
        }
        public override void clientClone(ModPlayer clientClone)
        {
            MyPlayer clone = clientClone as MyPlayer;
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write(energy);
            packet.Send(toWho, fromWho);
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (kineticConverter && energy < maxEnergy)
            {
                int energyAmount = 0;
                if (proj.hostile)
                {
                    energyAmount += (int)(proj.damage * 0.15f);
                }
                int projSpeed = (int)((Math.Abs(proj.velocity.X) + Math.Abs(proj.velocity.Y)) * 0.5f);
                energyAmount += projSpeed;
                energy += energyAmount;
                CombatText.NewText(player.getRect(), Color.LightBlue, energyAmount, false, false);
            }
        }
    }
}