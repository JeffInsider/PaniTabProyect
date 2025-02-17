import { Route, Routes } from "react-router-dom";
import { UserListPage } from "../pages/UserListPage";
import { UserDetailPage } from "../pages/UserDetailPage";

export const UserRouter = () => {
    return (
        <Routes>
            {/* Ruta principal de User */}
            <Route path="/" element={<UserListPage />} />
            {/* Otras Rutas de User */}
            <Route path="/:userId" element={<UserDetailPage />} />
        </Routes>
    );
}