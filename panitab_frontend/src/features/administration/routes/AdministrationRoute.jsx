import { Route, Routes } from "react-router-dom";
import { DashboardPage } from "../pages/DashboardPage";

export const AdministrationRoute = () => {
    return (
        <Routes>
            {/* Ruta principal de Administración */}
            <Route path="/" element={<DashboardPage />} />
            {/* Otras Rutas de Administración */}
        </Routes>
    );
}