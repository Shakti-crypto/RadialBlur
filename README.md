# Radial Blur Shader and Render Feature for Unity URP

This repository contains a custom radial blur effect implemented as a Unity URP Render Feature. The shader and accompanying scripts allow you to add a radial blur effect to your Unity projects with ease.

![Radial Blur Demo](https://github.com/Shakti-crypto/RadialBlur/blob/main/ReadmeMedia/RadialBlurDemo-min.gif)

## Key Features
- Developed for **Unity 2021.3.15f1** and **URP 12.1.8**.
- Includes a fully functional demo scene: **RadialBlurDemoScene**.
- Simple setup instructions to integrate into your project.
- Adjustable blur strength and width for fine-tuning.

## Demo Scene
Explore the **RadialBlurDemoScene** to see the effect in action:
1. Navigate to the **GlobalVolume** in the scene.
2. Adjust the radial blur effect settings and watch the changes in real-time.

The demo scene also utilizes the **[FREE Skybox Extended Shader](https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400)** by **BOXOPHOBIC** . Special thanks to **BOXOPHOBIC** for this excellent asset.

## Setup Instructions
Follow these steps to replicate and use the radial blur effect in your project:

1. **Copy Required Files**:
   - Copy the scripts from:  
     `Assets/_Project/_Script/Effects`
   - Copy the shader from:  
     `Assets/_Project/Shaders`

2. **Add Render Feature**:
   - Open your URP Renderer settings.
   - At the bottom, click **Add Render Feature**.
   - Select **Radial Blur Render Pass Feature**.
   - Assign the **RadialBlur** shader to the **Radial Shader** variable in the render feature settings.

3. **Configure Render Pass**:
   - Set the **Render Pass Event** to **After Rendering**.

4. **Enable Effect in Post-Processing**:
   - Go to your **Post-Processing Volume**.
   - Select **Add Override**, then choose **Custom**, and select **Radial Blur Effect Component**.
   - Adjust **Blur Strength** and **Blur Width** to your desired values.

## Parameters
- **Blur Strength**: Controls the intensity of the blur effect.
- **Blur Width**: Adjusts the spread of the blur from the center outward.

## Acknowledgments
This project utilizes the **[FREE Skybox Extended Shader](https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400)** by **BOXOPHOBIC** to create a visually appealing demo scene. Many thanks to **BOXOPHOBIC** for providing this asset for free.

## Notes
- Ensure that your project is set up to use URP.
- Make sure the **Post-Processing Volume** is turned on for your Main Camera and correctly configured in your scene.

## Contributing
Feel free to fork the repository and suggest improvements via pull requests. If you encounter any issues, please report them in the **Issues** section of this repository.

## License
This project is open-source and available under the [MIT License](LICENSE).
