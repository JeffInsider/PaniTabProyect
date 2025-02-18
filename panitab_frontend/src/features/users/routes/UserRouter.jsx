import { Route, Routes } from "react-router-dom";
import { UserListPage } from "../pages/UserListPage";
import { UserDetailPage } from "../pages/UserDetailPage";
import { UserCreatePage } from "../pages/UserCreatePage";

export const UserRouter = () => {
    return (
        <Routes>
            {/* Ruta principal de User */}
            <Route path="/" element={<UserListPage />} />
            {/* Otras Rutas de User */}
            <Route path="/:userId" element={<UserDetailPage />} />
            <Route path="/create" element={<UserCreatePage />} />
        </Routes>
    );
}