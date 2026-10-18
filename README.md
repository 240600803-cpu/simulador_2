# Simulador

Este repositorio contiene un proyecto base de Unity 2021.3 LTS para un simulador de lanzamiento de concreto sin VR, con control por teclado y un lanzador móvil.

## Qué incluye
- Movimiento del jugador con WASD / flechas.
- Rotación del lanzador con Q / E.
- Carga de fuerza con Space (mantener presionado) y disparo al soltar.
- Proyectiles con Rigidbody y destrucción automática.
- Cámara que sigue al jugador.
- Interfaz simple con barra de carga y texto de fuerza.
- Escena base lista para abrir en Unity.

## Requisitos
- Unity 2021.3 LTS (recomendado)
- Windows, macOS o Linux (según soporte de Unity)

## Cómo abrir el proyecto
1. Descarga este repositorio como ZIP desde GitHub.
2. Descomprime la carpeta.
3. Abre la carpeta con Unity Hub.
4. Usa Unity 2021.3 LTS o una versión compatible.
5. Si Unity te pide abrir la escena, usa la escena principal en `Assets/Scenes/MainScene.unity`.

## Controles
- Movimiento: WASD / Flechas
- Rotar lanzador: Q / E
- Cargar y disparar: Space (mantener pulsado para cargar, soltar para lanzar)
- Reiniciar escena: R

## Estructura sugerida del proyecto
- `Assets/Scenes/MainScene.unity`
- `Assets/Scripts/PlayerMovement.cs`
- `Assets/Scripts/LauncherController.cs`
- `Assets/Scripts/Projectile.cs`
- `Assets/Scripts/CameraFollow.cs`
- `Assets/Scripts/UIManager.cs`

## Como descargar el ZIP del repo
1. Abre la página del repositorio en GitHub.
2. Haz clic en el botón verde `Code`.
3. Selecciona `Download ZIP`.

## Nota
Este proyecto es un ejemplo base para empezar un simulador sencillo. Puedes ampliarlo con:
- más obstáculos
- temporizador
- score
- cámara fija / cámara 3ª persona
- lógica de daño / impacto
- soporte para joystick o puerto serial
