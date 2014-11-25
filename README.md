unity-ads-demo
==============

Unity Ads integration demo for Unity3D.

## Prerequisites

Download and import the latest version of [Unity Ads from the Asset Store](https://www.assetstore.unity3d.com/en/#!/content/21027).

## Unity Ads Helper Script

A quick drag-n-drop solution to integrating Unity Ads:
* [Unity Ads Helper Script](https://gist.github.com/wcoastsands/9c1682579412efd49e32) 

### How to Use It

Simply add it to a new game object in your scene, enter the game IDs specific to each platform in the fields provided, and then call UnityAdsHelper.ShowAd() from anywhere in your game code to show an ad.

We still recommend that you first check that ads are ready and available, so best to place UnityAdsHelper.ShowAd() within a conditional statement that calls UnityAdsHelper.isReady(). If you're referencing this script from a UnityScript, first move the UnityAdsHelper script to the Standard Assets directory.

And finally, enable Development Mode in Build Settings to enable test mode and SDK debug levels for Unity Ads. While test mode is enabled, test specific game IDs will be used if they are provided.

### Features and Benefits

A few of the benefits of using the UnityAdsHelper script:
* Manage game IDs by platform 
* Manage test mode settings
* Manage SDK debug output levels

These settings are automatically handled by the UnityAdsHelper script based on whether Development Build is enabled or disabled in Build Settings. This gives you the flexibility to test without the hassle of having to make changes to your code, or risk submitting a production build with test mode still enabled. 

Use this script to customize and create new ShowAd and HandleShowResult methods. Then simply call your custom ShowAd methods from any script in your project.

If you want to keep statistics generated while testing separate from production stats, create a secondary game profile for each platform in the [Unity Ads Admin](http://unityads.unity3d.com/admin/). Then enter the new game IDs for these profiles into the Test Game ID fields for each platform in the inspector. Test profiles can also be useful if you have a development server for testing [S2S redeem callbacks](https://github.com/Applifier/unity-ads/wiki/Server-to-server-Redeem-Callbacks).

## Unity Ads Demo Scripts

The following demo scripts are just a few examples of how to integrate Unity Ads into your Unity project. Each demo is fully commented to help you get up to speed quickly, and better understand Unity Ads:
* [Basic Unity Ads Demo](https://gist.github.com/wcoastsands/3d003528a76f94fa2ede)
* [Robust Unity Ads Demo](https://gist.github.com/wcoastsands/ddca26812e5e31bc81e3)
* [Super Simple Unity Ads Demo](https://gist.github.com/wcoastsands/b2b8b018a0ffe07e3aee)
