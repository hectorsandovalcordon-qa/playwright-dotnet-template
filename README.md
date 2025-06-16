# Plantilla de Proyecto Playwright en .NET C#

Esta plantilla proporciona una guía paso a paso para crear un proyecto de pruebas automatizadas con Playwright en .NET C# desde la línea de comandos.

---

## **Requisitos previos**

Antes de comenzar, asegúrate de tener instalados los siguientes programas:

1. **.NET SDK**  
   Descárgalo desde [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

2. **PowerShell Core (pwsh)**  
   Descárgalo desde [https://github.com/PowerShell/PowerShell/releases](https://github.com/PowerShell/PowerShell/releases).

3. **Git**  
   Descárgalo desde [https://git-scm.com/](https://git-scm.com/).

---

## **Pasos para crear el proyecto**

### 1. Crear un nuevo proyecto de pruebas
Ejecuta el siguiente comando para crear un proyecto de pruebas con xUnit:
```bash
dotnet new xunit -n PlaywrightTests
cd PlaywrightTests

