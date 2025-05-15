# React Native Expo StreamDeck

Este proyecto es una aplicación desarrollada con **React Native**. La aplicación incluye funcionalidades como generación de códigos QR (escanéo en desarrollo), un reloj, y un diseño adaptable a modo oscuro.

## Tabla de Contenidos

- [Características](#características)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Ejecución](#ejecución)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Estilos](#estilos)
- [API](#api)
- [Modo Oscuro](#modo-oscuro)
- [Contribuciones](#contribuciones)
- [Licencia](#licencia)

---

## Características

- **Generación de códigos QR**: Genera códigos QR dinámicos basados en datos obtenidos de una API.
- **Escaneo de códigos QR (Desarrollo)**: Usa la cámara del dispositivo para escanear códigos QR.
- **Reloj en tiempo real**: Un reloj digital centrado en la pantalla.
- **Modo oscuro**: Cambia el diseño de la aplicación a modo oscuro.
- **Diseño modular**: Los estilos están centralizados en un archivo externo (`styles.ts`).

---

## Requisitos Previos

Antes de comenzar, asegúrate de tener instalado lo siguiente:

- **Node.js** (version 14 o superior)
- **npm** o **yarn**
- **Expo CLI** (para ejecutar la aplicación)
- Un emulador de Android/iOS o un dispositivo físico con la app de Expo instalada.

---

## Instalación

1. Clona este repositorio en tu máquina local:

```bash
git clone https://github.com/5PIN0/streamdeck.git
cd streamdeck

2. Instala las dependencias del proyecto:

```bash
npm install
# o
yarn install
```

---

## Ejecución

1. Inicia el servidor de desarrollo de Expo:

```bash
npm start
# o 
yarn start
```

2. Escanea el código QR que aparece en la terminal o en el navegador de la app de Expo en tu dispositivo físico, o selecciona un emulador para ejecutarlo.

---

## Estructura del Proyecto

```plaintext
c:\ReactNative\streamdeck\
├── App.tsx               # Archivo principal de la aplicación
├── styles.ts             # Archivo de estilos centralizados
├── PanelEnvio.tsx        # Componente para el panel de envío
├── node_modules/         # Dependencias del proyecto
├── package.json          # Configuración del proyecto y dependencias
└── README.md             # Documentación del proyecto
```

---

## Estilos

Los estilos están centralizados en el archivo `styles.ts`. Esto permite mantener el código limpio y modular. Ejemplo de un estilO:

```typescript
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F4F7FA',
  },
  reloj: {
    fontSize: 32,
    textAlign: 'center',
    fontFamily: 'Courier New',
    color: '#333F50',
    backgroundColor: '#B0C4DE',
    paddingVertical: 10,
    paddingHorizontal: 15,
    borderRadius: 8,
  },
});
```

Para usar los estilos en un componente:

```tsx
import styles from './styles';

<View style={styles.container}>
    <Text style{styles.reloj}>11:19 PM</Text>
</View>
```

## API

La aplicación interactúa con una API para generar enlaces y datos dinámicos. Asegúrate de que la API esté disponible en la red local.

### Endpoints utilizados:

1. **Generar enlace**:
    - URL: `http://172.16.10.21:7115/api/Funciones/enlace/{parametro}`
    - Método: `GET`
    - Respuesta: Devuelve un enlace que se convierte en un código QR.

2. **Abrir enlace**:
    - URL: `http://172.16.10.21:7115/api/Funciones/abrirenlace/{id}`
    - Método: `GET`
    - Respuesta: Realiza una acción basada en el ID proporcionado.

---

## Modo oscuro

El modo oscuro se activa dinámicamente cambiando el estado de la aplicación. Los estilos oscuros estan definidos en `styles.ts`y se aplican condicionalmente.

Ejemplo:

```tsx
<View style={[styles.container, modo === 'oscuro' && styles.containerOscuro]}>
    <Text style={[styles.titulo, modo === 'oscuro' && styles.tituloOscuro]}>
    Bienvenido
    </Text>
</View>
```

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


**README IN ENGLISH**

# React Native Expo StreamDeck

This project is an application developed with **React Native**. The app includes features such as QR code generation (scanning in development), a clock, and a design adaptable to dark mode.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the App](#running-the-app)
- [Project Structure](#project-structure)
- [Styles](#styles)
- [API](#api)
- [Dark Mode](#dark-mode)
- [Contributions](#contributions)
- [License](#license)

---

## Features

- **QR Code Generation**: Dynamically generates QR codes based on data retrieved from an API.
- **QR Code Scanning (In Development)**: Uses the device's camera to scan QR codes.
- **Real-Time Clock**: A digital clock centered on the screen.
- **Dark Mode**: Switches the app's design to dark mode.
- **Modular Design**: Styles are centralized in an external file (`styles.ts`).

---

## Prerequisites

Before starting, make sure you have the following installed:

- **Node.js** (version 14 or higher)
- **npm** or **yarn**
- **Expo CLI** (to run the app)
- An Android/iOS emulator or a pyshical device with the Expo app installed.

---

## Installation

1. Clone this repository to your local machine:

```bash
git clone https://github.com/5PIN0/streamdeck.git
cd streamdeck
```

2. Install the project dependencies:

```bash
npm install
# or
yarn install
```

---

## Running the App

1. Start the Expo development server:

```bash
npm start
# or
yarn start
```

2. Scan the QR code displayed in the terminal or browser using the Expo app on your physical device, or select an emulator to run it.

---

## Project Structure

```plaintext
c:\ReactNative\streamdeck\
├── App.tsx               # Main application file
├── styles.ts             # Centralized styles file
├── PanelEnvio.tsx        # Component for the send panel
├── node_modules/         # Project dependencies
├── package.json          # Project configuration and dependencies
└── README.md             # Project documentation
```

---

## Styles

Styles are centralized in the `styles.ts` file. This keeps the code clean and modular. Example of a style:

```typescript
const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor:'#F4F7FA',
    },

    reloj: {
        fontSize: 32,
        textAlign: 'center',
        fontFamily: 'Courier New',
        color: '#333F50',
        backgroundColor: '#B0C4DE',
        paddingVertical: 10,
        paddingHorizontal: 15,
        borderRadius: 8,
    },
}),
```

To use the styles in a component:

```tsx
import styles from './styles';

<View sytle={styles.container}>
    <Text style={styles.clock}>11:42 PM</Text>
</View>
```

---

## API

The app interacts with an API to generate links and dynamic data. Make sure the API is available on the local network.

### Endpoints used:

1. **Generate Link**:
    - URL: `http://172.16.10.21:7115/api/Funciones/enlace/{parametro}`
    - Method: `GET`
    - Response: Returns a link that is converted into a QR code.

2. **Open Link**:
    - URL: `http://172.16.10.21:7115/api/Funciones/abrirenlace/{id}`
    - Method: `GET`
    - Response: Performs an action based on the provided ID.

---

##Dark Mode

Dark mode is dynamically activated by changing the app's state. Dark styles are defined in `styles.ts` and applied conditionally.

Example:

```tsx
<View style={[styles.container, mode === 'oscuro' && sytles.containerOscuro]}>
    <Text style={[styles.title, mode === 'oscuro' && styles.titletitleOscuro]}>
    Welcome
    </Text>
</View>
```

---

## Contributions

Contributions are welcome! If you want to contribute:

1. Fork the repository.
2. Create a branch for your feature (`git checkout -b feature/new-feature`).
3. Make your changes and commit them (`git commit -m 'Add new feature'`).
4. Submit a pull request.

---

## License

This project is licensed under the MIT License. See the `LICENSE`file for more details.