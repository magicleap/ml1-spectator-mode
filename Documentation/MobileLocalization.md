# Mobile Localization
### Co-located Shared Experiences between Magic Leap devices and mobile phones running iOS or Android.

By using a shared image target, a Magic Leap headset can share virtual content in a co-located experience with any modern smartphone thanks to Unity's ARFoundation and our Network Anchors package. A platform-specific scene is built to each device, and by scanning the same image in the same physical location, all devices can share networked coordinates. In this scene, the only shared content is a central object that turns from a cube to a sphere when network coordinates have been successfully localized, and a sphere over each device that follows the devices position.

<br/>

## Image Tracking and Mobile Localization Dependencies

**Magic Leap** 
- Unity 2020.2 +
- Lumin SDK 0.25.0+
- Photon
- MagicLeap.Spectator.NetworkAnchors

**Mobile (iOS and Android)**
- Unity 2020.2 +
- ARFoundation 4.1.7 +
- Photon
- MagicLeap.Spectator.NetworkAnchors

<br/>

## Setup

The image tracking localization demo uses a shared scene called "MobileLocalizationTest" that is used by all clients, combined with one other scene based on which platform you're building to.

The MobileLocalizationTest scene can be adjusted to use any image target you choose, though reference the [Magic Leap image target page](https://developer.magicleap.com/en-us/learn/guides/lumin-sdk-image-tracking) to choose a reliable image. We'll be using the submarine image from that page:

![Magic Leap Submarine image target](submarine.png)

To use your own image, load the image into your Assets folder. Select the image -- in the inspector, make sure two fields are adjusted:
- Set "Non-Power of 2" to "None"
- Set "Read/Write Enabled" to enabled (checked)

Measure the longest side of the image as it will be displayed when used in the co-located experience (i.e. if displaying on a screen, measure it at the exact window/screen size, or if using a printed image, measure the printed size). For our submarine image, the longer dimension is the horizontal side (length), and when displayed full-screen on our laptop, the length is 0.288 meters. Record the measurement **in meters**, you'll need it later.

Next we'll update platform specific components. 

### Magic Leap Scene Components

Open the MobileLocalizationTest scene. In the hierarchy under PunNetworkAnchorController > MultiPlatformCoordinateProvider > MagicLeapCoordinateProvider, there is a script component called "ML Generic Coordinate Provider"
- The name field is not important, name it whatever you want.
- Select the image you uploaded under the "Image" field in the inspector.
- Type the measurement you recorded eariler under the "Longer Dimension" field. *Make sure you're typing it in meters.*
- The other fields don't need to be adjusted by default. The Image Target Visual is the prefab that will be attached to the image to indicate the Magic Leap has detected the image target -- in our case, a small green cube.

### Mobile Scene Components

Open the ARFoundation_Rig scene. For ARFoundation's Image Tracking, we need to make an XR Reference Image Library.
- Right click in our Assets folder and select Create > XR > Reference Image Library. 
- Then, in the inspector of the newly created Library, select "Add Image". 
- Select the image you uploaded in the "Texture 2D" field.
- Name the image whatever you want.
- Select "Specify Size" and enter the measurement you recorded earlier under the appropriate dimension (i.e. our submarine, the X size is 0.288 meters). The other dimension should scale accordingly.
- Select "Keep Texture at Runtime".
    
Then, in the hierarchy under ---AR Foundations--- > AR Session Origin, there is a script component called "AR Tracked Image Manager". 
- Select the XR Reference Image Library we created in the "Serialized Library" Field.
- The other fields don't need to be adjusted by default. The Tracked Image Prefab is the prefab that will be attached to the image to indicate the Magic Leap has detected the image target (small green cube).

Lastly, make sure you add a scripting define symbol to the Player Settings:
- Go to Project Settings > Player > (Android or iOS, depending on which device you're building to) > Other Settings > Scripting Define Symbols.
- Add a new input field by clicking the plus symbol, and type "AR_FOUNDATION".

<br/>

## Building to Device

**Magic Leap Build Settings**
- Make sure the MobileLocalizationTest scene and the MagicLeap_Rig scene are both selected, with the MobileLocalizationTest scene above the MagicLeap_Rig scene in priority.
- In Project Settings > Magic Leap > Manifest Settings > Reality, make sure "Camera Capture" privilege is enabled.
- The other build settings are the same as the previous demos in this project.

**Mobile Build Settings**
- Select the appropriate build platform (iOS or Android).
- Make sure the MobileLocalizationTest scene and the ARFoundation_Rig scene are both selected, with the MobileLocalizationTest scene above the ARFoundation_Rig scene in priority.
- For more detailed instructions on building to mobile phones, [this guide has a good walkthrough for each platform](https://www.raywenderlich.com/14808876-ar-foundation-in-unity-getting-started).

<br/>

## Running The Scenes

- Once scenes have been built to all devices, simply open the app on each device, and turn the camera to face the image you have set up. There are different scanning and connection instructions per device:
    - Magic Leap
        - Pressing the trigger starts the scanning process. A small red camera icon will appear in the top left of the view, indicating that it is currently looking for image targets. Once your image is detected, the green cube prefab will appear in the center of the image. After a few seconds, the camera icon will disappear. If the image ever moves or the prefab looks off, simply rescan to update the coordinates.
        - Pressing the bumper after scanning the image will send your localized coordinates to the other clients in your room. The cube in the center should turn into a sphere, indicating a successful localization.
    - Mobile
        - The phone will automatically and constantly scan for image targets. Simply point the camera at the image target and the green cube prefab should appear in the center of the image. If the cube starts to drift, simply look at the image again to recenter it.
        - Once you've scanned the image, press the "Create Anchor" button to send your localized coordinates to theother clients in your room. The cube in the center should turn into a sphere, indicating a successful localization.
- Photon should automatically connect all clients in a networked room. Before images have been scanned and network anchors sent, you may see the other clients' spheres in incorrect positions. Once all clients detect the image and create their shared network anchor, the sphere positions should be updated correctly.
- Once all devices have connected, detected the image, and a shared anchor has been created, clients should be able to see a sphere following each device's location, indicating the network coordinates have been synced successfully. 

<br/>

## Troubleshooting

- For viewing debug logs on mobile, we recommend using XCode for iOS and Unity's AndroidLogcat for Android.
- For viewing debug logs on the Magic Leap, you can use The Lab's Device Bridge.
- If your Magic Leap is having trouble syncing the network anchors, your device may not have access to multi-user PCFs, which are needed to anchor the network coordinates. Make sure when you start the Magic Leap, it has recognized your playspace or that you rescan the room. If your debug logs say "0 Multi PCFs found" when creating a network anchor, this is likely the issue.
- If clients have detected the image successfully, but don't appear in each other's scenes, you might need to restart the app. Sometimes Photon doesn't place users in the same networked room if the clients open the app at the same time.
- If the prefabs aren't showing up on the image, you may not be detecting the image -- make sure the lighting is sufficient and that you aren't too close or too far from the image.
- If the prefab is not in the center of the image (too close or too far), make sure you've input the correct image measurements. A cube that is too close means the inputed measurement is smaller than the physical dimension, and too far means it is larger than the actual size.