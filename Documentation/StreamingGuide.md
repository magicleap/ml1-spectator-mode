# NDI Streaming with OBS

This guide includes the steps required to retreve the streamed image from the Desktop and iOS client apps using OBS.

[toc]

<br/>

## Combine Camera Streams using OBS

### Download and Install

1. Download [OBS](https://obsproject.com/)
2. Install the [OBS NDI plugin](https://github.com/Palakis/obs-ndi/releases/tag/4.9.1)
3. Inside your OBS project, add a new NDI Source to the scene.  
![image](https://user-images.githubusercontent.com/38482323/129374634-0a32d0ae-7449-4a6d-bc93-a0b6212c5628.png)
![image](https://user-images.githubusercontent.com/38482323/129374689-6dbb28a4-729c-4f43-b819-021826884751.png)
4. Add a new video source and select the camera you are using to capture from the headset’s position as the Source Name.  
5. Make sure your NDI Source is above the Video Source in the source hierarchy.  
![image](https://user-images.githubusercontent.com/38482323/129375076-4c43c47b-e1fd-4835-989f-de7d11e3db28.png)

### Start Streaming

1. Start the game on the desktop and headsets.
2. To create or find a network anchor on the Magic Leap Headsets, press the trigger on the controller. 
3. To begin streaming press on desktop press the "1" key on your keyboard, this will also cylce through the players.
4. To stream on your iOS device, press the start sreaming button on the screen.

### Adjusting the Bandwidth

1. Once the client and server Unity applications are connected, select the NDI Source in the Sources panel inside the OBS application.
2. Select the gear icon to open the NDI's source properties.
3. Select the drop down arrow near the Sources Name Field.  
4. Set Bandwidth to Highest, Sync to Source Timing, and Latency mode to Normal.  
![image](https://user-images.githubusercontent.com/38482323/129375339-bafb4275-fee9-45ee-9be5-c498e8069f58.png)
5. OBS should now display a feed of the virtual content from the headset’s perspective with a transparent background overlaid on top of the video feed.   
6. Adjust the size of the NDI stream capture to match the full screen of the video capture  
![image](https://user-images.githubusercontent.com/38482323/129375444-b2776e51-59b4-49c4-85dc-52c6c05608a2.png)
7. When you are ready to record the composited video, press Start Recording under the Controls header.


## Troubleshooting NDI stream latency issues:

If you are experiencing significant delays between the NDI stream and the video stream, try the following steps:
* Right click on your Video Capture source and select Filters 
* Under Audio/Video Filters, click the + and select Video Delay (Async)
![image](https://user-images.githubusercontent.com/38482323/129377076-356757ca-312f-4562-8e32-f468832e8d0a.png)
* Enter a name for this delay and input the number of milliseconds you want to delay the Video Feed by. You will need to test different lengths until the video feed playback matches the NDI stream output.
* Click [here](https://www.youtube.com/watch?v=xq9gZSDFse0) for a more detailed video tutorial on these steps.

NDI Stream not showing as option in OBS:
* Double check the steps in the "Create the NDI Rig" section -- make sure if you're using the standard render pipeline, you set the NDI Sender capture method to "game view". If using URP, you can use "camera", but you must set the NDI Camera's Camera/HDR setting to "Off".

If you are still having issues with NDI stream latency, you can try the following:
* Add a second machine to process the rendering.
* If you are on a Windows computer, you can try the [Spout plugin for Unity](https://github.com/keijiro/KlakSpout).


The image streams but alpha channel shows as black.
* Inside your Unity project, make sure that the NDI component has alpha enabled in its NDI Sender Script and the Camera that you are using as your source uses a solid black background with transparent alpha. 
![image](https://user-images.githubusercontent.com/38482323/129375993-5a10fdbd-e99b-4c7b-a3d8-9f692c294e13.png)
