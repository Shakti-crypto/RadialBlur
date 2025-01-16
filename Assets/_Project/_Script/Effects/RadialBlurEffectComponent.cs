using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


[Serializable, VolumeComponentMenuForRenderPipeline("Custom/Radial Blur", typeof(UniversalRenderPipeline))]

public class RadialBlurEffectComponent : VolumeComponent, IPostProcessComponent
{
    public FloatParameter BlurStrength = new FloatParameter(0);
    public FloatParameter BlurWidth = new FloatParameter(0);


    public bool IsActive()
    {
        return true;
    }

    public bool IsTileCompatible()
    {
        return true;
    }
}
