import { useContext } from "react"
import { Navigate } from "react-router-dom";
import { AuthContext } from "../context";

//Estas seran las rutas publicas, es decir, las que no requieren autenticacion
export const PublicRouter = ({children}) => {
    //Se obtiene el estado de la autenticacion
    const {logged} = useContext(AuthContext);
    //Si el usuario no esta logueado, se muestra el contenido de la ruta, de lo contrario se redirige a la ruta /home
    return !logged ? children : <Navigate to="/home" />
}