import React, { useEffect, useState } from 'react';
import { View, Text, Button, StyleSheet, TouchableOpacity, Image, Modal, Alert, TextInput } from 'react-native';
import QRCode from 'react-native-qrcode-svg';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import PanelEnvio from './PanelEnvio';
import styles from './styles';
// import { Camera, CameraType } from 'expo-camera';
// const CameraComponent = Camera as unknown as React.ComponentType<React.PropsWithChildren<{ style: any; type: CameraType }>>;


const apiUrl = 'http://172.16.10.21:7115/api/Funciones';

const Stack = createStackNavigator();

const HomeScreen = ({ navigation }: { navigation: any }) => {

  const [modo, setModo] = useState('claro');
  // const [crucetaTipo, setCrucetaTipo] = useState('botones');
  const [horaActual, setHoraActual] = useState(new Date());
  const [menuVisible, setMenuVisible] = useState(false);
  const [qrVisible, setQrVisible] = useState(false);
  const [qrValue, setQrValue] = useState('');
  const [parametroModalVisible, setParametroModalVisible] = useState(false);
  const [parametro, setParametro] = useState('');
  const [scanModalVisible, setScanModalVisible] = useState(false);
  const [inputQR, setInputQR] = useState('');
  const qrValidos = ['exampleQRCode123', 'exampleQRCode456'];
  // const [cameraVisible, setCameraVisible] = useState(false);
  // const [tienePermisos, setTienePermisos] = useState<boolean | null>(null);

  const inicializarPreferencias = () => {
    const modoGuardado = 'claro';
    // const crucetaGuardada = 'botones';
    // setModo(modoGuardado);
    // setCrucetaTipo(crucetaGuardada);
    console.log('[DEBUG] Preferencias cargadas:', { modoGuardado });
    // console.log('[DEBUG] Preferencias cargadas:', { modoGuardado, crucetaGuardada });
  };

  useEffect(() => {
    if (qrVisible) {
      const timeout = setTimeout(() => {
        console.log('[DEBUG] Cerrando el modal del QR automáticamente');
        setQrVisible(false);
      }, 20000);

      return () => clearTimeout(timeout);
    }
  }, [qrVisible]);

  useEffect(() => {
    console.log('[DEBUG] Ejecutando useEffect para inicializar preferencias');
    inicializarPreferencias();

    const intervalo = setInterval(() => {
      setHoraActual(new Date());
    }, 1000);
    console.log('[DEBUG] Intervalo de actualización de hora establecido');

    return () => {
      console.log('[DEBUG] Limpiando intervalo de actualización de hora');
      clearInterval(intervalo);
    };
  }, []);

//   useEffect(() => {
//     console.log('[DEBUG] Ejecutando useEffect para solicitar permisos de cámara');
//       const solicitarPermisos = async () => {
//       try {
//         const { status } = await Camera.requestCameraPermissionsAsync();
//         console.log(`[DEBUG] Permisos de cámara: ${status}`);
//         setTienePermisos(status === 'granted');
//       } catch (error) {
//         console.error('[ERROR] Error al solicitar permisos de cámara:', error);
//         setTienePermisos(false);
//       }
//     };
//       solicitarPermisos();
//   }, []);
// if (tienePermisos === null) {
//   console.log('[DEBUG] Permisos de cámara aún no concedidos');
//   return <Text>Solicitando permisos de cámara...</Text>;
// }

// if (tienePermisos === false) {
//   console.log('[DEBUG] Permisos de cámara denegados');
//   return <Text>No se concedieron permisos para usar la cámara.</Text>;
// }

  const actualizarModo = (nuevoModo: string) => {
    console.log(`[DEBUG] Actualizando modo a: ${nuevoModo}`);
    setModo(nuevoModo);
  };

  // const actualizarVisibilidadCruceta = (tipoCruceta: string) => {
  //   console.log(`[DEBUG] Actualizando visibilidad de la cruceta a: ${tipoCruceta}`);
  //   setCrucetaTipo(tipoCruceta);
  // };

  const manejarClickBoton = (id: string) => {
    console.log(`[DEBUG] Botón "${id}" presionado`);
    enviarPeticionAPI(id);
  };

  const enviarPeticionAPI = async (id: string) => {
    console.log(`[DEBUG] Enviando petición a la API con ID: ${id}`);
    try {
      const url = `http://172.16.10.21:7115/api/Funciones/abrirenlace/${encodeURIComponent(id)}`;
      console.log(`[DEBUG] URL de la API: ${url}`);

      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Content-Type': 'text/plain',
        },
      });

      console.log('[DEBUG] Respuesta de la API:', response);

      if (!response.ok) {
        throw new Error(`Error en la API: ${response.status}`);
      }

      const contentType = response.headers.get('Content-Type');
      let data;

      if (contentType && contentType.includes('application/json')) {
        data = await response.json();
      } else {
        data = await response.text();
      }

      console.log('[DEBUG] Respuesta procesada de la API:', data);

      if (data) {
        Alert.alert('Éxito', 'El botón se creó correctamente');
      } else {
        Alert.alert('Error', 'La API no devolvió datos válidos');
      }
    } catch (error) {
      console.error('[ERROR] Error al enviar la petición a la API:', error);
      Alert.alert('Error', 'Hubo un problema al enviar la petición a la API');
    }
  };

  const generarQR = async () => {
  try {
    const url = `http://172.16.10.21:7115/api/Funciones/enlace/${encodeURIComponent(parametro)}`;
    console.log(`[DEBUG] Realizando petición GET a: ${url}`);

    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`Error en la API: ${response.status}`);
    }

    const contentType = response.headers.get('Content-Type');
    let data;

    if (contentType && contentType.includes('application/json')) {
      data = await response.json();
    } else {
      data = await response.text();
    }

    console.log('[DEBUG] Respuesta de la API:', data);

    if (data) {
      setQrValue(data);
      setQrVisible(true);
    } else {
      Alert.alert('Error', 'La API no devolvió un enlace válido');
    }
  } catch (error) {
    console.error('[ERROR] Error al generar el QR:', error);
    Alert.alert('Error', 'Hubo un problema al generar el QR');
  }
};

const manejarEnvioParametro = async () => {
  try {
    setParametroModalVisible(false);

    const url1 = `http://172.16.10.21:7115/api/Funciones/abrir/cmd`;
    console.log(`[DEBUG] Realizando primera petición GET a: ${url1}`);

    const response1 = await fetch(url1, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response1.ok) {
      throw new Error(`Error en la primera API: ${response1.status}`);
    }

    const contentType1 = response1.headers.get('Content-Type');
    let data1;

    if (contentType1 && contentType1.includes('application/json')) {
      data1 = await response1.json();
    } else {
      data1 = await response1.text();
    }

    console.log('[DEBUG] Respuesta de la primera API:', data1);

    const url2 = `http://172.16.10.21:7115/api/Funciones/enlace/${encodeURIComponent(parametro)}`;
    console.log(`[DEBUG] Realizando segunda petición GET a: ${url2}`);

    const response2 = await fetch(url2, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response2.ok) {
      throw new Error(`Error en la segunda API: ${response2.status}`);
    }

    const contentType2 = response2.headers.get('Content-Type');
    let data2;

    if (contentType2 && contentType2.includes('application/json')) {
      data2 = await response2.json();
    } else {
      data2 = await response2.text();
    }

    console.log('[DEBUG] Respuesta de la primera API:', data2);

if (data2) {
      setQrValue(data2);
      setQrVisible(true);
    } else {
      Alert.alert('Error', 'La primera API no devolvió un enlace válido');
    }

    Alert.alert('Éxito', 'Ambas solicitudes se realizaron correctamente');
  } catch (error) {
    console.error('[ERROR] Error al realizar las solicitudes:', error);
    Alert.alert('Error', 'Hubo un problema al realizar las solicitudes');
  }
};

const manejarEscaneoQR = async () => {
  if (qrValidos.includes(inputQR)) {
    try {
      console.log(`[DEBUG] Código QR válido: ${inputQR}`);
      const url = `${apiUrl}/${encodeURIComponent(inputQR)}`;
      console.log(`[DEBUG] Realizando petición GET a: ${url}`);

      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error(`Error en la API: ${response.status}`);
      }

      const contentType = response.headers.get('Content-Type');
      let data;

      if (contentType && contentType.includes('application/json')) {
        data = await response.json();
      } else {
        data = await response.text();
      }

      console.log('[DEBUG] Respuesta de la API:', data);
      Alert.alert('Éxito', 'El QR fue procesado correctamente');
    } catch (error) {
      console.error('[ERROR] Error al procesar el QR:', error);
      Alert.alert('Error', 'Hubo un problema al procesar el QR');
    }
  } else {
    console.log(`[DEBUG] Código QR no válido: ${inputQR}`);
    Alert.alert('Error', 'El QR no coincide con los códigos válidos');
  }

  setScanModalVisible(false);
  setInputQR('');
};

// const manejarCodigoQRDetectado = async ({ data }: { data: string }) => {
//     try {
//         const response = await fetch(data); 
//         if (!response.ok) {
//             throw new Error('Error en la respuesta de la API');
//         }
//         const json = await response.json();
//         // Validar el contenido del JSON
//         if (json && typeof json === 'object') {
//             alert(`Datos recibidos: ${JSON.stringify(json)}`);
//             // Procesa los datos recibidos según tu lógica
//         } else {
//             alert('Respuesta no válida de la API');
//         }
//     } catch (error) {
//         console.error('Error al escanear el código:', error);
//         alert('Hubo un problema al procesar el código QR.');
//     }
// };
// if (hasPermission === null) {
//   return <Text>Solicitando permisos de cámara...</Text>;
// }

// if (hasPermission === false) {
//   return <Text>No se concedieron permisos para usar la cámara.</Text>;
// }

  const botones = [
    { id: 'modo', img : require('./assets/img/claro.png'), alt: 'Modo'},
    { id: 'boton1', img: require('./assets/img/search.png'), alt: 'Buscar' },
    { id: 'boton2', img: require('./assets/img/settings.png'), alt: 'Configuración' },
    { id: 'boton3', img: require('./assets/img/user.png'), alt: 'Usuario' },
    { id: 'boton4', img: require('./assets/img/email.png'), alt: 'Correo' },
    { id: 'boton5', img: require('./assets/img/download.png'), alt: 'Descargar' },
    { id: 'boton6', img: require('./assets/img/upload.png'), alt: 'Subir' },
    { id: 'boton7', img: require('./assets/img/like.png'), alt: 'Favoritos' },
    { id: 'boton8', img: require('./assets/img/logout.png'), alt: 'Cerrar sesión' },
  ];

  return (
    <View style={[styles.container, modo === 'oscuro' &&  styles.containerOscuro]}>
      {/* Reloj */}
      <View style={styles.relojContainer}>
        <Text style={[styles.reloj, modo === 'oscuro' && styles.relojOscuro]}>
          {horaActual.toLocaleTimeString('es-ES', {hour: '2-digit', minute: '2-digit'})}
        </Text>
      </View>

      {/* CONFIGURACION */}
      <View style={[styles.configuracionMenu, modo === 'oscuro' &&  styles.configuracionOscuro]}>
        <TouchableOpacity onPress={() => setMenuVisible(!menuVisible)}>
          <Image
            source={
              modo === 'oscuro'
                ? require('./assets/img/settingsoscuro.png')
                : require('./assets/img/settings.png')
            }
            style={styles.iconoConfiguracion}
          />
        </TouchableOpacity>

        {/* Menú desplegable */}
        {menuVisible && (
          <View style={styles.menuDesplegable}>
            <View>
              <Text>Modo Oscuro</Text>
              <Button
                title={modo === 'oscuro' ? 'Desactivar' : 'Activar'}
                onPress={() => actualizarModo(modo === 'oscuro' ? 'claro' : 'oscuro')}
              />
            </View>
            {/* <View>
              <Text>Cruceta</Text>
              <Button
                title={`Tipo: ${crucetaTipo}`}
                onPress={() =>
                  actualizarVisibilidadCruceta(
                    crucetaTipo === 'botones' ? 'imagen' : crucetaTipo === 'imagen' ? 'ninguna' : 'botones'
                  )
                }
              />
            </View> */}
            <View>
              <Button
                title="Generar QR"
                onPress={() => setParametroModalVisible(true)}
              />
            </View>
            <View>
              <Button
                title="Ir a Panel de Envío"
                onPress={() => navigation.navigate('PanelEnvio')}
              />
            </View>
            {/* <View>
              <Button
                title="ESCANEAR QR"
                onPress={() => setCameraVisible(true)} // Abre la cámara
              />
            </View> */}
          </View>
        )}
      </View>

      {/* Menú de botones */}
      <Text style={[styles.titulo, modo === 'oscuro' && styles.tituloOscuro]}>Menú de botones</Text>
      <View style={styles.menuContainer}>
        {botones.map((boton, index) => {
          return (
            <TouchableOpacity key={index} onPress={() => manejarClickBoton(boton.id)} style={[styles.boton, modo === 'oscuro' &&  styles.botonOscuro]}>
              <Image source={boton.img} style={styles.icono} />
            </TouchableOpacity>
          );
        })}
      </View>

      {/* Cruceta
      {crucetaTipo === 'botones' && (
        <View style={styles.crucetaContainer}>
          <Button title="▲" onPress={() => console.log('Arriba')} />
          <View style={styles.crucetaFila}>
            <Button title="◀" onPress={() => console.log('Izquierda')} />
            <Button title="⏺" onPress={() => console.log('Seleccionar')} />
            <Button title="▶" onPress={() => console.log('Derecha')} />
          </View>
          <Button title="▼" onPress={() => console.log('Abajo')} />
        </View>
      )} */}

      {/* Modal para ingresar el parámetro */}
      <Modal visible={parametroModalVisible} transparent={true} animationType="slide">
        <View style={styles.modalContainer}>
          <View style={styles.parametroContainer}>
            <Text style={styles.parametroTitle}>Ingrese el parámetro</Text>
            <TextInput
              style={styles.input}
              value={parametro}
              onChangeText={setParametro}
              placeholder="Ingrese el parámetro"
            />
            <Button
              title="Enviar"
              onPress={() => {
                setParametroModalVisible(false);
                manejarEnvioParametro();
              }}
            />
            <Button title="Cancelar" onPress={() => setParametroModalVisible(false)} />
          </View>
        </View>
      </Modal>

      {/* Modal para mostrar el QR */}
      <Modal visible={qrVisible} transparent={true} animationType="slide">
        <View style={styles.modalContainer}>
          <View style={styles.qrContainer}>
            <Text style={styles.qrTitle}>Escanea este código QR</Text>
            {qrValue ? (
              <QRCode value={qrValue} size={200} />
            ) : (
              <Text>Cargando...</Text>
            )}
          </View>
        </View>
      </Modal>

    {/* Modal para la cámara
    <View style={styles.container}>
      <Button title="Abrir cámara" onPress={() => setCameraVisible(true)} />
    </View>
    {cameraVisible && (
      <Modal visible={cameraVisible} transparent={true} animationType="slide">
        <CameraComponent
          style={{ flex: 1 }}
          type={CameraType.back} // Especifica el tipo de cámara (frontal o trasera)
        >
          <View style={styles.cameraOverlay}>
            <Text style={styles.cameraText}>Escanea el código QR</Text>
            <Button title="Cancelar" onPress={() => setCameraVisible(false)} />
          </View>
        </CameraComponent>
      </Modal>
    )} */}

      {/* Modal para escanear QR */}
      <Modal visible={scanModalVisible} transparent={true} animationType="slide">
        <View style={styles.modalContainer}>
          <View style={styles.parametroContainer}>
            <Text style={styles.parametroTitle}>Ingrese el código QR</Text>
            <TextInput
              style={styles.input}
              value={inputQR}
              onChangeText={setInputQR}
              placeholder="Ingrese el código QR"
            />
            <Button
              title="Enviar"
              onPress={() => {
                console.log(`[DEBUG] Código QR ingresado: ${inputQR}`);
              }}
            />
            <Button title="Cancelar" onPress={() => setScanModalVisible(false)} />
          </View>
        </View>
      </Modal>
    </View>
  );
};

const App = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Home">
        <Stack.Screen name="Home" component={HomeScreen} options={{ title: 'StreamDeck' }} />
        <Stack.Screen name="PanelEnvio" component={PanelEnvio} options={{ title: 'Panel de Envío' }} />
      </Stack.Navigator>
    </NavigationContainer>
  );
};


export default App;
