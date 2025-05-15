import React, { useState } from 'react';
import { View, Text, TextInput, Button, StyleSheet, Alert } from 'react-native';

const PanelEnvio = () => {
    const [nombreBoton, setNombreBoton] = useState('');
    const [funcion, setFuncion] = useState('');

    const enviarDatosAPI = async () => {
        try {
            console.log(`[DEBUG] Enviando datos a la API: Nombre del Botón: ${nombreBoton}, Función: ${funcion}`);
            const url = `http://172.16.10.21:7115/api/Botones`;

            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    nombre: nombreBoton,
                    funcion: funcion,
                }),
            });

            if (!response.ok) {
                throw new Error(`Error en la API: ${response.status}`);
            }

            const data = await response.json();
            console.log('[DEBUG] Respuesta de la API:', data);

            Alert.alert('Éxito', 'Los datos se enviaron correctamente');
            setNombreBoton('');
            setFuncion('');
        } catch (error) {
            console.error('[ERROR] Error al enviar los datos a la API:', error);
            Alert.alert('Error', 'Hubo un problema al enviar los datos');
        }
    };

    return (
        <View style={styles.container}>
            <Text style={styles.label}>Nombre del Botón</Text>
            <TextInput
                style={styles.input}
                value={nombreBoton}
                onChangeText={setNombreBoton}
                placeholder="Ingrese el nombre del botón"
            />

            <Text style={styles.label}>Función</Text>
            <TextInput
                style={styles.input}
                value={funcion}
                onChangeText={setFuncion}
                placeholder="Ingrese la función"
            />

            <Button title="Enviar" onPress={enviarDatosAPI} />
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        padding: 20,
        backgroundColor: '#fff',
    },
    label: {
        fontSize: 16,
        fontWeight: 'bold',
        marginBottom: 5,
    },
    input: {
        height: 40,
        borderColor: '#ccc',
        borderWidth: 1,
        borderRadius: 5,
        marginBottom: 15,
        paddingHorizontal: 10,
    },
});

export default PanelEnvio;