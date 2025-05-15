# MovliAPI

MovliAPI es una API RESTful desarrollada en .NET 9 que permite gestionar funciones y botones, así como ejecutar acciones asociadas a cada uno. Está diseñada para integraciones donde se requiera lanzar aplicaciones, abrir enlaces o generar códigos QR a partir de acciones configurables. Utiliza MySQL como base de datos.

---

## Requisitos

- .NET 9 SDK
- MySQL Server
- Visual Studio 2022 (Recomendado)

---

## Configuración inicial

1. Clona el repositorio.
2. Configura la cadena de conexión en `appsettings.json`:
    ```json
    {
        "ConnectionString": {
            "MySqlConnection": "serverlocalhost;database=movli;user=root;password=tu_contraseña"
        }
    }
    ```

3. Restaura los paquetes **NuGet** y compila la solución.
4. Asegúrate de tener las tablas necesarias en la base de datos (`./sql`).

---

## Modelos principales

### Funcion
- **Id** (int): Identificador único.
- **Nombre** (string): Nombre de la función.
- **Acción** (string): Acción asociada (ruta o comando).
- **Descripción** (string, opcional): Descripción de la función.

### Boton
- **Id** (int): Identificador único.
- **Nombre** (string): Nombre del botón.
- **Función** (string): Nombre de la función asociada.
- **Descripción** (string, opcional): Descripción del botón.

---

## Endpoints

### Funciones
- **GET /api/funciones**
  Devuelve todas las funciones.

- **GET /api/funciones/{id}**
  Devuelve una función por su ID.

- **POST /api/funciones**
  Crea una nueva función.
  **Body:**
  ```json
  {
    "nombre": "string",
    "accion": "string",
    "descripcion": "string (opcional)"
  }
  ```

- **PUT /api/funciones/{id}**
  Actualiza una función existente.
  **Body igual al POST.**

- **DELETE /api/funciones/{id}**
  Elimina una función por su ID.

- **GET /api/funciones/abrirenlace/{nombreBoton}**
  Ejecuta la acción asociada a un botón (abrir enlace).

- **GET /api/funciones/abrir/{nombreBoton}**
  Ejecuta la acción asociada a un botón (abrir aplicación).

- **GET /api/funciones/enlace/{nombreFuncion}**
  Devuelve un enlace para la función, útil para generar un QR.

### Botones
- **GET /api/botones**
  Devuelve todos los botones.

- **GET /api/botones/{id}**
  Devuelve un botón por su ID.

- **POST /api/botones**
  Crea un nuevo botón.
  **Body:**
  ```json
  {
    "nombre": "string",
    "funcion": "string",
    "descripcion": "string (opcional)"
  }
  ```

- **PUT /api/botones/{id}**
  Actualiza un botón existente.
  **Body igual al POST.**

- **DELETE /api/botones/{id}**
  Elimina un botón por su ID.

---

## Observaciones

- Los nombres de funciones y botones deben ser únicos.
- Las acciones deben ser rutas válidas o comandos ejecutables en el sistema operativo.
- La API no implementa autenticación por defecto; si es necesario, añade un middleware de autenticación.
- Puedes usar el endpoint `/api/funciones/enlace/{nombreFuncion}` para obtener un enlace listo para generar un código QR.

---

## Contribuciones

¡Las contribuciones son bienvenidas! Sideseas contribuir:

1. Haz un fork del repositorio.
2. Crea una rama para tu funcionalidad (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz un commit (`git commit -m 'Añadir nueva funcionalidad'`).
4. Envía un pull request.

---

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE`para más detalles.

---
---

**README in ENGLISH**

# MovliAPI

MovliAPI is a RESTful API developed in .NET 9 that allows managing functions and buttons, as well as executing actions associated with each. It is designed for integrations where launching applications, opening links, or generating QR codes from configurable actions is required. It uses MySQL as its database.

---

## Requirements

- .NET 9 SDK
- MySQL Server
- Visual Studio 2022 (Recommended)

---

## Initial Setup

1. Clone the repository.
2. Configure the connection string in `appsettings.json`:
  ```json
  {
    "ConnectionString": {
      "MySqlConnection": "server=localhost;database=movli;user=root;password=your_password"
    }
  }
  ```

3. Restore **NuGet** packages and build the solution.
4. Ensure the necessary tables are present in the database (`./sql`).

---

## Main Models

### Function
- **Id** (int): Unique identifier.
- **Name** (string): Name of the function.
- **Action** (string): Associated action (route or command).
- **Description** (string, optional): Description of the function.

### Button
- **Id** (int): Unique identifier.
- **Name** (string): Name of the button.
- **Function** (string): Name of the associated function.
- **Description** (string, optional): Description of the button.

---

## Endpoints

### Functions
- **GET /api/functions**
  Returns all functions.

- **GET /api/functions/{id}**
  Returns a function by its ID.

- **POST /api/functions**
  Creates a new function.
  **Body:**
  ```json
  {
    "name": "string",
    "action": "string",
    "description": "string (optional)"
  }
  ```

- **PUT /api/functions/{id}**
  Updates an existing function.
  **Body same as POST**

- **DELETE /api/functions/{id}**
  Deletes a function by its ID.

- **GET /api/functions/abrirenlace{nombreBoton}**
  Executes the action associated with a button (open link).

- **GET /api/functions/enlace{nombreFuncion}**
  Returns a link for the function, useful for generating a QR code.

### Buttons

- **GET /api/buttons**
  Returns all buttons.

- **GET /api/buttons/{id}**
  Returns a button by its ID.

- **POST /api/buttons**
  Creates a new button.
  **Body:**
  ```json
  {
    "name": "string",
    "function": "string",
    "description": "string (optional)"
  }
  ```

- **PUT /api/buttons/{id}**
  Updates an existing button.
  **Body same as POST**

- **DELETE /api/buttons/{id}**
  Deletes a button by its ID.

---

## Notes

- Function and button names must be unique.
- Actions must be valid routes or executable commands on the operating system.
- The API does not implement authentication by default; if needed, add an authentication middleware.
- You can use the endpoint `/api/functions/link/{nombreFuncion}` to get a link ready for generating a QR code.

---

## Contributions
Contributions are welcome! If you want to contribute:

1. Fork the repository.
2. Create a branch for your feature (`git checkout -b feature/new-feature`).
3. Make your changes and commit them (`git commit -m 'Add new feature'`).
4. Submit a pull request.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.