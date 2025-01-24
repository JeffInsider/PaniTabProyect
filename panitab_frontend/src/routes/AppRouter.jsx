import { Route, Routes } from "react-router-dom";
import { PublicRouter } from "./PublicRouter";
import { PrivateRouter } from "./PrivateRouter";
import { LoginPage } from "../pages/auth/LoginPage";
import HomePage from "../pages/home/HomePage";

//Despues de hacer los auth este archivo es para toda la navegacion de la aplicacion usando rutas privadas y publicas
export const AppRouter = () => {
    return (
        <Routes>
                {/* Rutas publicas */}
                <Route
                    path="/"
                    element={
                        <PublicRouter>
                            <LoginPage />
                        </PublicRouter>
                    }
                />
                {/* Rutas publicas */}
                <Route
                    path="/home"
                    element={
                        <PrivateRouter>
                            <HomePage />
                        </PrivateRouter>
                    }
                />
            </Routes>
    );
};