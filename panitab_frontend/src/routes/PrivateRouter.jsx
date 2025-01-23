//Estas seran las rutas privadas que requieren autenticacion

import { useContext } from "react";
import { AuthContext } from "../context";
import { Navigate } from "react-router-dom";

export const PrivateRouter = ({children}) => {
    //Se obtiene el estado de la autenticacion
    const {logged} = useContext(AuthContext);
    //Si el usuario esta logueado, se muestra el contenido de la ruta, de lo contrario se redirige a la ruta /
    return logged ? children : <Navigate to="/" />
}