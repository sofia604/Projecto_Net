# 🎬 Proyecto: Sitio Web de Películas (ASP.NET Core MVC + EF Core)

Este proyecto permite gestionar un catálogo de películas, actores, directores, géneros y comentarios usando **ASP.NET Core MVC** y **Entity Framework Core**.

---

## 📌 Descripción general

El sistema ofrece funcionalidades CRUD (Crear, Leer, Actualizar, Eliminar) sobre todas las entidades del modelo, incluyendo películas, actores, directores, géneros y comentarios.  
Se implementaron relaciones uno-a-muchos y muchos-a-muchos con entidades intermedias, así como validaciones tanto del lado del servidor como del cliente.  
Se utilizaron vistas parciales, formularios protegidos con antiforgery tokens, y estilos con Bootstrap 5.

---

## 🛠️ Tecnologías utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (LocalDB)
- Bootstrap 5
- jQuery para validaciones del cliente
- Vistas parciales y modales con AJAX

---

## ✅ Funcionalidades

- Registrar, editar, eliminar y visualizar películas
- Subida de imagen para portada de películas
- Filtros por nombre y género
- Asociar actores a películas (relación muchos a muchos)
- Comentarios de usuarios sobre películas
- Paginación de listados
- CRUD completo para actores, directores y géneros

---

## 🚀 Instrucciones para correr el proyecto

1. Clona el repositorio:  
   `https://github.com/sofia604/Projecto_Net.git`

2. Abre la solución en **Visual Studio 2022**

3. Revisa la cadena de conexión en `appsettings.json`

4. Abre la Consola del Administrador de Paquetes y ejecuta:
   ```powershell
   Add-Migration Inicial
   Update-Database
