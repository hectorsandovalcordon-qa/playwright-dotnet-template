# Plantilla de Proyecto Playwright en .NET C#

Esta plantilla proporciona una guía paso a paso para crear un proyecto de pruebas automatizadas con Playwright en .NET C# desde la línea de comandos.

---

## **Requisitos previos**

Antes de comenzar, asegúrate de tener instalados y configurados los siguientes programas:

1. **.NET SDK**  
   Descárgalo desde [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).  
   **Nota**: Asegúrate de que la ruta de instalación de .NET esté añadida a las variables de entorno del sistema:
   - Ruta predeterminada:  
     ```plaintext
     C:\Program Files\dotnet\
     ```
   - Para añadirla:
     - Ve a **"Configuración avanzada del sistema"** > **"Variables de entorno"**.
     - Busca la variable `Path` y haz clic en **Editar**.
     - Añade la ruta `C:\Program Files\dotnet\` si no está ya presente.
     - Haz clic en **Aceptar** para guardar los cambios.

2. **PowerShell Core (pwsh)**  
   Descárgalo desde [https://github.com/PowerShell/PowerShell/releases](https://github.com/PowerShell/PowerShell/releases).  
   **Nota**: Asegúrate de que la ruta de instalación de PowerShell Core esté añadida a las variables de entorno del sistema:
   - Ruta predeterminada:  
     ```plaintext
     C:\Program Files\PowerShell\7\
     ```
   - Sigue el mismo proceso que para .NET para añadir esta ruta al `Path`.

3. **Git**  
   Descárgalo desde [https://git-scm.com/](https://git-scm.com/).

4. **Configurar Git**  
   Configura tu nombre de usuario y correo electrónico para Git:
   ```bash
   git config --global user.name "Tu Nombre"
   git config --global user.email "tuemail@example.com"

## **Requisitos previos**
1. **Crear un nuevo proyecto de pruebas**
   Ejecuta el siguiente comando para crear un proyecto de pruebas con xUnit:
