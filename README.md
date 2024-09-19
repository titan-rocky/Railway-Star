<h1 align="center">Railway Star</h1>
<div align="center" style="">
  <img src="https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white" alt="Unity">&nbsp;
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white" alt="C#">&nbsp;
  <img src="https://img.shields.io/badge/android%20studio-346ac1?style=for-the-badge&logo=android%20studio&logoColor=white" alt="Android Studio">&nbsp;
  <img src="https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white" alt="Git">&nbsp;
</div>
<p align="center">
  <i>&quot;I feel that Augmented reality is perhaps the ultimate computer.&quot; — Satya Nadella</i>
</p>
<p align="center">
  An AR-based Navigation System for Railway Stations
</p>

## Introduction
This project has been developed as a solution for the problem statement **SIH 1710 - Enhancing Navigation for Railway Station Facilities and Locations** posted by the **Ministry of Railway** of India, submitted for the *[Smart India Hackathon 2024](https://www.sih.gov.in/)*.

### Overview
This project focuses on developing a navigation system for railway stations, making it easier for passengers to find facilities such as platforms, ticket counters, restrooms, and food courts. The solution includes:
- Mobile app with AR based interactive navigation
- Touch-screen digital kiosks with navigation systems
- Voice-guided navigation for the visually impaired
- Real-time updates for layout changes
- Seamless integration with existing railway services

## Technology
### Vuforia By PTC
Vuforia by PTC is an advanced augmented reality (AR) platform that allows developers to create immersive AR experiences for mobile and digital devices. It's commonly used for industrial applications, product visualization, and interactive learning by integrating 3D content into real-world environments. Learn more at [Vuforia](https://www.ptc.com/en/products/vuforia).
### Area Targets
Vuforia’s **Area Targets** provide superior tracking by mapping entire physical environments, allowing for precise AR experiences across large spaces like retail stores or industrial facilities. In contrast to **Image, Model, and Object Targets**, which rely on specific visual markers, Area Targets offer spatial recognition for continuous, environment-wide tracking. This makes them more effective for AR applications needing persistent localization and interaction. Their resilience to lighting variations and scale makes them ideal for creating highly immersive AR experiences in expansive environments.
### Unity AI Navigation
Unity’s AI Navigation library offers robust pathfinding capabilities for complex 3D environments. At its core, NavMesh (Navigation Mesh) maps out traversable areas and dynamically updates as environments change. NavMeshAgents use this mesh for precise pathfinding, while NavMeshLinks and NavMeshSurfaces allow agents to cross varied terrains and transitions seamlessly. For AR applications like SIH1710, these features can enhance real-time navigation within large railway stations, providing responsive, obstacle-aware paths and improving accessibility for all users in dynamic environments.
## Implementation
This project has been created, implementing all the above technologies in an efficient way, such that it can find paths for complex routes too. This will be of great use for people to navigate to places, specially targeting people who are new to the place, and people with disabilities. This project also focuses on accessibility improvements such that disabled people find it easier to navigate, with technologies such as voice playback and haptic feedback when they navigate through the path. 
### Railway Star Mobile App - Build and Release
This project aims to provide a cross-platform application, but here (Unity Build Configuration) the build settings are configured only for Android (API version <34). Since its made with Unity, we can change the build system to support other mobile operating systems such as IOS, etc. 
### Kiosks
This project also implements a seperate scene for Kiosks, enabling ***Bird's Eye View*** of a Railway Station and its catering shops, platforms, and other service providers. Similar to minimaps in games, kiosks are expected to have a display with navigation console, to look across various locations from a static point. Kiosks are expected to be present at three places in each platform.
### Technical Constraints and Resource Utilization
- We have used the LIDAR sensor in iPad Pro, and not the professional grade LIDAR sensor which is expensive. The iPad Pro's LIDAR sensor introduces limitations in area target precision, with potential deformations due to its scanning scope, leading to reduced spatial accuracy than one generated with the industry-rated sensors.
- Instead of a railway station, we modeled our navigation system using our college environment, which may not fully reflect station-specific challenges.
- The current UI serves as a preliminary prototype with basic functionality. Future iterations will focus on refining the user experience and interface performance based on project needs and feedback.
