// gestiona el inicio y cierre de sesion en toda la aplicacion con el localstorage

import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "./AuthContext";

const init = () => {
    const token = localStorage.getItem("token");

    // si el token existe, el usuario está logueado
    return {
        logged: !!token,
        user: token ? { token } : null,
    };
};

// crea un contexto que va a almacenar el estado de la autenticación (si el usuario está logueado o no, y los datos asociados como el token).
export const AuthProvider = ({ children }) => {
    const navigate = useNavigate();
    const [userState, setUserState] = useState(init);

    // funcion para iniciar sesion
    const login = (data) => {
        localStorage.setItem("user", JSON.stringify(data));
        localStorage.setItem("token", data.token);
        localStorage.setItem("refreshToken", data.refreshToken);
        localStorage.setItem("tokenExpiration", data.tokenExpiration);
        setUserState({
            logged: true,
            user: data,
            token: data.token,
            refreshToken: data.refreshToken,
            username: data.username,
            tokenExpiration: data.tokenExpiration,
        });
        navigate("/home");
    };

    // funcion para cerrar sesion

    const logout = () => {
        localStorage.clear();
        setUserState({
            logged: false,
            user: null,
        });
        navigate("/");
    };

    // provee el estado de la autenticación y las funciones para iniciar y cerrar sesión
    return (
        <AuthContext.Provider value={{ ...userState, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};