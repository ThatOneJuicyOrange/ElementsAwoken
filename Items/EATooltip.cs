﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;

namespace ElementsAwoken.Items
{
    public class EATooltip : GlobalItem
    {
        public bool donator = false;
        public bool artifact = false;
        public bool developer = false;
        public bool youtuber = false;
        public bool flyingBoots = false;
        public bool tech = false;
        public bool unobtainable = false;
        public bool testing = false;
        public EATooltip()
        {
            donator = false;
            artifact = false;
            developer = false;
            youtuber = false;
            flyingBoots = false;
            tech = false;
            unobtainable = false;
            testing = false;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            EATooltip myClone = (EATooltip)base.Clone(item, itemClone);
            myClone.donator = donator;
            myClone.artifact = artifact;
            myClone.developer = developer;
            myClone.youtuber = youtuber;
            myClone.flyingBoots = flyingBoots;
            myClone.tech = tech;
            myClone.unobtainable = unobtainable;
            myClone.testing = testing;
            return myClone;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            EATooltip modItem = item.GetGlobalItem<EATooltip>();
            EARarity eaRarity = item.GetGlobalItem<EARarity>();
            if (!item.expert)
            {
                if (modItem.donator)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Donator Item-");
                    tip.overrideColor = new Color(118, 108, 247);
                    tooltips.Insert(1, tip);
                }
                if (modItem.artifact)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Artifact Item-");
                    tip.overrideColor = new Color(255, 154, 30);
                    tooltips.Insert(1, tip);
                }
                if (modItem.developer)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Developer Item-");
                    tip.overrideColor = new Color(214, 32, 177);
                    tooltips.Insert(1, tip);
                }
                if (modItem.youtuber)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Youtuber Item-");
                    tip.overrideColor = new Color(3, 160, 92);
                    tooltips.Insert(1, tip);
                }
                if (modItem.flyingBoots)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "Allows wings to be attached onto the boots");
                    tip.overrideColor = new Color(89, 255, 33);
                    tooltips.Insert(1, tip);
                }
                if (modItem.tech)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Tech-");
                    tip.overrideColor = new Color(122, 230, 217);
                    tooltips.Insert(1, tip);
                }
                if (modItem.unobtainable)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Unobtainable-");
                    tip.overrideColor = Color.Red;
                    tooltips.Insert(1, tip);
                }
                if (modItem.testing)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "-Testing-");
                    tip.overrideColor = Color.HotPink;
                    tooltips.Insert(1, tip);
                }
                if (eaRarity.awakened)
                {
                    TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "Awakened");
                    tooltips.Add(tip);
                }
            }
            if (ModContent.GetInstance<Config>().debugMode)
            {
                string tipString = "";
                if (item.useTime > 0) tipString += "Use Time: " + item.useTime + "\n";
                if (item.useAnimation > 0) tipString += "Use Animation: " + item.useAnimation + "\n";
                if (item.useStyle > 0) tipString += "Use Style: " + item.useStyle + "\n";
                if (item.damage > 0) tipString += "Base Damage: " + item.damage + "\n";
                if (item.knockBack > 0) tipString += "Base Knockback: " + item.knockBack + "\n";
                TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", tipString);
                tip.overrideColor = Color.AliceBlue;
                tooltips.Add(tip);
            }   
        }   
    }
}