# 🧪 Proyecto de Pruebas Automatizadas con xUnit + Playwright (.NET)

Esta guía explica cómo crear un proyecto de automatización desde cero utilizando [xUnit](https://xunit.net/) como framework de pruebas y [Playwright para .NET](https://playwright.dev/dotnet/) como motor de automatización de navegadores.

---

## ✅ Requisitos Previos

Antes de comenzar, asegúrate de tener instalados y configurados correctamente los siguientes componentes:

### 1. [.NET SDK 6.0 o superior](https://dotnet.microsoft.com/download)

Verifica la instalación:
```bash
dotnet --version
```

Asegúrate de que `dotnet` esté en tu variable de entorno `PATH`.

---

### 2. [PowerShell Core (`pwsh`)](https://learn.microsoft.com/powershell/scripting/install/installing-powershell)

Playwright requiere PowerShell para instalar los navegadores.

Verifica la instalación:
```bash
pwsh --version
```

Asegúrate de que `pwsh` esté en tu variable de entorno `PATH`.

---

## 🛠️ Crear Proyecto de Pruebas con xUnit y Playwright

### 1. Crear solución y proyecto xUnit

```bash
dotnet new sln -n PlaywrightTests
dotnet new xunit -n MyTests
dotnet sln add MyTests/MyTests.csproj
```

---

### 2. Añadir Playwright al proyecto

```bash
cd MyTests
dotnet add package Microsoft.Playwright
```

---

### 3. Instalar navegadores con Playwright

```bash
playwright install
```

> ⚠️ Este comando ejecuta internamente un script de PowerShell. Asegúrate de tener `pwsh` disponible o el comando fallará.

---

## 🧪 Crear una prueba de ejemplo

Crea un archivo `Tests.cs` dentro del proyecto `MyTests/` con el siguiente contenido:

```csharp
using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

public class ExampleTests
{
    [Fact]
    public async Task OpenGoogleTest()
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://www.google.com");
        var title = await page.TitleAsync();
        Assert.Contains("Google", title);
        await browser.CloseAsync();
    }
}
```

---

## ▶️ Ejecutar las pruebas

Desde la raíz del proyecto (donde esté la solución `.sln`):

```bash
dotnet test
```

---

## 📁 Estructura del Proyecto

```bash
PlaywrightTests/
├── MyTests/
│   ├── MyTests.csproj
│   ├── Tests.cs
├── PlaywrightTests.sln
├── README.md
```

---

## 📌 Notas

- Playwright soporta múltiples navegadores: Chromium, Firefox y WebKit. Puedes cambiar `playwright.Chromium` por `playwright.Firefox` o `playwright.Webkit`.
- Puedes configurar el navegador para modo visible (`Headless = false`) durante el desarrollo.
- Puedes usar `dotnet watch test` para pruebas automáticas al guardar cambios.

---
