# Librería API

API REST para la gestión de una librería: autores, libros, préstamos y autenticación basada en JWT.

## Stack técnico

- **.NET 10** con Minimal APIs
- **Entity Framework Core** sobre SQL Server LocalDB
- **ASP.NET Core Identity** con roles
- **Autenticación JWT** (Bearer)
- **Swagger / OpenAPI**

## Configuración y ejecución

### Requisitos

- SDK de .NET 10
- SQL Server LocalDB (incluido con Visual Studio)


**Puede cambiar la cadena de conexion a una tradicional de SQLSERVER **

la cual se Encuentra en LibreriaWepApi/appsetting.json

"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibreriaDB;Trusted_Connection=True;MultipleActiveResultSets=True"

### Pasos

1. Restaurar paquetes NuGet:

```bash
dotnet restore
```

2. Aplicar las migraciones a la base de datos:

```bash
dotnet ef database update --project Data --startup-project LibreriaWebApi
```

3. Ejecutar la API:

```bash
dotnet run --project LibreriaWebApi
```

Al iniciarse, la aplicación crea automáticamente los roles y el usuario administrador (ver [Credenciales de prueba](#credenciales-de-prueba)).

### URLs base

| Perfil | URL |
| --- | --- |
| HTTP | `http://localhost:5288` |
| HTTPS | `https://localhost:7087` |

### Swagger UI

En entorno de desarrollo, la documentación interactiva está disponible en:

```
http://localhost:5288/swagger
```

### Credenciales de prueba

Al arrancar, se crea un usuario administrador:

Usuario :`admin`
Clave :`!Admin123` 


## Autenticación

La API usa tokens **JWT**. Los endpoints protegidos requieren el encabezado:

```
Authorization: Bearer <token>
```

Para obtener un token:

```
POST /api/auth/login
```

```json
{
  "userName": "admin",
  "password": "!Admin123"
}
```

Respuesta:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2026-08-01T18:00:00Z"
}
```
Puede generar una JWT con la siguiente web
https://randomkeygen.com/jwt-secret


### Roles

Rol : `Admin` | Descripción | Acceso a todos los endpoints, incluyendo registro de usuarios y creación/devolución/eliminación de préstamos.
Rol : `Usuario`Descripción | Acceso de lectura y gestión de autores y libros, y gestión de préstamos (excepto devolver y eliminar). 


La configuración JWT (llave, emisor, audiencia y expiración) se encuentra en `LibreriaWebApi/appsettings.json`:

| Configuración      | Valor por defecto |
| ---                | ---               |
| `Jwt:Issuer`       | `LibreriaApi`     |
| `Jwt:Audience`     | `LibreriaApi`     |
| `Jwt:ExpireMinutes`| `60`              |

## Endpoints

### Autenticación — `api/auth`

| Método | Ruta                 | Rol requerido | Descripción                        |
| ---    | ---                  | ---           | ---                                |
| `POST` | `/api/auth/register` | `Admin`       | Registrar un nuevo usuario         |
| `POST` | `/api/auth/login`    | Público       | Iniciar sesión y obtener token JWT |

#### `POST /api/auth/register`

Cuerpo:

```json
{
  "nombre": "Juan Pérez",
  "userName": "juanperez",
  "password": "ClaveSegura123"
}
```

Respuestas:

- `200 OK` — Usuario creado correctamente:

```json
{
  "message": "Usuario creado correctamente"
}
```

- `400 Bad Request` — Error al crear el usuario:

```json
{
  "error": "El mensaje del error"
}
```

- `401 Unauthorized` — Token ausente, inválido o sin rol `Admin`.

#### `POST /api/auth/login`

Cuerpo:

```json
{
  "userName": "admin",
  "password": "!Admin123"
}
```

Respuestas:

- `200 OK` — Token JWT y fecha de expiración.
- `401 Unauthorized` — Credenciales inválidas.

### Autores — `api/autores`

Todos los endpoints requieren rol `Usuario` o `Admin`.

| Método   | Ruta                | Descripción                   |
| ---      | ---                 | ---                           |
| `GET`    | `/api/autores/{id}` | Obtener un autor por su Id    |
| `GET`    | `/api/autores/`     | Obtener todos los autores     |
| `POST`   | `/api/autores/`     | Crear un nuevo autor          |
| `PUT`    | `/api/autores/{id}` | Actualizar un autor existente |
| `DELETE` | `/api/autores/{id}` | Eliminar un autor existente   |

#### `GET /api/autores/{id}`

Respuesta `200 OK`:

```json
{
  "autor_id": 1,
  "nombre": "Gabriel García Márquez",
  "nacionalidad": "Colombiana"
}
```

- `404 Not Found` — No existe autor con ese Id:

```json
{
  "error": "No se encontro un autor con el Id: 99"
}
```

#### `GET /api/autores/`

Respuesta `200 OK`:

```json
[
  {
    "autor_id": 1,
    "nombre": "Gabriel García Márquez",
    "nacionalidad": "Colombiana"
  }
]
```

#### `POST /api/autores/`

Cuerpo:

```json
{
  "nombre": "Isabel Allende",
  "nacionalidad": "Chilena"
}
```

Respuesta `201 Created`:

```json
{
  "autor_id": 2,
  "nombre": "Isabel Allende",
  "nacionalidad": "Chilena"
}
```

- `400 Bad Request` — Datos inválidos (ver [Validaciones](#autores)).
- `500 Internal Server Error` — Error interno.

#### `PUT /api/autores/{id}`

Cuerpo:

```json
{
  "nombre": "Isabel Allende",
  "nacionalidad": "Estadounidense"
}
```

Respuestas: `200 OK` con el autor actualizado, `400 Bad Request`, `404 Not Found`, `500 Internal Server Error`.

#### `DELETE /api/autores/{id}`

Respuestas:

- `204 No Content` — Autor eliminado.
- `404 Not Found` — No existe el autor.
- `500 Internal Server Error`.

### Libros — `api/libros`

| Método   | Ruta                        | Rol requerido      | Descripción                             |
| ---      | ---                         | ---                | ---                                     |
| `GET`    | `/api/libros/{id}`          | `Usuario`, `Admin` | Obtener un libro por su Id              |
| `GET`    | `/api/libros/`              | `Usuario`, `Admin` | Obtener todos los libros                |
| `GET`    | `/api/libros/antes-de-2000` | `Usuario`, `Admin` | Obtener libros publicados antes de 2000 |
| `POST`   | `/api/libros/`              | `Admin`            | Crear un nuevo libro                    |
| `PUT`    | `/api/libros/{id}`          | `Usuario`, `Admin` | Actualizar un libro existente           |
| `DELETE` | `/api/libros/{id}`          | `Usuario`, `Admin` | Eliminar un libro existente             |

#### `GET /api/libros/{id}`

Respuesta `200 OK`:

```json
{
  "libro_id": 1,
  "titulo": "Cien años de soledad",
  "autor_id": 1,
  "ano_publicacion": "1967-01-01T00:00:00",
  "genero": "Realismo"
}
```

- `404 Not Found` — No existe libro con ese Id.

#### `GET /api/libros/`

Respuesta `200 OK`:

```json
[
  {
    "libro_id": 1,
    "titulo": "Cien años de soledad",
    "autor_id": 1,
    "ano_publicacion": "1967-01-01T00:00:00",
    "genero": "Realismo"
  }
]
```

#### `GET /api/libros/antes-de-2000`

Respuesta `200 OK`:

```json
[
  {
    "libro_id": 1,
    "titulo": "Cien años de soledad",
    "ano_publicacion": "1967-01-01T00:00:00"
  }
]
```

#### `POST /api/libros/` (solo `Admin`)

Cuerpo:

```json
{
  "titulo": "La casa de los espíritus",
  "autor_id": 2,
  "ano_publicacion": "1982-01-01T00:00:00",
  "genero": "Realismo"
}
```

Respuesta `201 Created`:

```json
{
  "libro_id": 2,
  "titulo": "La casa de los espíritus",
  "autor_id": 2,
  "ano_publicacion": "1982-01-01T00:00:00",
  "genero": "Realismo"
}
```

- `400 Bad Request` — Datos inválidos (ver [Validaciones](#libros)).
- `401 Unauthorized` — Requiere rol `Admin`.
- `500 Internal Server Error`.

#### `PUT /api/libros/{id}`

Cuerpo:

```json
{
  "titulo": "La casa de los espíritus",
  "autor_id": 2,
  "ano_publicacion": "1982-01-01T00:00:00",
  "genero": "Novela"
}
```

Respuestas: `200 OK` con el libro actualizado, `400 Bad Request`, `404 Not Found`, `500 Internal Server Error`.

#### `DELETE /api/libros/{id}`

Respuestas: `204 No Content`, `404 Not Found`, `500 Internal Server Error`.

### Préstamos — `api/prestamos`

| Método   | Ruta                          | Rol requerido      | Descripción                              |
| ---      | ---                           | ---                | ---                                      |
| `GET`    | `/api/prestamos/{id}`         | `Usuario`, `Admin` | Obtener un préstamo por su Id            |
| `GET`    | `/api/prestamos/`             | `Usuario`, `Admin` | Obtener todos los préstamos              |
| `GET`    | `/api/prestamos/no-devueltos` | `Usuario`, `Admin` | Obtener todos los préstamos no devueltos |
| `POST`   | `/api/prestamos/`             | `Usuario`, `Admin` | Crear un nuevo préstamo                  |
| `PUT`    | `/api/prestamos/{id}`         | `Admin`            | Devolver un préstamo                     |
| `DELETE` | `/api/prestamos/{id}`         | `Admin`            | Eliminar un préstamo existente           |

#### `GET /api/prestamos/{id}`

Respuesta `200 OK`:

```json
{
  "prestamo_id": 1,
  "libro_id": 1,
  "fecha_prestamo": "2026-07-15T00:00:00",
  "fecha_devolucion": null
}
```

- `404 Not Found` — No existe préstamo con ese Id.

#### `GET /api/prestamos/`

Respuesta `200 OK`:

```json
[
  {
    "prestamo_id": 1,
    "libro_id": 1,
    "fecha_prestamo": "2026-07-15T00:00:00",
    "fecha_devolucion": null
  }
]
```

#### `GET /api/prestamos/no-devueltos`

Devuelve el nombre del autor y el título de cada libro prestado sin devolver.

Respuesta `200 OK`:

```json
[
  {
    "nombre": "Gabriel García Márquez",
    "titulo": "Cien años de soledad"
  }
]
```

#### `POST /api/prestamos/`

Cuerpo (`fecha_prestamo` es opcional; si se omite, se usa la fecha actual UTC-4):

```json
{
  "libro_id": 1,
  "fecha_prestamo": "2026-07-15T00:00:00"
}
```

Respuesta `201 Created`:

```json
{
  "prestamo_id": 1,
  "libro_id": 1,
  "fecha_prestamo": "2026-07-15T00:00:00",
  "fecha_devolucion": null
}
```

- `400 Bad Request` — Datos inválidos (ver [Validaciones](#prestamos)).
- `500 Internal Server Error`.

#### `PUT /api/prestamos/{id}` (solo `Admin`)

Registra la devolución de un préstamo. `fecha_devolucion` es opcional; si se omite, se usa la fecha actual UTC-4.

Cuerpo:

```json
{
  "fecha_devolucion": "2026-07-30T00:00:00"
}
```

Respuesta `200 OK`:

```json
{
  "prestamo_id": 1,
  "libro_id": 1,
  "fecha_prestamo": "2026-07-15T00:00:00",
  "fecha_devolucion": "2026-07-30T00:00:00"
}
```

- `400 Bad Request` — El préstamo ya fue devuelto o la fecha es anterior a la de préstamo.
- `401 Unauthorized` — Requiere rol `Admin`.
- `404 Not Found` — No existe el préstamo.

#### `DELETE /api/prestamos/{id}` (solo `Admin`)

Respuestas: `204 No Content`, `404 Not Found`, `500 Internal Server Error`.

## Modelos de datos

### Autores

| Campo          | Tipo     | Descripción                           |
| ---            | ---      | ---                                   |
| `autor_id`     | `int`    | Identificador único (autoincremental) |
| `nombre`       | `string` | Nombre del autor                      |
| `nacionalidad` | `string` | Nacionalidad del autor                |

### Libros

| Campo             | Tipo       | Descripción                           |
| ---               | ---        | ---                                   |
| `libro_id`        | `int`      | Identificador único (autoincremental) |
| `titulo`          | `string`   | Título del libro                      |
| `autor_id`        | `int`      | Id del autor del libro                |
| `ano_publicacion` | `DateTime` | Fecha de publicación                  |
| `genero`          | `string`   | Género del libro                      |

### Préstamos

| Campo              | Tipo        | Descripción                                        |
| ---                | ---         | ---                                                |
| `prestamo_id`      | `int`       | Identificador único (autoincremental)              |
| `libro_id`         | `int`       | Id del libro prestado                              |
| `fecha_prestamo`   | `DateTime`  | Fecha en que se realizó el préstamo                |
| `fecha_devolucion` | `DateTime?` | Fecha de devolución (nulo mientras no se devuelva) |

## Validaciones

### Autores

- `nombre`: obligatorio, entre 2 y 50 caracteres.
- `nacionalidad`: obligatorio, entre 2 y 50 caracteres.

### Libros

- `titulo`: obligatorio, entre 2 y 80 caracteres.
- `genero`: obligatorio, entre 2 y 15 caracteres.
- `ano_publicacion`: no puede ser posterior a la fecha actual.
- `autor_id`: debe ser mayor que 0.

### Préstamos

- `libro_id`: debe ser mayor que 0.
- `fecha_devolucion`: no puede ser anterior a `fecha_prestamo`.
- No se puede registrar la devolución de un préstamo ya devuelto.

## Códigos de estado

| Código | Significado |
| --- | --- |
| `200 OK` | Operación exitosa. |
| `201 Created` | Recurso creado correctamente. |
| `204 No Content` | Recurso eliminado correctamente. |
| `400 Bad Request` | Datos de entrada inválidos. Formato de error: `{ "error": "mensaje" }`. |
| `401 Unauthorized` | Falta el token, es inválido o el rol no tiene permiso. |
| `404 Not Found` | El recurso solicitado no existe. Formato de error: `{ "error": "mensaje" }`. |
| `500 Internal Server Error` | Error interno del servidor. |

## Ejemplo de flujo completo

1. Obtener token como administrador:

```bash
POST /api/auth/login
```

```json
{
  "userName": "admin",
  "password": "!Admin123"
}
```

2. Usar el token en el resto de peticiones:

```
Authorization: Bearer <token>
```

3. Crear un autor:

```json
POST /api/autores/
{
  "nombre": "Julio Cortázar",
  "nacionalidad": "Argentina"
}
```

4. Crear un libro (requiere rol `Admin`):

```json
POST /api/libros/
{
  "titulo": "Rayuela",
  "autor_id": 3,
  "ano_publicacion": "1963-01-01T00:00:00",
  "genero": "Novela"
}
```

5. Crear un préstamo:

```json
POST /api/prestamos/
{
  "libro_id": 3
}
```

6. Consultar préstamos no devueltos:

```bash
GET /api/prestamos/no-devueltos
```

7. Devolver el préstamo (requiere rol `Admin`):

```json
PUT /api/prestamos/1
{
  "fecha_devolucion": "2026-08-01T00:00:00"
}
```
