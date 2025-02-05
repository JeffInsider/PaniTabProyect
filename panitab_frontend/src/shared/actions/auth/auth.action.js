import { panitabApi} from "./../../../config/api/panitabApi";

//metodo para hacer login
export const loginAsync = async (form) => {
    try {
        const {data} = await panitabApi.post('/auth/login', form);
        console.log("Login exitoso", data);
        return data;
    } catch (error) {
        console.error("Error en el login", error);
        //manejar bien el error
        if (error?.response?.status === 401) {
            return { ...error?.response?.data, message: 'Correo o contraseña incorrectos' };
        }

        // Para otros errores (por ejemplo, servidor caído o problemas de red)
        return { message: 'Hubo un problema con la conexión o el servidor', ...error?.response?.data };
        //return error?.response?.data;
    }
}