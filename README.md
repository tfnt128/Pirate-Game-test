# Pirate Adventure Game

- **Basic Controls:**
  - **Move Forward:** Press the **W key** or **Up arrow**.
  - **Rotate Left:** Press the **A key** or **Left arrow**.
  - **Rotate Right:** Press the **D key** or **Right arrow**.
  - **Shoot Cannons:**
    - Press the **Spacebar** or **Middle Mouse Button** to fire the front cannon.
    - Use the **Left Mouse Button** or **Z key** to fire the three left cannons.
    - Employ the **Right Mouse Button** or **X key** to unleash the three right cannons.
  - **Options in Main Menu:**
    - Adjust **Game Session Time** and **Spawn Enemy Rate** in the Options menu.

- **Adjusting Spawn Rates and Game Session Time in the Main Menu:**
  - Navigate to the Main Menu and enter the `Options` section.
  - Inside the Options menu, you'll find sliders for customizing both spawn rates and game session time.
  - **Spawn Rates:** Use the slider to set the frequency of enemy appearances, ranging from 2 to 10 seconds.
  - **Game Session Time:** Adjust the slider to determine the duration of your game session, with options spanning from 60 to 180 minutes.
  - Fine-tune these settings to tailor your gameplay experience to your preferences.
  - Click on `Save` to apply the changes, then proceed to play and have fun!
    
Enjoy the thrilling experience of sailing the high seas, avoiding enemies, and firing powerful cannons at your foes!

## Advanced Gameplay

For those who want to dive deeper into the mechanics and customization options inside unity's editor, here's a detailed breakdown:

- **Inspector Customization:**
  - Explore the Unity Inspector to customize cannon controls:
    - Copy and rotate cannon sprites to add more cannons.
    - Choose firing buttons for each cannon.
    - Adjust firing rates to control the tempo of cannon shots.
  
- **Enemies:**
  - Encounter two types: Chaser Enemies and Shooter Enemies.
  - Chaser Enemies follow your ship, attempting collisions for damage.
  - Shooter Enemies detect and fire upon your ship when in range.
  - You can customize their specific speed for chasing the player and shooters' fire rate in the inspector.

- **Spawning:**
  - Use a simple spawner to define locations and prefab types.
 
- **Game Session:**
  - A timer limits each game session.
  - Game over screen displays points earned, with options to play again or return to the main menu.

- **Scoring:**
  - Earn points by defeating enemies through standard methods.
  - Points are not awarded for non-standard enemy deaths.
 
- **Pooling System:**
  - Cannons use a pooling system, efficiently reusing cannonballs.
  - Different cannon types have dedicated pools managed by `ProjectPoolManager`.

- **Creating a New Projectile Type and Pool:**
  - Navigate to the `Data` folder in your project.
  - Right-click, go to `Create`, and select `Create Projectile Type`.
  - Customize the newly created `ProjectileTypeSO` asset by changing the prefab and adjusting the max size for this specific pool.
  - Locate `CannonsManager`, a parent of `Managers` in the hierarchy.
  - Add the newly created `ProjectileTypeSO` scriptable object to the list within `CannonsManager`.
  - Now, if you want to apply this new projectile to a specific player or enemy, drag the `ProjectileTypeSO` scriptable object onto the designated slot in the projectile type section.
  - With these steps, the new projectile is configured with its pool and customized settings.

- **Health System:**
  - Characters (ships) share a unified health system.
  - Deterioration occurs when health reaches a critical threshold, leading to sprite changes.
 
- **Changing Deterioration and Health Threshold:**
  - To alter the deterioration sprites, navigate to your player or enemy ship in the hierarchy.
  - Find the `ShipDeterioration` script, and in the `Deterioration Sprites` section, modify the sprites. The order in the list dictates the level of deterioration, with lower sprites indicating more deterioration.
  - To adjust the health threshold (the life at which deterioration level changes), find the `HealthManager` script on your player or enemy ship in the hierarchy.
  - Look for the Deterioration Level configuration and click on the scriptable objects.
  - Modify the level enumerators and the health threshold for that specific level.
