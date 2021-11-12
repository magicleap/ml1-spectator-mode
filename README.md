# Spectator Mode

With this demo project and our [network anchors](https://github.com/magicleap/com.magicleap.spectator.networkanchors/tree/main) package, you can create multi-user, co-located Unity apps where:
  - users share **spatially anchored virtual content** 
  - that virtual content is **streamed over NDI to OBS** and composited with the feed of **any external camera** so viewers can see exactly what the users are seeing **in real time**
  - Magic Leap headsets and iOS or Android phones share **cross-platform** virtual content through **image tracking localization**
  - any or all of the above can be used, features are all **independent** of each other

This readme includes the instructions on setting up the demo as well as the technical information required to create custom streaming solutions. Please report any questions or issues using Github’s issue ticketing system.  

<br/>

## Table of Contents
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Integrating Spectator Mode into an Existing Project](#integrating-spectator-mode-into-an-existing-project)
- [Included Packages](#included-packages)

<br/>

## Requirements  
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

**Mobile -- iOS and Android (Image Target Localization)**
- Unity 2020.2 +
- ARFoundation 4.1.7 +
- Photon
- MagicLeap.Spectator.NetworkAnchors

<br/>

## Getting Started

Clone this repository and be sure to include the following flag to also get the corresponding submodules:
  ```
  git clone --recurse-submodules <Spectator-Mode Repo>
  ```

Demo scenes, scripts, and prefabs are currently split between the Assets/_SpectatorMode directory and the Nework Anchors Samples directory found by going to Package Manager > Packages: In Project > Custom > Network Anchors > Samples and selecting "Import".

Check out the [Documentation](https://github.com/magicleap/spectator-mode/tree/main/Documentation) directory for detailed walkthrough guides on enabling and integrating Spectator Mode features:
- [Basic Setup and Getting Started with Photon](https://github.com/magicleap/spectator-mode/Documentation/GettingStarted.md)
    -  Project set up and basic scene using Photon Networking to create a co-located Unity experience.
- [Streaming with NDI and OBS](https://github.com/magicleap/spectator-mode/Documentation/StreamingGuide.md)
    - Creating necessary NDI streaming compnents in unity scene and combining the camera streams using OBS.
- [Running The Demo Spectator Mode Scene](https://github.com/magicleap/spectator-mode/Documentation/SpectatorModeDemoScene.md)
    - Steps to run our pre-built Photon and NDI scene on Magic Leap and Standalone.
- [Image Tracking Mobile Localization](https://github.com/magicleap/spectator-mode/Documentation/MobileLocalization.md)
    - Sharing co-located content between the Magic Leap and a mobile phone (iOS or Android) by scanning an image target to create a cross-platform network anchor.

<br/>

## Integrating Spectator Mode into an Existing Project

1. Add the following packages to your own project:
    - Packages/jp.keijiro.klak.ndi@2.0.2
    - Add the following registry to your projects manifest.json file and download "System.Memory" from the Package Manager:
      ```
      "scopedRegistries": [
          {
            "name": "Unity NuGet",
            "url": "https://unitynuget-registry.azurewebsites.net",
            "scopes": [
              "org.nuget"
            ]
          }
      ]
      ```
    - Follow the [magicleap.spectator.networkanchors](https://github.com/magicleap/com.magicleap.spectator.networkanchors/tree/main) "Install Guide" to add the Network Anchors package. You can also download the Examples by going to Package Manager > Packages: In Project > Custom > Network Anchors > Samples and selecting "Import".
2. Follow the [Walkthrough Guides](https://github.com/magicleap/spectator-mode/tree/main/Documentation) that correspond to the features you wish to integrate (multi-user connection with Photon, co-located virtual content, NDI streaming, or image target localization).

<br/>

## Included Packages
[com.magicleap.spectator.networkanchors](https://github.com/magicleap/com.magicleap.spectator.networkanchors)
A lightweight tool that enables co-location experiences using a shared anchor.

[jp.keijiro.klak.ndi@2.0.2](https://github.com/keijiro/KlakNDI)
Modified version of keijiro’s Klak NDI package. Modified to exclude Klak libraries from Magic Leap builds. Itself has a dependency on the System.Memory package, also included.
