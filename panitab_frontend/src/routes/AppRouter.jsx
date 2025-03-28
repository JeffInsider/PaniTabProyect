import { Navigate, Route, Routes } from "react-router-dom";
import { ProtectedLayout } from "../shared/components/ProtectedLayout";
import { SecurityRouter } from "../features/security/routes";
import { HomeRouter } from "../features/home/routes/HomeRouter";
import { UserRouter } from "../features/users/routes/UserRouter";
import { AdministrationRoute } from "../features/administration/routes/AdministrationRoute";

//Despues de hacer los auth este archivo es para toda la navegacion de la aplicacion usando rutas privadas y publicas
export const AppRouter = () => {
    return (
        <Routes>
            {/* Redirigir a /home */}
            <Route path="/" element={<Navigate to="/home" />} /> 
            
            {/* Rutas protegidas */}
            <Route element={<ProtectedLayout />}>
                <Route path="/home/*" element={<HomeRouter />} />
                <Route path="/users/*" element={<UserRouter />} />
                <Route path="/administration/*" element={<AdministrationRoute />}></Route>
            </Route>

            {/* Rutas de seguridad */}
            <Route path="/security/*" element={<SecurityRouter />} />

        </Routes>
    );
};