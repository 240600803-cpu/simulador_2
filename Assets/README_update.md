# Simulador - Actualización

He añadido varios scripts y utilidades para las características que pediste:

- Obstáculos: `Assets/Scripts/ObstacleSpawner.cs` — genera obstáculos (primitivos o prefabs) en un área.
- Temporizador y puntuación: `Assets/Scripts/GameManager.cs` — controla el tiempo del nivel, puntuación y pantalla de fin.
- Escena industrial procedural: `Assets/Scripts/SceneBuilderIndustrial.cs` — construye un "túnel" simple con primitivas y coloca player + launcher.
- Control por joystick: `Assets/Scripts/JoystickController.cs` — helper para mapear joystick/teclado a movimiento y rotación.
- Lectura por serie: `Assets/Scripts/SerialController.cs` — lector serial básico (ejemplo genérico).

Cómo usar la escena industrial (rápido):
1. Crea una escena nueva en Unity (File -> New Scene) y guarda como `Assets/Scenes/IndustrialScene.unity`.
2. Arrastra un empty GameObject a la jerarquía y nómbralo `SceneBuilder`.
3. Añade el componente `SceneBuilderIndustrial` al `SceneBuilder`.
4. Opcionalmente crea/arrastra prefabs simples: Player prefab (cube con Rigidbody y PlayerMovement), Launcher prefab (con LauncherController y spawnPoint).
5. Presiona Play — el script construirá el túnel y colocará el player y launcher.

Cómo usar obstacles y puntuación:
- Añade un GameObject vacío `Level` y agrega `ObstacleSpawner` y configura `obstacleCount`, `size` y `center`.
- Asegúrate de tener un `GameManager` en la escena (crear un empty `GameManager` y asignar `GameManager` script). Asigna referencias UI (timerText, scoreText, endPanel).
- En los scripts de proyectil o lógica de impacto, llama a `GameManager.Instance.AddScore(1)` cuando un proyectil alcance un objetivo.

Control por joystick:
- El script `JoystickController` usa los ejes "Horizontal" y "Vertical" (por defecto mapeados en Input Manager) para movimiento — funciona con joystick/gamepad sin cambios en muchos casos.
- Para rotación usa un eje llamado "Rotate" (no está creado por defecto). Puedes mapear un eje en Edit -> Project Settings -> Input Manager (ojo: el Input Manager clásico soporta hasta 11 axes por defecto; ajusta si es necesario).
- También escucha `JoystickButton0` (botón A / trigger) para disparar junto con la lógica del LauncherController.

Control por Serial:
- `SerialController` abre un puerto serial y lee líneas en un hilo. Es un ejemplo para que adaptes al firmware de tu caja de control. No expone valores directamente; puedes parsear `latestLine` en `Update` y aplicar a actuadores.

Siguientes pasos que puedo hacer (elige):
- Generar la escena `IndustrialScene.unity` ya construida en el repo para que la descargues y abras directamente.
- Añadir prefabs (Player, Launcher, Projectile, Obstacle) para que la escena corra sin pasos manuales.
- Añadir efectos VFX/Sound y límites de proyectiles/recarga.

Dime cuál opción prefieres y lo añado al repo y te paso el ZIP con todo ya listo para abrir en Unity.