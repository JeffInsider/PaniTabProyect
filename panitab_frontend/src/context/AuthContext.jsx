// crea un contexto que va a almacenar el estado de la autenticación (si el usuario está logueado o no, y los datos asociados como el token).
import { createContext } from "react";

export const AuthContext = createContext();