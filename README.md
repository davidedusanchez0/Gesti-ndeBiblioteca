# Gestión de Biblioteca

Hola 👋

Este proyecto lo hice como práctica de programación orientada a objetos usando `C#` y `Windows Forms`.
La idea fue construir un sistema de biblioteca **local** (sin base de datos), pero que igual permitiera manejar usuarios, libros, préstamos y estadísticas de forma completa.

## ¿Qué hace la aplicación?

Permite:

- Iniciar sesión con dos roles: `Administrador` y `Cliente`.
- Gestionar libros (crear, consultar, editar y eliminar).
- Gestionar usuarios (crear, editar y eliminar desde Admin).
- Registrar y devolver préstamos.
- Ver estadísticas con gráficos.
- Guardar la información en archivos `.txt`.

## Tecnologías que usé

- `.NET 8`
- `Windows Forms`
- `OxyPlot.WindowsForms` para los gráficos
- Persistencia local con:
  - `libros.txt`
  - `usuarios.txt`
  - `prestamos.txt`

## Funcionalidades por módulo

### 1) Login y roles

- El usuario inicia sesión con username y contraseña.
- Si el rol es `Cliente`, se ocultan pestañas y acciones de administración.
- Si el rol es `Administrador`, tiene acceso a gestión completa.

### 2) Catálogo de libros

- Alta de libros nuevos.
- Edición de libros existentes.
- Eliminación con confirmación.
- Búsqueda por título, autor o ISBN.
- Columna `CopiasEnPrestamo` visible solo para Admin.

### 3) Gestión de usuarios (Admin)

- Crear usuarios nuevos.
- Editar usuario (contraseña/rol).
- Eliminar usuario.
- Validación para no repetir usernames.

### 4) Préstamos

- Solicitar préstamo (descuenta copias).
- Devolver préstamo (devuelve copias).
- El cliente ve sus préstamos.
- El admin ve el registro global.

### 5) Estadísticas

- Top 5 libros más prestados.
- Top 5 usuarios más activos.
- Visualización con gráfico de barras y gráfico pastel.

## Estructura del proyecto

```text
Gestión de Biblioteca/
├─ Models/
│  ├─ Usuario.cs
│  ├─ Administrador.cs
│  ├─ Cliente.cs
│  ├─ Libro.cs
│  └─ Prestamo.cs
├─ Services/
│  ├─ UsuarioService.cs
│  ├─ LibroService.cs
│  └─ PrestamoService.cs
├─ Form1.cs                 # Login
├─ FormBiblioteca.cs        # Pantalla principal
└─ README.md
```

## Cómo ejecutar

1. Abrir la solución en Visual Studio.
2. Restaurar paquetes NuGet (si se solicita).
3. Compilar (`Build`).
4. Ejecutar (`F5`).

## Requisitos

- Visual Studio 2022/2026 con `.NET Desktop Development`.
- SDK `.NET 8`.

## Nota sobre archivos de datos

Como la persistencia es local, los `.txt` se generan en `bin/...`.
Si limpias la carpeta de salida, esos archivos pueden regenerarse.

## ¿Qué practiqué en este proyecto?

- POO: clases, encapsulación, herencia y polimorfismo.
- Organización por capas simples: `Models`, `Services`, `UI`.
- Estructuras de datos: listas, diccionarios y matriz bidimensional.
- Validaciones y manejo de errores.

## Autor

Proyecto desarrollado por **David**.
