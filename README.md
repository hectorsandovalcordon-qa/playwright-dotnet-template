# 🎯 Playwright xUnit Template

Plantilla reutilizable de pruebas automatizadas con:
- [x] Playwright para testing E2E
- [x] xUnit como framework de pruebas
- [x] Page Object Model (POM)
- [x] Fixtures compartidos
- [x] Paralelismo
- [x] Métricas automáticas por test
- [x] Integración continua (CI) vía YAML

---

## 🧪 Clonar el repositorio

```bash
git clone https://github.com/<TU-USUARIO>/<NOMBRE-DEL-REPO>.git
cd <NOMBRE-DEL-REPO>
```

---

## 📦 Instalar la plantilla localmente

```bash
dotnet new install .
```

> Esto registra la plantilla en tu sistema usando el archivo `.template.config/template.json`.

---

## 🚀 Crear un nuevo proyecto basado en la plantilla

```bash
dotnet new playwright-xunit -n MiProyectoDeTests
cd MiProyectoDeTests
```

---

## 🧬 Estructura generada

```plaintext
MiProyectoDeTests/
├── MiProyectoDeTests.sln
├── MiProyectoDeTests/
│   ├── MiProyectoDeTests.csproj
│   ├── Tests/
│   ├── Pages/
│   ├── Fixtures/
│   └── Utils/
```

---

## ▶️ Ejecutar los tests por línea de comandos

```bash
dotnet test
```

---

## 🐛 Ejecutar un test individual (modo debug en VS Code)

1. Abre el archivo del test.
2. Añade un breakpoint.
3. Usa la paleta de comandos `Ctrl+Shift+P` → `Debug Test`.
4. O configura en `launch.json` un perfil para `xUnit`.

---

## 🧹 Desinstalar la plantilla (opcional)

```bash
dotnet new uninstall Playwright.Template.CSharp
```

---

## 📌 Autor

Creado por **Héctor Sandoval**  
📎 Proyecto orientado a facilitar el desarrollo de pruebas E2E reutilizables y mantenibles.
