
# Running the Demo Scene

This guide provides the steps to setup and run the example project. The demo scene is located at **Assets/_SpectatorMode/Scenes/PunSpectatorView**.

## Photon Setup

1. Open the photon server settings by selecting **Window > PUN Wizard**
2. Select **Setup Project** from the **Pun Wizard** window.
3. Configure your project to use your Photon App Id or an on premise server.

## Image Target Setup

1. To localize with Mobile devices, you will need to print out the [Image Target](./submarine.png). 
2. For the target to track correctly, the image size needs to be adjusted. The default is based on the standard size when printed on US printer paper (0.288 x 0.2036 meters). To adjust the size:
    1. Select the **ARFReferenceImageLibrary** asset under `Assets/Samples/NetworkAnchors/1.1.0/Examples/ImageTracking/ImageTarget`.
    1. Change the **Physical Size (meters)** in the inspector to your prefered size.
    1. Open the **PunSpecatorView** scene located under `_SpecatorMode/Scenes/`
    1. Search for the **MagicLeapCoordinateProvider** object in the Hierarchy and select it. Expand the **Target Info** section in the inspector and set the **Longer Dimension** (meters) value.

## Build and Run

### Magic Leap
1. Open the **Build Settings** Window
2. If you are not targeting Lumin already, select **Lumin** from the **Platform** list, then **Switch Platform** 
2. Make sure that at least the **PunSpectatorView** and **MagicLeap_Spectator_Rig** scenes are listed and enabled. 
3. Press **Build**

### Desktop
1. Make sure your player settings are set to: 
    * Windows: x64 - D3D11/D3D12
    * macOS: x64 or arm64 (M1), Metal
    * Linux: x64, Vulkan
1. Open the **Build Settings** Window
2. If you are not targeting Lumin already, select **PC, Mac & Standalone** from the **Platform** list, then **Switch Platform** 
2. Make sure that at least the **PunSpectatorView** and **MagicLeap_Spectator_Rig** scenes are listed and enabled. 
3. Press **Build**

### iOS
1. This project uses KlakNDI to stream, iOS requires the  NDI Advanced SDK to build on Xcode. Please download and install the [NDI Advanced SDK](https://www.ndi.tv/sdk/#download) for iOS in advance of building.
2. Ensure you've downloaded the ARKit Package and selected it under Player Settings > XR Plug-in Management.
3. Make sure your player settings are set to: 
    * iOS: arm64, Metal
    * iOS > Other Settings > Camera Usage Description: "Camera Needed for AR"
    * iOS > Other Settings > Scripting Define Symbols: Add "AR_FOUNDATIONS"
4. Open the **Build Settings** Window
5. If you are not targeting Lumin already, select **iOS** from the **Platform** list, then **Switch Platform** 
6. Make sure that at least the **PunSpectatorView** and **ARFoundation_Spectator_Rig** scenes are listed and enabled. 
7. Press **Build**

### Android
**Note: Android does not support streaming through NDI**
1. Ensure you've downloaded the ARCore Package and selected it under Player Settings > XR Plug-in Management.
2. Open the **Build Settings** Window.
3. If you are not targeting Lumin already, select **Android** from the **Platform** list, then **Switch Platform** .
4. Make sure that at least the **PunSpectatorView** and **ARFoundation_Spectator_Rig**scenes are listed and enabled. 
5. Press **Build**


## Occlusion Manager

The ARFoundation_Spectator_Rig scene contains an occlusion manager under ---AR Foundations--- > ARSessionOrigin > ARCamera > AR Occlusion Manager. By default this will hide the scene content if it is behind/blocked by physical objects. If you can't see the UI panel in the scenes, toggle "Occlusion" off in-app -- the panel might just be hiding behind an object.

## Using OBS
To learn how to retrieve the streamed image, view our [Streaming Guide](./StreamingGuide.md)
