import { create } from "zustand";
import { loginAsync } from "../../../shared/actions/auth/auth.action";
import { jwtDecode } from "jwt-decode";

export const useAuthStore = create((set, get) => ({ // el set es para modificar el estado y el get es para obtener el estado
    user: null,
    token: null,
    roles: [],
    refreshToken: null,
    isAuthenticated: false,
    message: '',
    error: false,
    // se va a restablecer cada vez que se recargue la pagina

    //init solo se ejecuta una vez cuando se carga la pagina
    init: () => {
        const storedUser = localStorage.getItem('user');
        const storedToken = localStorage.getItem('token');
        const storedRefreshToken = localStorage.getItem('refreshToken');

        if (storedToken && storedRefreshToken){
            set({
                user: storedUser ? JSON.parse(storedUser) : null,
                token: storedToken,
                refreshToken: storedRefreshToken,
                isAuthenticated: true,
            });
        }
    },

    login: async (form) => {
        console.log("Form Values:", form); // Verifica si los valores están llegando
        const {status, data, message} = await loginAsync(form);
        console.log("Login Response:", {status, data, message}); // Verifica la respuesta

        if (status)
        {
            set(
                {
                    error: false,
                    user: {
                        email: data.email,
                        tokenExpiration: data.tokenExpiration,
                        fullName: data.fullName,
                    },
                    token: data.token,
                    refreshToken: data.refreshToken,
                    isAuthenticated: true,
                    message: message
                });
            localStorage.setItem('user', JSON.stringify(get().user ?? {}));
            localStorage.setItem('token', get().token);
            localStorage.setItem('refreshToken', get().refreshToken);

            return;
        }

        set({message: message, error: true});

        return;
    },

    setSession: (user, token, refreshToken) => {
        set(
            {
                user: user,
                token: token,
                refreshToken: refreshToken,
                isAuthenticated: true,
            });

        localStorage.setItem('user', JSON.stringify(get().user ?? {}));
        localStorage.setItem('token', get().token);
        localStorage.setItem('refreshToken', get().refreshToken);
    },

    //metodo para cerrar sesion
    logout: () => {
        set(
            {
                user: null,
                token: null,
                roles: [],
                refreshToken: null,
                isAuthenticated: false,
                error: false,
                message: '',
            });
        localStorage.clear();
    },

    validateAuthentication: () => {
        const token = localStorage.getItem('token');

        if (!token)
        {
            set({isAuthenticated: false});
            return;
        }

        try {
            const decodeJwt = jwtDecode(token);
            const currentDate = Math.floor(Date.now()/1000) //convertir la fecha y hora actual a segundos
            
            if(decodeJwt.exp < currentDate)
            {
                console.log('El token ha expirado');
                set({isAuthenticated: false});
                return;
            }

            const roles = decodeJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

            console.log(roles);

            set({isAuthenticated: true, roles: typeof(roles) === 'string' ? [roles] : roles});
        } catch (error) {
            console.log('Error al validar el token', error);
            set({isAuthenticated: false});
        }
    }
}));