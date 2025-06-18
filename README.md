# Plantilla Playwright xUnit para .NET

Esta plantilla te permite crear proyectos de automatización de tests usando Playwright y xUnit con una estructura robusta, métricas, paralelismo y configuración para integración continua.

---

## Cómo usar esta plantilla

### 1. Clonar el repositorio de la plantilla

```bash
git clone https://github.com/hectorsandovalcordon-qa/playwright-dotnet-template.git
cd playwright-dotnet-template
```

### 2. Instalar la plantilla localmente

Desde la raíz del repositorio clonado, ejecuta:

```bash
dotnet new -i .
```

Esto instalará la plantilla en tu máquina local.

### 3. Crear un nuevo proyecto usando la plantilla

En la carpeta donde quieras crear tu nuevo proyecto, ejecuta:

```bash
dotnet new playwright-xunit -n MiProyectoTests
cd MiProyectoTests
```

Esto generará un nuevo proyecto basado en la plantilla.

### 4. Ejecutar los tests

Dentro de la carpeta del proyecto generado, ejecuta:

```bash
dotnet test
```

---

## Estructura del proyecto generado

## Estructura del proyecto

- `.vscode/`  
  Configuraciones específicas para Visual Studio Code (opcional).

- `/Infrastructure/`  
  Utilidades para logging, métricas y otras herramientas de soporte.

- `/Pages/`  
  Modelos de página siguiendo el patrón Page Object Model.

- `/Properties/`  
  Archivos de configuración y metadatos del proyecto.

- `/Tests/`  
  Tests automatizados con xUnit.

- `/Utils/`  
  Utilidades y helpers varios para el proyecto.

---

## Integración Continua

Se incluye un ejemplo de workflow para GitHub Actions que ejecuta los tests en cada pull request a las ramas `dev` o `main`.

Archivo: `.github/workflows/ci.yml`

---

## Contacto

Héctor Sandoval  
Repositorio: https://github.com/hectorsandovalcordon-qa/playwright-dotnet-template  
LinkedIn: https://www.linkedin.com/in/hectorsandovalcordon  

---

¡Disfruta automatizando con esta plantilla! Si tienes dudas o sugerencias, abre un issue o PR en el repositorio.
