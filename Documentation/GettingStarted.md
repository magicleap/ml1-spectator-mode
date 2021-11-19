# Getting Started with Spectator Mode

This guide will walk you through implementing a spectator view in a new scene.

## Co-Located Networking Scene Requirements

**Magic Leap**
- Unity 2020.3.x
- Lumin SDK 0.26.0+
- Magic Leap Unity SDK / Package
- Photon App ID
- com.magicleap.spectator.networkanchors
- .NET Standard 2.0 profile

***Note:** If you need guidance on downloading the Magic Leap Unity SDK or configuring your Unity project to build to a Magic Leap headset, check out the ["Getting Started In Unity Guide"](https://developer.magicleap.com/en-us/learn/guides/unity-setup-intro).*

<br/>

## Create A Co-Location Network Experience
In this section we will use Photon and the included Network Anchors package to create a multi-user, co-located scene.

### Implement Network Anchors
Follow the Network Anchors [Install Guide](https://github.com/magicleap/com.magicleap.spectator.networkanchors)

## View content using OBS

### Create the NDI Rig
1. Switch your project to Standalone and set the Graphic API to the one listed in the Requirements section.
2. In your scene, create a new empty GameObject and name it NDIController.
3. Add the **NDI Controller** component to the object.
4. Create an empty GameObject as a child of the NDI controller. Name it Root.
5. Create a new Camera as a child of the Root object. Name it NDICamera.
6. Set the Camera's Clear Flags to Solid Color, and set the background to Black with zero opacity.
7. Add the NDI Sender Component, select Enable Alpha. 
8. Set the sender's Source Camera as itself.
9. Set the capture method to the following depending on project's renderer:
    - If using the standard render pipeline, you set the NDI Sender capture method to "Game View". 
    - If using URP, you set the capture method to "Camera" and the NDI Camera's Camera/HDR setting to "Off".
10. Set the NDIController's main camera to the scene's Main Camera and the NDI camera as the child NDICamera.

<br/>

To learn how to record your game through the desktop client view our [Streaming Guide](./StreamingGuide.md)