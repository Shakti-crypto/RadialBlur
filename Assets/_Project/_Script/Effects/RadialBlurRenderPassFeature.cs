using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RadialBlurRenderPassFeature : ScriptableRendererFeature
{
    class RadialBlurRenderPass : ScriptableRenderPass
    {
        
        private Material m_radialMaterial;
        private RenderTargetIdentifier currentRenderTarget;
        private RadialBlurEffectComponent m_radialBlurEffect;

        public void Setup(Material radialMaterial, RenderTargetIdentifier renderTargetIdentifier)
        {
            m_radialMaterial = radialMaterial;
            currentRenderTarget = renderTargetIdentifier;
            
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {

            if (m_radialMaterial == null)
            {
                Debug.LogWarning("Shader not set for render feature.");
                return;
            }


            CommandBuffer cmd = CommandBufferPool.Get();

            SetRadialMaterialPropertiesFromPostProcessingVolume();

            if (m_radialBlurEffect == null)
            {
                Debug.LogWarning("Cannot find radial blur effect.");
                return;
            }

            int tempTextureID = Shader.PropertyToID("_MainTex");
            RenderTextureDescriptor renderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            renderTextureDescriptor.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempTextureID, renderTextureDescriptor);
            Blit(cmd, currentRenderTarget, tempTextureID, m_radialMaterial);
            Blit(cmd, tempTextureID, currentRenderTarget);

            cmd.ReleaseTemporaryRT(Shader.PropertyToID("_MainTex"));
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);


        }


        private void SetRadialMaterialPropertiesFromPostProcessingVolume()
        {
            VolumeStack volumeStack = VolumeManager.instance.stack;
            m_radialBlurEffect = volumeStack.GetComponent<RadialBlurEffectComponent>();
            if (m_radialBlurEffect == null)
            {
                Debug.LogWarning("Radial Blur not found in global volume");
                return;
            }
            m_radialMaterial.SetFloat("_BlurStrength", m_radialBlurEffect.BlurStrength.value);
            m_radialMaterial.SetFloat("_BlurWidth", m_radialBlurEffect.BlurWidth.value);
        }

    }

    [SerializeField] private Shader radialShader;
    [SerializeField] private RenderPassEvent renderPassEvent=RenderPassEvent.AfterRendering;
    RadialBlurRenderPass m_ScriptablePass;
    Material m_radialMaterial;

    public override void Create()
    {
        m_radialMaterial = CoreUtils.CreateEngineMaterial(radialShader);
        m_ScriptablePass= new RadialBlurRenderPass();
        m_ScriptablePass.renderPassEvent = renderPassEvent;
    }


    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType == CameraType.Game)
        {
            m_ScriptablePass.Setup(m_radialMaterial, renderer.cameraColorTarget);
            renderer.EnqueuePass(m_ScriptablePass);
        }
    }


    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(m_radialMaterial);
    }
}


