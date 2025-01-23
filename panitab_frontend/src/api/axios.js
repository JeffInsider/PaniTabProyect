//archivo importante para la conexion con el backend

import axios from "axios";

// le indicamos a axios cual es la url del backend
axios.defaults.baseURL = "https://localhost:7093/api";

//este metodo es para establecer el token de autenticacion en el header de las peticiones
//esa funcion se ejecuta de inmediato asegurando que el token este siempre actualizado
const setAuthToken = () => {
    const token = localStorage.getItem("token");

    if (token) {
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
        delete axios.defaults.headers.common["Authorization"];
    }
};

setAuthToken();

let refresh = false;

//interceptores de axios para modificar las peticiones y respuestas
//interceptor de respuesta
axios.interceptors.response.use(
    (response) => response,
    async (error) => {

        //si la respuesta es un error 401 y no se ha intentado refrescar el token, se intenta refrescar el token
        if (error.response.status === 401 && !refresh) {
            refresh = true;
            try {
                const response = await axios.post("/auth/refresh-token", {
                    token: localStorage.getItem("token") ?? "",
                    refreshToken: localStorage.getItem("refreshToken") ?? "",
                },
                {
                    withCredentials: true,
                });

                //si la respuesta es correcta, se actualiza el token y se vuelve a hacer la peticion

                if (response.status === 200) {
                    //se usa el doble data ya que la respuesta del backend tiene la forma {data: {token, refreshToken, tokenExpiration}}
                    localStorage.setItem("token", response.data.data.token);
                    localStorage.setItem("refreshToken", response.data.data.refreshToken);
                    localStorage.setItem("tokenExpiration", response.data.data.tokenExpiration);
                    setAuthToken();

                    return axios(error.config);
                }
                
            } catch (error) {
                console.error("Error al renovar el token",error);
                localStorage.clear();
                window.location.href = "/";
                return Promise.reject(error);
            }
        }
    }
);

//interceptor de peticion
axios.interceptors.request.use(
    //se ejecuta antes de hacer la peticion
    (config) => {
        const token = localStorage.getItem("token");

        //si hay token, se añade al header de la peticion
        if (token) {
            config.headers["Authorization"] = `Bearer ${token}`;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export default axios;