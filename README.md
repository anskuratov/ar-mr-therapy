# AR Exposure Therapy

  Unity augmented (mixed) reality application for Google Tango Lenovo Phab 2 Pro for exposure therapy with _snake_, _spider_ and _rat_.

## Description

  The developed MR system is based on a **_Google  Tango_** smartphone (Lenovo Phab 2 Pro). This smartphone is equipped with motion tracking and depth sensors. It allows the system to integrate depth information in a scene efficiently. The system will reconstruct the surface in the scene including its illuminant condition and will detect the ground plane of the scene and other planes as well with the support of Tango SDK. Then, a 3D model of a virtual animal will be added to the reconstructed scene, and the scene with the animal will be rendered using Unity game engine. The rendered image will be shown on the screen of the smartphone so that the image will be seen by an observer using a VR (virtual reality) smartphone holder (e.g., Google cardboard). The system will operate this whole process in real time.

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

  We selected rat as one of the disgusting and frequently faced animal in Russia. For example, it is possible to meet a rat in the Moscow subway, near garbage cans, in cellars. Some people consider rats as potential disease carriers what cause fear and phobic avoidance of this animal.

###### List of animations ([rat_60fps](https://github.com/anskuratov/ar-exp-therapy/blob/models/Resources/rat/maya/Assets/rat_60fps.fbx)):

* **fr[0 - 1]** : Bind Pose;
* **fr[2 - 160]**: IDLE (Starts with Bind P. and ends with the same P.);
* **fr[161-199]**: Bind Pose **→** Stand;
* **fr[200-318]**: Itches;
* **fr[319-355]**: Stand P. **→** Bind Pose;
* **fr[356-414]**: Bind Pose **→** Movement;
* **fr[415-490]**: Movement;
* **fr[491-520]**: Movement **→** Bind Pose.

## Table of Contents

## Installation

## Usage

## Credits

## License
MIT License

[Nikita Sergeev](https://github.com/ndsergeev) | [Andrew Skuratov](https://github.com/anskuratov)
Copyright 2018

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
