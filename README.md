# 🧪 Proyecto de Pruebas Automatizadas con xUnit + Playwright (.NET)

Guía rápida para configurar un entorno de pruebas en .NET usando xUnit y Playwright.

---

## ✅ Requisitos Previos

Asegúrate de tener instalado y configurado en el `PATH`:

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download) → `dotnet --version`
- [PowerShell Core (`pwsh`)](https://learn.microsoft.com/powershell/) → `pwsh --version`

También necesitas conexión a internet para instalar los navegadores de Playwright.

---

## 🛠️ Crear el Proyecto

```bash
# Crear proyecto y solución
dotnet new xunit -n MyTests
dotnet new sln -n PlaywrightTests
dotnet sln add MyTests/MyTests.csproj

# Añadir Playwright
cd MyTests
dotnet add package Microsoft.Playwright
```

---

## 🌐 Instalar Navegadores

Antes de instalar, **compila el proyecto** para generar el script `playwright.ps1`:

```bash
dotnet build
pwsh bin/Debug/net6.0/playwright.ps1 install
```

> Reemplaza `net6.0` según tu versión de .NET.

---

## 🧪 Prueba de Ejemplo

Archivo: `MyTests/Tests.cs`

```csharp
using Microsoft.Playwright;
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

## ▶️ Ejecutar Pruebas

```bash
dotnet test
```

---

## 📝 Notas

- Usa `Chromium`, `Firefox` o `Webkit` según tus necesidades.
- Activa modo visible con `Headless = false`.
- Ejecuta pruebas en caliente con `dotnet watch test`.

---
