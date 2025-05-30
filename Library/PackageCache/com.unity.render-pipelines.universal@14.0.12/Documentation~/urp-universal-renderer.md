# Universal Renderer

This page describes the URP Universal Renderer settings.

For more information on rendering in URP, refer to [Rendering in the Universal Render Pipeline](rendering-in-universalrp.md).

## Rendering Paths

The URP Universal Renderer implements three Rendering Paths:

* Forward Rendering Path.

* [Forward+ Rendering Path](rendering/forward-plus-rendering-path.md).

* [Deferred Rendering Path](rendering/deferred-rendering-path.md).

### Rendering Path comparison

Each rendering path uses a different set of steps to calculate lighting and draw objects. Which rendering path you choose affects the performance of your game and lighting options.

- Forward rendering path: URP draws each object one by one. For each object, URP checks every light that affects it to calculate how the object looks. 
- Forward+ rendering path: Works similarly to the Forward rendering path, but lets you use many more lights without affecting performance. 
- Deferred rendering path: URP first renders information about every object into multiple buffers. Then in a later ('deferred') step, URP draws each screen pixel one by one by combining the information from the buffers.

The following table shows the differences between the Forward and the Deferred Rendering Paths in URP.

| Feature | Forward | Forward+ | Deferred |
|---------|---------|----------|----------|
| Maximum number of real-time lights per object. | 9 | Unlimited. [The per-Camera limit applies](rendering/forward-plus-rendering-path.md#reduce-build-time). | Unlimited |
| Per-pixel normal encoding | No encoding (accurate normal values). | No encoding (accurate normal values). | Two options:<ul><li>Quantization of normals in G-buffer (loss of accuracy, better performance).</li><li>Octahedron encoding (accurate normals, might have significant performance impact on mobile GPUs).</li></ul>For more information, refer to the section [Encoding of normals in G-buffer](rendering/deferred-rendering-path.md#accurate-g-buffer-normals). |
| MSAA | Yes | Yes | No |
| Vertex lighting | Yes | No | No |
| Camera stacking | Yes | Yes | Supported with a limitation: Unity renders only the base Camera using the Deferred Rendering Path. Unity renders all overlay Cameras using the Forward Rendering Path. |

## How to find the Universal Renderer asset

To find the Universal Renderer asset that a URP asset is using:

1. Select a URP asset.

2. In the Renderer List section, click a renderer item or the vertical ellipsis icon (&vellip;) next to a renderer.

    ![How to find the Universal Renderer asset](Images/urp-assets/find-renderer.png)

## Universal Renderer asset reference

This section describes the properties of the Universal Renderer asset.

### Filtering

This section contains properties that define which layers the renderer draws.

| Property | Description |
|:-|:-|
| **Opaque Layer Mask** | Select which opaque layers this Renderer draws |
| **Transparent Layer Mask** | Select which transparent layers this Renderer draws |

### Rendering

This section contains properties related to rendering.

| Property | Description |
|:-|:-|
| **Rendering&#160;Path** | Select the Rendering Path.<br/>Options:<ul><li>**Forward**: The Forward Rendering Path.</li><li>**Forward+**: The [Forward+ Rendering Path](rendering/forward-plus-rendering-path.md).</li><li>**Deferred**: The [Deferred Rendering Path](rendering/deferred-rendering-path.md).</li></ul> |
| &#160;&#160;**Depth&#160;Priming&#160;Mode** | Specify whether Unity uses scene depth data to identify pixels the camera can't see, then skips the shader fragment stage for those pixels. This speeds up rendering, but has an upfront memory and performance cost. The amount rendering speeds up depends on how many pixels are hidden, and the complexity of the fragment shader code Unity skips.<br/><br/>The options are:<ul><li>**Disabled**: Unity doesn't perform depth priming.</li><li>**Auto**: Unity performs depth priming only if it's already performed a depth prepass. A depth prepass renders scene depth data early in the render pipeline. This option is not supported on Android, iOS, and Apple TV.</li><li>**Forced**: Unity always performs a depth prepass and depth priming.</li></ul><br/>Depth priming isn't compatible with the following:<ul><li>Platforms that use tile-based rendering.</li><li>The Deferred rendering path.</li><li>Multisample anti-aliasing (MSAA).</li></ul> |
| &#160;&#160;**Accurate G-buffer normals** | Indicates whether to use a more resource-intensive normal encoding/decoding method to improve visual quality.<br /><br />This property is available only if **Rendering Path** is set to **Deferred**. |
| **Depth&#160;Texture&#160;Mode** | Specifies at which stage in the render pipeline URP should copy the scene depth to a depth texture. The options are:<ul><li>**After Opaques**: URP copies the scene depth after the opaques render pass.</li><li>**After Transparents**: URP copies the scene depth after the transparents render pass.</li><li>**Force Prepass**: URP does a depth prepass to generate the scene depth texture.</li></ul>**Note**: On mobile devices, the **After Transparents** option can lead to a significant improvement in memory bandwidth. This is because the Copy Depth pass causes a switch in render target between the Opaque pass and the Transparents pass. When this occurs, Unity stores the contents of the Color Buffer in the main memory, and then loads it again once the Copy Depth pass is complete. The impact increases significantly when MSAA is enabled, because Unity must also store and load the MSAA data alongside the Color Buffer. |

### Native RenderPass

This section contains properties related to URP's Native RenderPass API.

| Property | Description |
|:-|:-|
| **Native RenderPass** | Indicates whether to use URP's Native RenderPass API. When enabled, URP uses this API to structure render passes. As a result, you can use [programmable blending](https://docs.unity3d.com/Manual/SL-PlatformDifferences.html#using-shader-framebuffer-fetch) in custom URP shaders. Enable Native RenderPass if you use Vulkan or Metal graphics APIs, so URP automatically reduces how often it copies render textures into and out of memory. For more information about the RenderPass API, refer to [ScriptableRenderContext.BeginRenderPass](https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.BeginRenderPass.html).<br/><br/>**Note**: Enabling this property has no effect on OpenGL ES. |

### Shadows

This section contains properties related to rendering shadows.

| Property | Description |
|:-|:-|
| **Transparent Receive Shadows** | When this option is on, Unity draws shadows on transparent objects. |

### Overrides

This section contains Render Pipeline properties that this Renderer overrides.

#### Stencil

With this check box selected, the Renderer processes the Stencil buffer values.

![URP Universal Renderer Stencil override](Images/urp-assets/urp-universal-renderer-stencil-on.png)

For more information on how Unity works with the Stencil buffer, refer to [ShaderLab: Stencil](https://docs.unity3d.com/Manual/SL-Stencil.html).

In URP, you can use bits 0 to 3 of the stencil buffer for custom rendering effects. This means you can use stencil indices 0 to 15.

### Compatibility

This section contains settings related to backwards compatibility.

| Property | Description |
|:-|:-|
| **Intermediate Texture** | This property lets you force URP to renders via an intermediate texture.<br/>Options: <ul><li>**Auto**: URP uses the information provided by the `ScriptableRenderPass.ConfigureInput` method to determine automatically whether rendering via an intermediate texture is necessary.</li><li>**Always**: forces rendering via an intermediate texture. Use this option only for compatibility with Renderer Features that do not declare their inputs with `ScriptableRenderPass.ConfigureInput`. Using this option might have a significant performance impact on some platforms.</li></ul> |

### Renderer Features

This section contains the list of Renderer Features assigned to the selected Renderer.

For information on how to add a Renderer Feature, refer to [How to add a Renderer Feature to a Renderer](urp-renderer-feature-how-to-add.md).

URP contains the pre-built Renderer Feature called [Render Objects](renderer-features/renderer-feature-render-objects.md).
