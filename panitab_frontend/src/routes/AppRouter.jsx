import { Navigate, Route, Routes } from "react-router-dom";
import HomePage from "../features/home/pages/HomePage";
import { ProtectedLayout } from "../shared/components/ProtectedLayout";
import { SecurityRouter } from "../features/security/routes";

//Despues de hacer los auth este archivo es para toda la navegacion de la aplicacion usando rutas privadas y publicas
export const AppRouter = () => {
    return (
        <Routes>
            <Route path="/" element={<Navigate to="/home" />} /> {/* Redirigir a /home */}
            <Route element={<ProtectedLayout />}>
                <Route path="/home" element={<HomePage />} />
            </Route>
            <Route path="/security/*" element={<SecurityRouter />} />

            {/*<Route path="/login" element={<LoginPage />} />*/}
        </Routes>
    );
};