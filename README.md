# Spectator Mode

This project demonstrates how to render virtual content using an external camera and a Magic Leap's headset position. In this demo, we use the [magicleap.spectator.networkanchors](https://github.com/magicleap/com.magicleap.spectator.networkanchors/tree/main) package to create a multi-user colocation experience. By using this project you will be able to take an external camera, place it above or below the Magic Leap headset, and have the camera feed and Magic Leap virtual content composited together in OBS so anyone watching the stream can see what the users of the co-located experience are seeing.

This readme includes the instructions on setting up the demo as well as the technical information required to create custom streaming solutions. Please report any questions or issues using Github’s issue ticketing system.  

**Note**:  
This project uses submodules. Use `git clone --recurse-submodules` when cloning.

This project includes modified versions of the `jp.keijiro.klak.spout` and `jp.keijiro.klak.ndi` packages. These packages were modified to include assembly dentitions that prevent the NDI and Spout packages from compiling when targeting Magic Leap. 

##### If you are integrating Spectator mode into an existing project:
Copy the following directories into your own project:
* Packages/jp.keijiro.klak.ndi@1.0.12
* Packages/jp.keijiro.klak.spout@1.0.1

Add the following registry to your projects manifest.json file:
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
## Table of Contents
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Troubleshooting](#troubleshooting)
- [Included Packages](#included-packages)

## Requirements  
**Desktop & Magic Leap**
- Unity 2020.3.x
- Photon
- com.magicleap.spectator.networkanchors

**Magic Leap** 
- Lumin SDK 0.25.0+

**Standalone Client  (NDI Streaming)**
- Windows: D3D11 or D3D12 
- macOS: Metal required
- Linux: Vulkan required

**Standalone Client (Spout Streaming)**
*Offers better video streaming latency.*
- Windows: D3D11 or D3D12 
- URP / HDRP

## Getting Started
With a headset and an external camera hooked up to your computer, you can follow the following steps to create or add a spectator view to your project. This guide will use the sample scene under _SpectatorMode/Scenes/PunSpectatorView, which can be both be built to the headset and run on standalone. Together we will:

1) Create a co-location network experience.
2) Use a desktop client to render a virtual camera mapped to the Magic Leap's position.
3) Combine the virtual and physical camera streams using OBS.

*Prerequisites:* 
* Magic Leap Unity SDK / Package -- If you need assistance configuring your Unity project to build to a Magic Leap headset, check out the [Getting Started In Unity Guide](https://developer.magicleap.com/en-us/learn/guides/unity-setup-intro). The versions are outdated, but the core steps are the same.

### Create A Co-Location Network Experience
The PunSpectatorView scene includes a pre-configured co-location experience. To learn how to create your own using our cross-platform network anchor package, view the com.magicleap.spectator.networkanchors [README](https://github.com/magicleap/com.magicleap.spectator.networkanchors/tree/main).

1. In addition to the other standard setup steps for Lumin, make sure you change the default certificate location to your personal magic leap certificate folder -- under Project Settings > Player > Lumin > Publishing Settings > ML Certificate.
2.  Build the PunSpectatorView scene to your Magic Leap device. If the first build fails due to an error with the Spout plugin, run the build again and it should work. This is a bug we will address in a future release.

### Create the Desktop Spectator Client
We will use the Desktop client to render the Virtual Content. To match the virtual content's position with the external camera, we will create a virtual camera that can follow the network position of the Magic Leap. 

1. Switch your project to Standalone and make sure the Graphics API under Project Settings > Player > Standalone is the one specified by your O.S. listed in the [Requirements](requirements) section.
2. The sample scene already has the NDI Components needed to stream to OBS, so you can skip the "Create the NDI Rig" section. If you are building your own scene from scratch, follow that section.
3. You can now connect the scene in standalone Unity and the headset client by:
    1. Running the scene in the ML device.
    2. Playing the scene in Unity -- the console should show you connecting to the Photon room and 2 players being inside it.
    3. Pressing the bumper on the ML Controller will start the localization process -- the cube in the scene should turn into a sphere at your controller position, indicating the clients have localized. The service status bar should show that the clients have been connected and are now sharing network coordinates.
    4. Pressing the "1" key on standalone will switch which camera view is being streamed to OBS -- to set up OBS, head to the "Combine Camera Streams Using OBS" section.

### Create the NDI Rig
1. In the NetworkAnchorsExample scene, create a new Empty GameObject, name it NDIController.
2. Add the NDI Controller component to the object.
3. Create an empty GameObject as a child of the NDI controller. Name it Root.
4. Create a new Camera as a child of the Root object. Name it NDICamera.
5. Set the Camera's Clear Flags to Solid Color, and set the background to Black with zero opacity.
6. Add the Ndi Sender Component, select Enable Alpha and set the capture method to camera*. Set the sender's Source Camera as itself.
7. Set the NDIController's main camera to the scene's Main Camera and the NDI camera as the child NDICamera.

**Note: If using the standard render pipeline, you must instead set the Ndi Sender capture method to "game view". If using URP, you can use "camera", but you must set the NDI Camera's Camera/HDR setting to "Off".*

### Combine Camera Streams using OBS

1. Download [OBS](https://obsproject.com/)
2. Install the [OBS NDI plugin](https://github.com/Palakis/obs-ndi/releases/tag/4.9.1)
3. Inside your OBS project, add a new NDI Source to the scene.  
![image](https://user-images.githubusercontent.com/38482323/129374634-0a32d0ae-7449-4a6d-bc93-a0b6212c5628.png)
![image](https://user-images.githubusercontent.com/38482323/129374689-6dbb28a4-729c-4f43-b819-021826884751.png)
4. Add a new video source and select the camera you are using to capture from the headset’s position as the Source Name.  
5. Make sure your NDI Source is above the Video Source in the scene hierarchy.  
![image](https://user-images.githubusercontent.com/38482323/129375076-4c43c47b-e1fd-4835-989f-de7d11e3db28.png)

*If using the sample scene, skip to step 8. If building from scratch, follow 6-7.*

6. Follow the steps listed in the [KlakNDI](https://github.com/keijiro/KlakNDI) repo to add an NDI stream to your Unity project.
7. Inside your Unity project, make sure that the NDI component has alpha enabled in its NDI Sender Script and the Camera that you are using as your source uses a solid black background with transparent alpha. 
![image](https://user-images.githubusercontent.com/38482323/129375993-5a10fdbd-e99b-4c7b-a3d8-9f692c294e13.png)

8. Start the game on the desktop and headsets.
9. To create or find a network anchor on the Magic Leap Headsets, press the bumper on the controller. On the desktop, select the game and press the spacebar. To switch which camera is streaming to OBS, press the "1" key.
10. Once the client and server Unity applications are connected, select the NDI Source in the Sources panel inside the OBS application.
11. Select the gear icon to open the NDI's source properties.
12. Select the drop down arrow near the Sources Name Field.  
13. Set Bandwidth to Highest, Sync to Source Timing, and Latency mode to Normal.  
![image](https://user-images.githubusercontent.com/38482323/129375339-bafb4275-fee9-45ee-9be5-c498e8069f58.png)
14. OBS should now display a feed of the virtual content from the headset’s perspective with a transparent background overlaid on top of the video feed.   
15. Adjust the size of the NDI stream capture to match the full screen of the video capture  
![image](https://user-images.githubusercontent.com/38482323/129375444-b2776e51-59b4-49c4-85dc-52c6c05608a2.png)
16. When you are ready to record the composited video, press Start Recording under the Controls header.

## Troubleshooting 
### Building Demo Project
* If you get an error about shaders and the Hidden/Spout folder -- just run the project again and it should clear up.
### NDI Stream not showing as option in OBS
* Double check the steps in the "Create the NDI Rig" section -- make sure if you're using the standard render pipeline, you set the Ndi Sender capture method to "game view". If using URP, you can use "camera", but you must set the NDI Camera's Camera/HDR setting to "Off".
### NDI stream latency issues:
If you are experiencing significant delays between the NDI stream and the video stream, try the following steps:
* Right click on your Video Capture source and select Filters 
* Under Audio/Video Filters, click the + and select Video Delay (Async)
![image](https://user-images.githubusercontent.com/38482323/129377076-356757ca-312f-4562-8e32-f468832e8d0a.png)
* Enter a name for this delay and input the number of milliseconds you want to delay the Video Feed by. You will need to test different lengths until the video feed playback matches the NDI stream output.
* Click [here](https://www.youtube.com/watch?v=xq9gZSDFse0) for a more detailed video tutorial on these steps.

If you are still having issues with NDI stream latency, you can try the following:
* Add a second machine to process the rendering
* If you are on a Windows computer, you can try the [Spout plugin for Unity](https://github.com/keijiro/KlakSpout).


## Included Packages
[com.magicleap.spectator.networkanchors](https://github.com/magicleap/com.magicleap.spectator.networkanchors)
A lightweight tool that enables co-location experiences using a shared anchor.

[jp.keijiro.klak.ndi@1.0.12](https://github.com/keijiro/KlakNDI)
Modified version of keijiro’s Klak NDI package. Modified to exclude Klak libraries from Magic Leap builds.

[jp.keijiro.klak.spout@1.0.1](https://github.com/keijiro/KlakSpout)
Modified version of keijiro’s Klak NDI package. Modified to exclude Klak libraries from Magic Leap builds.
