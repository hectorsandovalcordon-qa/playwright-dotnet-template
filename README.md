# Playwright .NET Template con xUnit
Plantilla base para proyectos de automatización de tests usando Playwright, .NET, xUnit y Page Object Model. Incluye integración con métricas de tests, paralelismo, fixtures, y CI con GitHub Actions.
---
## Contenido
- Estructura sólida basada en **Page Object Model (POM)**
- Uso de **xUnit** para pruebas con soporte a **fixtures** y paralelismo
- Métricas de ejecución de tests (duración, resultado, categoría)
- Pipeline de Integración Continua con GitHub Actions (YAML incluido)
- Ejemplo básico de tests para login y navegación
- Configuración para facilitar escalabilidad y mantenimiento
---
## Cómo empezar
### Clonar el repositorio
```bash
git clone https://github.com/hectorsandovalcordon-qa/playwright-dotnet-template.git
cd playwright-dotnet-template
```
### Ejecutar los tests desde línea de comandos
```bash
dotnet test
```
O con configuración específica:
```bash
dotnet test --logger "trx" --results-directory ./TestResults
```
### Estructura del proyecto
- `/Tests` - Código de tests con xUnit  
- `/PageObjects` - Modelos de página siguiendo POM  
- `/Fixtures` - Configuraciones comunes para tests  
- `/Infraestructure` - Código para métricas, logs, helpers  
- `/playwright.config.ts` - Configuración Playwright  
---
## Integración Continua
Se incluye un workflow de GitHub Actions para ejecutar tests automáticamente en PRs hacia `dev` y `main`. Puedes encontrar el archivo en:
```yaml
.github/workflows/playwright-tests.yml
```
---
## Métricas de Tests
Cada test registra métricas de ejecución (tiempo, resultado, categoría) que se almacenan en:
```json
Metrics/metrics.json
```
---
## Personalización
- Añade nuevos fixtures para compartir estado  
- Crea nuevos page objects para tus nuevas páginas  
- Extiende la métrica con nuevos campos si es necesario  
- Modifica el workflow para agregar notificaciones, reportes, etc.  
---
## Futuras mejoras
- Integración con reportes HTML (ejemplo con ReportPortal o Allure)  
- Captura automática de screenshots en fallos  
- Tests parametrizados con datos externos  
---
## Contacto
Creador: Héctor Sandoval  
Repositorio: https://github.com/hectorsandovalcordon-qa/playwright-dotnet-template  
LinkedIn: https://www.linkedin.com/in/hectorsandovalcordon  
---
¡Gracias por usar esta plantilla! Si quieres aportar mejoras, abre un pull request o issue.
