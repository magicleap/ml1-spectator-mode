# Spectator View

This project demonstrates how to render virtual content using an external camera and a Magic Leap's headset position. This readme includes the instructions as well as the technical information required to create custom streaming solutions. Please report any questions and issues using Github’s issue ticketing system.  

**Note**:  
The NDI streaming plugin is compatible with more platforms while the Spout plugin offers better video streaming latency.

This project uses submodules. Use `git clone --recurse-submodules` when cloning.

## Requirements
**Standalone Client** 
- Unity 2020.2 +
- Windows: D3D11 and D3D12 are supported 
- macOS: Metal required
- Linux: Vulkan required
- Photon

**Standalone Client (Spout Streaming)**
- Unity 2020.2 +
- URP / HDRP
- Windows: D3D11 and D3D12 are supported  
- Photon

**Magic Leap** 
- Unity 2020.2 +
- Lumin SDK 0.25.0+
- Photon

## Getting Started
The following steps are required create or add a spectator view to your project :
1) Create a co-location network experience.
2) Use a desktop client to render a virtual camera mapped to the Magic Leap's position.
3) Combine the virtual and physical camera stream using OBS

*Prerequisites:* 
* Magic Leap Unity SDK / Package

### Create A Co-Location Network Experience
In this section we will use the Photon and the included Network Anchors package to create a colocation experience. If you are using an existing project that has co-location implemented, you can skip this section.

####  Enable Photon Examples
1. Add download PUN 2 from the asset store and import it into your project.
2. Define the your Photon credentials in using the PUN wizard or PUN Server settings.
3. To enable the photon example scripts, navigate to the Player Settings  (Edit > Project Settings , then click Player).
4. Add PHOTON to the Scripting Define Symbols, located under the Other Settings section, then click Apply.

#### Create a Simple Scene
1. From the menu select File> New Scene. Then select the Basic (Built-in) template.
2. Replace the existing main camera with the Magic Leap Camera Prefab located under `Packages/Magic Leap SDK/Tools/Prefabs`.
3. Save the new scene, name it NetworkAnchorsExample.

#### Create the Photon Controller
1. Create an empty GameObject named PhotonController
2. Add the Simple Photon Room and Simple Photon Lobby components.
3. Set the Photon User Prefab to the SimplePhotonUser prefab located under `Assets/Network Anchors Examples/ PhotonExample/Resources/`

#### Implementing Network Anchors
1. Create an empty GameObject called NetworkAnchor
2. Set the transform's position to `0, 0, 1` and the rotation to `0, 18, 0`.
3. Add the Network Anchor Service component to the object. 
4. Then and the Network Anchor Localizer component.
5. Use the Network Anchor's Localizer's OnAnchorPlaced event to visualize when the anchor has been created/localized:
    1. Create a cube as a child of the object
    2. Set the scale to `0.3, 0.3, 0.3` and disable it. 
    3. Select the NetworkAnchor
    4. Create a new OnAnchorPlaced event
    5. Set the event target as the Cube and the event as GameObject.SetActive, set it to true.
1. Add the MultiPlatformCoordinateProvider prefab from `Assets/NetworkAnchorsExamples/PhotonExample/Prefabs` into the scene.

#### Connect the Service to Photon
1. Create an empty GameObject called PunNetworkAnchorController
2. Add the PhotonNetworkAnchorController component.
3. Set the fields with the objects in your scene.

#### Call Create or Find Network Anchor
1. Select the Main Camera Prefab and add the Magic Leap Network Anchor Example component.
2. Set the Network Anchor Localizer field to target the Network Anchor

#### Build and Run
- Save your example scene.
- From the Project settings, enable the Internet privileges.
- Set your project's project Identification Information.
- Target your developer certificate in the project settings.
- Deploy to device. 

To create or find a network anchor on the Magic Leap Headsets, press the trigger on the controller. On the desktop, select the game and press the spacebar.

### Create the Desktop Client
We will use the Desktop client to render the Virtual Content. To match the virtual content's position with the external camera, we will create a virtual camera that can follow the network position of the Magic Leap.

#### Create the NDI Rig
1. Duplicate your project folder or switch your project to Standalone and set the Graphic API to the one listed in the Requirements section.
2. In your the NetworkAnchorsExample scene, create a new empty GameObject name it NDIController
3. Add the NDI Controller component to the object.
4. Create an empty GameObject as a child of the NDI controller. Name it Root.
5. Create a new Camera as a child of the Root object. Name it NDICamera.
6. Set the Camera's Clear Flags to Solid Color, and set the background to Black with zero opacity.
7. Add the Nndi Sender Component, select Enable Alpha and set the capture method to camera. Set the sender's Source Camera as itself.
8. Set the NDIController's main camera to the scene's Main Camera and the NDI camera as the child NDICamera.

### Combine Camera Streams using OBS
1. Download [OBS](https://obsproject.com/)
2. Install the [OBS NDI plugin](https://github.com/Palakis/obs-ndi/releases/tag/4.9.1)
3. Inside your OBS project, add a new NDI Source to the scene.  
![image](https://user-images.githubusercontent.com/38482323/129374634-0a32d0ae-7449-4a6d-bc93-a0b6212c5628.png)
![image](https://user-images.githubusercontent.com/38482323/129374689-6dbb28a4-729c-4f43-b819-021826884751.png)
4. Add a new video source and select the camera you are using to capture from the headset’s position as the Source Name.  
5. Make sure your NDI Source is above the Video Source in the scene hierarchy.  
![image](https://user-images.githubusercontent.com/38482323/129375076-4c43c47b-e1fd-4835-989f-de7d11e3db28.png)
6. Follow the steps listed in the [KlakNDI](https://github.com/keijiro/KlakNDI) repo to add an NDI stream to your Unity project.
7. Inside your Unity project, make sure that the NDI component has alpha enabled in its NDI Sender Script and the Camera that you are using as your source uses a solid black background with transparent alpha. 
![image](https://user-images.githubusercontent.com/38482323/129375993-5a10fdbd-e99b-4c7b-a3d8-9f692c294e13.png)

8. Start the game on the desktop and headsets.
9. To create or find a network anchor on the Magic Leap Headsets, press the trigger on the controller. On the desktop, select the game and press the spacebar.
10. Once the client and server Unity applications are connected, select the NDI Source in the Sources panel inside the OBS application.
11. Select the gear icon to open the NDI's source properties.
12. Select the drop down arrow near the Sources Name Field.  
13. Set Bandwidth to Highest, Sync to Source Timing, and Latency mode to Normal.  
![image](https://user-images.githubusercontent.com/38482323/129375339-bafb4275-fee9-45ee-9be5-c498e8069f58.png)
14. OBS should now display a feed of the virtual content from the headset’s perspective with a transparent background overlaid on top of the video feed.   
15. Adjust the size of the NDI stream capture to match the full screen of the video capture  
![image](https://user-images.githubusercontent.com/38482323/129375444-b2776e51-59b4-49c4-85dc-52c6c05608a2.png)
16. When you are ready to record the composited video, press Start Recording under the Controls header.

### Troubleshooting NDI stream latency issues:
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




