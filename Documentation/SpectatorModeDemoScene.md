# Spectator Mode Demo Scene

This demo allows you to take an external camera, place it above or below the Magic Leap headset, and have the camera feed and Magic Leap virtual content composited together in OBS so anyone watching the stream can see what the users of the co-located experience are seeing.

This walkthrough is just for getting the inlcuded demo scene running on Magic Leap and Standalone. For information on how to create a custom scene, reference the guides on our [co-located networking](https://github.com/magicleap/spectator-mode/Documentation/GettingStarted.md) and NDI stream[](https://github.com/magicleap/spectator-mode/Documentation/StreamingGuide.md) features.

The demo scene is located at **Assets/_SpectatorMode/Scenes/PunSpectatorView**.

<br/>

## Demo Scene Requirements
**Desktop & Magic Leap**
- Unity 2020.3.x
- Photon
- com.magicleap.spectator.networkanchors

**Magic Leap** 
- Lumin SDK 0.25.0+
- Magic Leap Unity SDK / Package

**Standalone Client  (NDI Streaming)**
- Windows: D3D11 or D3D12 
- macOS: Metal required
- Linux: Vulkan required
- KlakNDI and System.Memory
- Camera
***Note:** Any external camera that can be used in OBS can be used as the composite feed. Anything from a simple webcam to a fancy camera rig will work.*

<br/>

## Configuration

If your Unity editor is not already configured for Magic Leap projects, check out the ["Getting Started In Unity Guide"](https://developer.magicleap.com/en-us/learn/guides/unity-setup-intro) for help downloading the SDK and adjusting your project settings.

For the other Spectator Mode project requirements, follow the download guide on the main [README](https://github.com/magicleap/spectator-mode).

<br/>

## Running on Magic Leap and Standalone

***Note:** This guide does not yet include instructions on how to mount an external camera above or below the Magic Leap headset. For demo purposes, we're using a static camera on the standalone client to test the OBS stream.*

1. Build the PunSpectatorView scene to the Magic Leap device(s), following the standard Lumin build settings.
2. You can now switch to Standalone within Unity and connect to the headset client by:
    1. Running the built scene in the ML device.
    2. Playing the scene in Unity -- the console should show you connecting to the Photon room and 2 players being inside it.
3. Pressing the bumper on the ML Controller will start the localization process -- the cube in the scene should turn into a sphere, indicating the clients have localized.
4. Pressing the "1" key will switch which camera view is being streamed to OBS -- to set up OBS, head to the "Combine Camera Streams Using OBS" section of the [Streaming Guide](https://github.com/magicleap/spectator-mode/Documentation/StreamingGuide.md).

