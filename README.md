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
### Unity
Unity is the core development platform used to build the AR navigation system. Its robust 3D engine enables the creation of real-time navigation solutions by leveraging features like AI Navigation and AR integration. Unity allows for precise pathfinding, dynamic environment handling, and responsive visual elements, which are crucial for guiding users through the railway station model. Additionally, Unity’s cross-platform support makes it adaptable for mobile apps and digital kiosks, ensuring flexibility and scalability in delivering the navigation experience across devices.
### Unity UI
Unity UI is leveraged to build highly interactive and adaptive user interfaces for the AR navigation app. The system supports the creation of intuitive menus, buttons, and real-time overlays, enhancing user interaction with the navigation features. Its responsive design capabilities ensure seamless functionality across a variety of screen sizes and devices, offering a consistent user experience from mobile platforms to larger digital displays, making it highly adaptable for this AR-driven project.

<br />
<div align="center" >
  <img src="https://github.com/user-attachments/assets/1ebf574b-a6bb-457a-bd37-41f7e16f7d92" width=200 />&nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://github.com/user-attachments/assets/ab4f98ad-4ff0-45dd-833d-5f976644338f" width=200 />&nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://github.com/user-attachments/assets/30799114-916d-4045-bbff-2a3c01baf254" width=186.5 /><br />
  <i>Fig: User Interface of the App</i>
</div>
<br />

### AI-Based Path Finding and Navigation
Unity’s AI Navigation library offers robust pathfinding capabilities for complex 3D environments. At its core, NavMesh (Navigation Mesh) maps out traversable areas and dynamically updates as environments change. NavMeshAgents use this mesh for precise pathfinding, while NavMeshLinks and NavMeshSurfaces allow agents to cross varied terrains and transitions seamlessly. For AR applications like SIH1710, these features can enhance real-time navigation within large railway stations, providing responsive, obstacle-aware paths and improving accessibility for all users in dynamic environments.

<br />
<div align="center">
  <img src="https://github.com/user-attachments/assets/54bc25b3-fd5b-4b3c-b6ee-fba3c4bc33f1" width=500 /><br />
  <i>Fig: Demonstration of the navigation facility in the app</i>
</div>
<br />

## Implementation
This project has been created, implementing all the above technologies in an efficient way, such that it can find paths for complex routes too. This will be of great use for people to navigate to places, specially targeting people who are new to the place, and people with disabilities. This project also focuses on accessibility improvements such that disabled people find it easier to navigate, with technologies such as voice playback and haptic feedback when they navigate through the path. 
### Railway Star Mobile App - Build and Release
This project aims to provide a cross-platform application, but here (Unity Build Configuration) the build settings are configured only for Android (API version <34). Since its made with Unity, we can change the build system to support other mobile operating systems such as IOS, etc. 
### Kiosks
This project also implements a seperate scene for Kiosks, enabling ***Bird's Eye View*** of a Railway Station and its catering shops, platforms, and other service providers. Similar to minimaps in games, kiosks are expected to have a display with navigation console, to look across various locations from a static point. Kiosks are expected to be present at three places in each platform.

<br />
<div align="center" >
  <img src="https://github.com/user-attachments/assets/b9de7ed8-31fb-4447-b704-8da2176e88e0" width=400 />&nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://github.com/user-attachments/assets/886ae5b9-0abe-40b8-9120-0f6f6d0ae294" width=420 /><br />
  <i>Fig: Bird's Eye View - Unity Prototype</i> 
</div>
<br />

### Technical Constraints and Resource Utilization
- We have used the LIDAR sensor in iPad Pro, and not the professional grade LIDAR sensor which is expensive. The iPad Pro's LIDAR sensor introduces limitations in area target precision, with potential deformations due to its scanning scope, leading to reduced spatial accuracy than one generated with the industry-rated sensors.
- Instead of a railway station, we modeled our navigation system using our college environment, which may not fully reflect station-specific challenges. But it can be scaled up to handle navigation for a railway station with the LIDAR sensor as mentioned previously. This is just a prototype.
- The current UI serves as a preliminary prototype with basic functionality. Future iterations will focus on refining the user experience and interface performance based on project needs and feedback.
