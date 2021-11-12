# Getting Started with Spectator Mode

The following steps are required create or add a spectator view to your project :
1. Create a co-location network experience.
2. Use a desktop client to render a virtual camera mapped to the Magic Leap's position.
3. Combine the virtual and physical camera streams using OBS.

This guide will cover step one. For step two and three, please read the [Streaming Guide](https://github.com/magicleap/spectator-mode/Documentation/StreamingGuide.md). A pre-built demo scene including Photon and NDI is located at Assets/_SpectatorMode/Scenes/PunSpectatorView. This scene can be built to both the Magic Leap headset and the standalone client.

<br/>

## Co-Located Networking Scene Requirements

**Magic Leap**
- Unity 2020.3.x
- Lumin SDK 0.25.0+
- Magic Leap Unity SDK / Package
- Photon
- com.magicleap.spectator.networkanchors

***Note:** If you need guidance on downloading the Magic Leap Unity SDK or configuring your Unity project to build to a Magic Leap headset, check out the ["Getting Started In Unity Guide"](https://developer.magicleap.com/en-us/learn/guides/unity-setup-intro).*

<br/>

## Create A Co-Location Network Experience
In this section we will use Photon and the included Network Anchors package to create a multi-user, co-located scene.

<br/>

####  Enable Photon Examples
1. Add [PUN 2](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922) from the asset store and import it into your project.
2. Define the your Photon credentials in using the PUN wizard or PUN Server settings. For help setting up your Photon app, checkout out Photon's [Setup Guide](https://doc.photonengine.com/zh-cn/pun/v2/demos-and-tutorials/pun-basics-tutorial/intro).
3. To enable the photon example scripts, navigate to the Player Settings  (Edit > Project Settings , then click Player).
4. Add PHOTON to the Scripting Define Symbols, located under the Other Settings section, then click Apply.

<br/>

#### Create a Simple Scene
1. From the menu select File > New Scene. Then select the Basic (Built-in) template.
2. Replace the existing main camera with the Magic Leap Camera Prefab located under `Packages/Magic Leap SDK/Tools/Prefabs`.
3. Save the new scene, name it NetworkAnchorsExample.

<br/>

#### Create the Photon Controller
1. Create an empty GameObject named PhotonController
2. Add the Simple Photon Room and Simple Photon Lobby components.
3. Set the Photon User Prefab to the SimplePhotonUser prefab located under `Assets/Network Anchors Examples/ PhotonExample/Resources/`

<br/>

#### Implementing Network Anchors
1. Create an empty GameObject called NetworkAnchor
2. Set the transform's position to `0, 0, 1` and the rotation to `0, 18, 0`.
3. Add the Network Anchor Service component to the object. 
4. Then add the Network Anchor Localizer component.
5. Use the Network Anchor's Localizer's OnAnchorPlaced event to visualize when the anchor has been created/localized:
    1. Create a cube as a child of the object
    2. Set the scale to `0.3, 0.3, 0.3` and disable it. 
    3. Select the NetworkAnchor
    4. Create a new OnAnchorPlaced event
    5. Set the event target as the Cube and the event as GameObject.SetActive, set it to true.
1. Add the MultiPlatformCoordinateProvider prefab from `Assets/NetworkAnchorsExamples/PhotonExample/Prefabs` into the scene.

<br/>

#### Connect the Service to Photon
1. Create an empty GameObject called PunNetworkAnchorController
2. Add the PhotonNetworkAnchorController component.
3. Set the fields with the objects in your scene.

<br/>

#### Call Create or Find Network Anchor
1. Select the Main Camera Prefab and add the Magic Leap Network Anchor Example component.
2. Set the Network Anchor Localizer field to target the Network Anchor.

<br/>

#### Build and Run
- Save your example scene and add it to the build order in Build Settings.
- In Project settings > Magic Leap > Manifest Settings, make sure the following privileges are enabled:
    - Autogranted
        - Internet
        - PCF Read
- In Project Settings > Player, set your project's project name and other identification information.
- In Project Settings > Player > Lumin > Publishing Settings, target your developer certificate.
- Deploy to device. 

To create or find a network anchor on the Magic Leap Headsets, press the bumper on the controller. On the desktop, select the game and press the spacebar.