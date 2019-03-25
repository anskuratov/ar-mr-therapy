# AR Exposure Therapy

  Unity augmented (mixed) reality application for Google Tango Lenovo Phab 2 Pro for exposure therapy with _snake_, _spider_ and _rat_.

## Description

The developed augmented reality system is based on a **_Google  Tango_** smartphone (Lenovo Phab 2 Pro). This smartphone is equipped with motion tracking and depth sensors. It allows the system to integrate depth information in a scene efficiently. The system reconstructs the surface in the scene including its illuminant condition and detects the ground plane of the scene and other planes as well with the support of Tango SDK. Then, a 3D model of a virtual animal adds to the reconstructed scene, and the scene with the animal renders using the Unity game engine. The rendered image superimposed on the video stream in real-time so that the image is seen by an observer using a VR (virtual reality) headset (e.g., Google cardboard).

### Animals implemented

#### 1. Snake
  ![The snake render](https://github.com/AnSkuratov/ar-exp-therapy/blob/master/pic/snake_gh.jpg)
  We selected [**_Vipera Nikolskii_**](https://en.wikipedia.org/wiki/Vipera_nikolskii) snake for ophidiophobia (fear of snakes) study.
  
###### List of animations ([snakeall_anim](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/snake/maya/Assets/snakeall_anim.fbx)):
* **fr[0 - 74]** - Bind Pose **→** Start Movement P.;
* **fr[75 - 299]** Movement;
* **fr[300 - 374]** Movement **→** IDLE
* **fr[375 - 454]** Tongue Movement 2 Times;
* **fr[455 - 534]** IDLE;
* **fr[535 - 678]** Tongue Movement 1 Time;
* **fr[679 - 854]** Response **→** Action;
* **fr[855 - 1000]**: IDLE **→** movement.

#### 2. Spider
  ![The spider render](https://github.com/AnSkuratov/ar-exp-therapy/blob/master/pic/spider_gh.jpg)

  Selected spider is an [**_Argiope bruennichi_**](https://en.wikipedia.org/wiki/Argiope_bruennichi) (wasp spider) female. We chose to use this spider because it is one of the most common spiders in Russia. Recognizing this spider in his habitation area is straightforward. Argiope bruennichi has a very bright wasp-like colour.
  
  ###### List of animations (separate files):
  * **file[ [idle](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/idle.fbx) ]**: IDLE;
  * **file[ [hesitating](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/hesitating.fbx) ]**: Hesitating;
  * **file[ [fear_start](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/fear_start.fbx) ]**: Defending Pose Start;
  * **file[ [fear_interm](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/fear_interm.fbx) ]**: Defending Pose;
  * **file[ [fear_end](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/fear_end.fbx) ]**: Defending Pose End;
  * **file[ [move_01_start](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/move_01_start.fbx) ]**: Movement start;
  * **file[ [move_01_move](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/move_01_move.fbx) ]**: Movement;
  * **file[ [move_01_end](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/spider/maya/Assets/move_01_end.fbx) ]**: Movement end.
  
#### 3. Rat
  ![The rat render](https://github.com/AnSkuratov/ar-exp-therapy/blob/master/pic/rat_gh.jpg)

  We selected rat as one of the disgusting and frequently faced animal in Russia. For example, it is _possible_ to meet a rat in the Moscow subway, near garbage cans, in cellars. Some people consider rats as potential disease carriers what cause fear and phobic avoidance of this animal.

###### List of animations ([rat_60fps](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/rat/maya/Assets/rat_60fps.fbx)):

* **fr[0 - 1]** : Bind Pose;
* **fr[2 - 160]**: IDLE (Starts with Bind P. and ends with the same P.);
* **fr[161-199]**: Bind Pose **→** Stand;
* **fr[200-318]**: Itches;
* **fr[319-355]**: Stand P. **→** Bind Pose;
* **fr[356-414]**: Bind Pose **→** Movement;
* **fr[415-490]**: Movement;
* **fr[491-520]**: Movement **→** Bind Pose.

## System Requirements

This application will work just on Lenovo Phab 2 Pro.

Be aware, before cloning you need intalled [git-lfs](https://github.com/git-lfs/git-lfs/wiki/Tutorial).

## Credits

The project was developed during BSc studies and we would like to thank our scientific supervisor [Tadamasa Sawada](https://www.hse.ru/en/staff/tsawada) and advisor [Tomas Jurcik](https://www.hse.ru/en/org/persons/198160260) for their involvement in this project.

## License
[Nikita Sergeev](https://github.com/ndsergeev) | [Andrew Skuratov](https://github.com/anskuratov)
Copyright 2018

**The MIT licence does not apply to FBX animal models.** All animal models were developed in Autodesk Maya Student Version and you **must not** use them for commercial purposes, check the [link](https://www.autodesk.com/company/terms-of-use/en/subscription-types#education) for details.
