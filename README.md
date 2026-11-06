## 🛠️ Configuración del entorno de desarrollo

Para trabajar en este proyecto, es necesario configurar el entorno de desarrollo con las siguientes herramientas.

### 1. Instalar .NET 10 SDK

Descarga e instala el **SDK de .NET 10** desde la página oficial de Microsoft:

[Descargar .NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

Una vez instalado, comprueba que está disponible ejecutando en una terminal:

```bash
dotnet --version
```

### 2. Instalar Visual Studio Code y C# Dev Kit

Descarga Visual Studio Code desde su sitio oficial:

[Descargar VS Code](https://code.visualstudio.com/)

Abre VS Code y accede a la pestaña **Extensiones**, situada en la barra lateral izquierda (icono de cuatro cuadrados) o utiliza el atajo `Ctrl + Shift + X`. Busca **C# Dev Kit**, selecciona la extensión oficial de Microsoft y pulsa **Instalar**.

### 3. Restaurar las dependencias del proyecto

Una vez clonado el repositorio, abre una terminal desde la carpeta que contiene el archivo `.csproj` y ejecuta:

```bash
dotnet restore
```

Este comando instalará de forma automática las dependencias declaradas en el proyecto, incluida **Raylib-cs 8.1.0**. No es necesario ejecutar `dotnet add package raylib-cs`.

### 4. Ejecutar el proyecto

Desde la misma carpeta en la terminal, ejecuta:

```bash
dotnet run
```

Esto compilará y ejecutará el videojuego de inmediato.