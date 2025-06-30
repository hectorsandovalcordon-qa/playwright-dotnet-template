# QA Framework - Multi-Driver Support

Framework escalable para pruebas automatizadas que soporta tanto Playwright como Selenium WebDriver.

## 🚀 Quick Start

1. **Restaurar dependencias:**
   ```bash
   dotnet restore
   ```

2. **Compilar:**
   ```bash
   dotnet build
   ```

3. **Instalar browsers de Playwright (opcional):**
   ```bash
   pwsh tests/QA.Framework.Tests/bin/Debug/net8.0/playwright.ps1 install
   ```

4. **Ejecutar tests:**
   ```bash
   dotnet test
   ```

## ⚙️ Configuración

Edita `appsettings.json` para cambiar entre drivers:

```json
{
  "TestSettings": {
    "WebDriver": "playwright",  // o "selenium"
    "Browser": "chromium"       // chromium, firefox, webkit, chrome, edge
  }
}
```

## 📊 Reportes Allure

```bash
npm install -g allure-commandline
allure serve allure-results
```

¡Disfruta del framework escalable! 🎉
