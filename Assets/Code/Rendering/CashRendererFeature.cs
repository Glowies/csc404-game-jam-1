using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CashRendererFeature : ScriptableRendererFeature
{
    public Texture2D TileTexture;
    public Vector2 TileSize;

    class CustomRenderPass : ScriptableRenderPass
    {
        private Material _material;
        private RenderTargetIdentifier _source;
        private RenderTargetHandle _tempTexture;

        public void Init(Material material)
        {
            _material = material;
            _tempTexture.Init("_TempCashTexture");
        }

        public void SetSource(RenderTargetIdentifier source)
        {
            _source = source;
        }

        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            RenderTextureDescriptor cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            cameraTextureDescriptor.depthBufferBits = 0;
            cmd.GetTemporaryRT(_tempTexture.id, cameraTextureDescriptor, FilterMode.Bilinear);
            ConfigureInput(ScriptableRenderPassInput.Normal);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBuffer = CommandBufferPool.Get("CashFeature");

            Blit(commandBuffer, _source, _tempTexture.Identifier(), _material, 0);
            Blit(commandBuffer, _tempTexture.Identifier(), _source);

            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(_tempTexture.id);
        }
    }

    CustomRenderPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        // Setup cash shader material
        var material = new Material(Shader.Find("Shader Graphs/CashFeatureShader"));
        material.SetTexture("_TileTexture", TileTexture);
        material.SetVector("_TileSize", TileSize);

        m_ScriptablePass = new CustomRenderPass();
        m_ScriptablePass.Init(material);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


