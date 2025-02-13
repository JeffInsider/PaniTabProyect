import { Route, Routes } from "react-router-dom";
import { UsersPage } from "../pages/UsersPage";

export const UserRouter = () => {
    return (
        <Routes>
            {/* Ruta principal de User */}
            <Route path="/" element={<UsersPage />} />

            {/* Otras Rutas de User */}
        </Routes>
    );
}