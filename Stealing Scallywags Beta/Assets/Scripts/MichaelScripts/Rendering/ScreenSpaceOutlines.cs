//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

//public class ScreenSpaceOutlines : ScriptableRendererFeature
//{
//    //using https://www.youtube.com/watch?v=LMqio9NsqmM

//    [System.Serializable]
//    private class ViewSpaceNormalsTextureSettings
//    {
//        public int shaderTagType;
//    }

//    private class ViewSpaceNormalsTexturePass : ScriptableRenderPass 
//    {
//        private ViewSpaceNormalsTextureSettings normalsTextureSettings;
//        private readonly ShaderTagId[] shaderTagIdList;
//        private readonly RenderTargetHandle normals;
//        private readonly Material normalsMaterial;
//        private FilteringSettings filteringSettings;

//        public ViewSpaceNormalsTexturePass (RenderPassEvent renderPassEvent, LayerMask outlinesLayerMask, ViewSpaceNormalsTextureSettings settings) 
//        {
//            normalsMaterial = new Material(Shader.Find("Hidden/ViewSpaceNormalsShader"));

//            shaderTagIdList = new ShaderTagId[]
//            {
//                new ShaderTagId ("UniversalForward"),
//                new ShaderTagId ("UniversalForwardOnly"),
//                new ShaderTagId ("LightweightForward"),
//                new ShaderTagId ("SRPDefualtUnlit"),
//            };
//            this.renderPassEvent = renderPassEvent;
//            normals.Init("_SceneViewSpaceNormals");

//            filteringSettings = new FilteringSettings(RenderQueueRange.opaque, outlinesLayerMask);
//        }

//        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor) 
//        {
//            RenderTextureDescriptor normalsTextureDescripter = cameraTextureDescriptor;
//            normalsTextureDescripter.colorFormat = normalsTextureSettings.colorFormat;
//            normalsTextureDescripter.depthBufferBits = normalsTextureSettings.depthBufferBits;
//            cmd.GetTemporaryRT(normals.id, cameraTextureDescriptor, FilterMode.Point);
//            ConfigureTarget(normals.Identifier());
//            ConfigureClear(ClearFlag.All, normalsTextureSettings.backgroundColor);
//        }

//        public override void Execute (ScriptableRenderContext context, ref RenderingData renderingData) 
//        {

//            if (!normalsMaterial)
//                return;
//            CommandBuffer cmd = CommandBufferPool.Get();
//            using (new ProfilingScope())
//            {
//                context.ExecuteCommandBuffer(cmd);
//                cmd.Clear();
//                DrawingSettings drawSettings = CreateDrawingSettings(shaderTagIdList[normalsTextureSettings.shaderTagType], ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
//                drawSettings.overrideMaterial = normalsMaterial;
//                context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);
//            }
//            context.ExecuteCommandBuffer(cmd);
//            CommandBufferPool.Release(cmd);
//        }

//        public override void OnCameraCleanup (CommandBuffer cmd) 
//        {
//            cmd.ReleaseTemporaryRT(normals.id);
//        }
//    }

//    private class ScreenSpaceOutlinePass : ScriptableRenderPass 
//    {
//        private readonly Material screenSpaceOutlineMaterial;
//        private RenderTargetIdentifier cameraColorTarget;
//        private RenderTargetIdentifier temporaryBuffer;
//        private int temporaryBufferID = Shader.PropertyToID("_TemoraryBuffer");

//        public ScreenSpaceOutlinePass(RenderPassEvent renderPassEvent) 
//        {
//            this.renderPassEvent = renderPassEvent;
//            screenSpaceOutlineMaterial = new Material(Shader.Find("Hidden/OutlineShader"));
//        }

//        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
//        {
            
//        }
//        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
//        {
//            if (!screenSpaceOutlineMaterial)
//                return;
//            CommandBuffer cmd = CommandBufferPool.Get();
//            using (new ProfilingScope (cmd, new ProfilingSampler ("ScreenSpaceOutlines")))
//            {
//                Blit(cmd, cameraColorTarget, cameraColorTarget, screenSpaceOutlineMaterial);
//                Blit(cmd, temporaryBuffer, cameraColorTarget, screenSpaceOutlineMaterial);
//            }
//            context.ExecuteCommandBuffer (cmd);
//            CommandBufferPool.Release (cmd);
//        }

//        public override void OnCameraCleanup(CommandBuffer cmd)
//        {
            
//        }

//    }

//    [SerializeField] private RenderPassEvent renderPassEvent;

//    private ViewSpaceNormalsTexturePass viewSpaceNormalsTexturePass;
//    private ScreenSpaceOutlinePass screenSpaceOutlinePass;


//    public override void Create()
//    {
//        viewSpaceNormalsTexturePass = new ViewSpaceNormalsTexturePass(renderPassEvent);
//        screenSpaceOutlinePass = new ScreenSpaceOutlinePass(renderPassEvent);
//    }

//    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
//    {
//        renderer.EnqueuePass(viewSpaceNormalsTexturePass);
//        renderer.EnqueuePass(screenSpaceOutlinePass);
//    }

//    [SerializeField] private ViewSpaceNormalsTextureSettings viewSpaceNormalsTextureSettings;

//    [SerializeField] private LayerMask outlinesLayerMask;
//}


