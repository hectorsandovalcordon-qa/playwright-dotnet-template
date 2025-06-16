# 🧪 Plantilla xUnit + Playwright para .NET

Este repositorio es una plantilla base para crear proyectos de pruebas automáticas usando **xUnit** y **Playwright** en .NET. Puedes clonarla, usarla como plantilla en GitHub o crear un proyecto desde cero con los mismos pasos.

---

## ✅ Requisitos Previos

Para usar esta plantilla necesitas tener los siguientes componentes instalados **y correctamente configurados en las variables de entorno**.

---

### 1. [.NET SDK 6.0 o superior](https://dotnet.microsoft.com/download)

- Verifica que esté instalado ejecutando:
  ```bash
  dotnet --version
  ```

- Asegúrate de que el SDK esté en el `PATH`:

  #### ➤ Windows:
  Agrega esta ruta en las variables de entorno del sistema:
  ```
  C:\Program Files\dotnet\
  ```

  #### ➤ Linux/macOS:
  Añade esta línea en tu archivo `.bashrc`, `.zshrc` o similar:
  ```bash
  export PATH=$PATH:/usr/share/dotnet
  ```

---

### 2. [PowerShell Core (`pwsh`)](https://learn.microsoft.com/powershell/scripting/install/installing-powershell)

Playwright usa PowerShell para instalar los navegadores, por lo que `pwsh` debe estar disponible.

- Verifica la instalación:
  ```bash
  pwsh --version
  ```

- Asegúrate de que esté en el `PATH`:

  #### ➤ Windows:
  Normalmente se encuentra en:
  ```
  C:\Program Files\PowerShell\7\
  ```

  #### ➤ Linux/macOS:
  Añade en tu archivo `.bashrc`, `.zshrc`, etc.:
  ```bash
  export PATH=$PATH:/opt/microsoft/powershell/7
  ```

---

### 3. Acceso a Internet

Requerido para que `playwright install` pueda descargar los navegadores:

```bash
playwright install
```

> ⚠️ Si no tienes `dotnet` o `pwsh` correctamente configurados en el `PATH`, es probable que encuentres errores al ejecutar los comandos.

---

## 🚀 Cómo usar esta plantilla

### Opción 1: Usar como plantilla en GitHub

1. Haz clic en `Use this template` en la parte superior del repositorio.
2. Clona tu nuevo repositorio:
   ```bash
   git clone https://github.com/tu-usuario/tu-repo.git
   cd tu-repo
   ```

---

### Opción 2: Crear el proyecto manualmente desde cero

```bash
dotnet new sln -n PlaywrightTests
dotnet new xunit -n MyTests
dotnet sln add MyTests/MyTests.csproj
cd MyTests
dotnet add package Microsoft.Playwright
playwright install
```

---

## 📄 Ejemplo de prueba básica

Archivo: `MyTests/Tests.cs`

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

## ▶️ Ejecutar pruebas

Desde la raíz del proyecto:

```bash
dotnet test
```

---

## 📦 Estructura del proyecto

```bash
MyPlaywrightXunitTemplate/
├── MyTests/
│   ├── Tests.cs
│   ├── MyTests.csproj
├── .gitignore
├── README.md
├── PlaywrightTests.sln
```

---

## 📌 Notas

- Puedes modificar la prueba base para adaptarla a tus necesidades.
- También puedes integrar otros frameworks como NUnit o MSTest si lo deseas.
- Esta plantilla usa Chromium en modo headless por defecto.

---
