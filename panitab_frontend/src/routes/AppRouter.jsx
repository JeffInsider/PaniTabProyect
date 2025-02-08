import { Navigate, Route, Routes } from "react-router-dom";
import { ProtectedLayout } from "../shared/components/ProtectedLayout";
import { SecurityRouter } from "../features/security/routes";
import { HomeRouter } from "../features/home/routes/HomeRouter";

//Despues de hacer los auth este archivo es para toda la navegacion de la aplicacion usando rutas privadas y publicas
export const AppRouter = () => {
    return (
        <Routes>
            {/* Redirigir a /home */}
            <Route path="/" element={<Navigate to="/home" />} /> 
            
            {/* Rutas protegidas */}
            <Route element={<ProtectedLayout />}>
                <Route path="/home/*" element={<HomeRouter />} />
            </Route>

            {/* Rutas de seguridad */}
            <Route path="/security/*" element={<SecurityRouter />} />

        </Routes>
    );
};