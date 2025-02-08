import { Route, Routes } from "react-router-dom";
import HomePage from "../pages/HomePage";

export const HomeRouter = () => {
    return (
        <Routes>
            {/* Ruta principal de Home */}
            <Route path="/" element={<HomePage />} />

            {/* Otras Rutas de home */}
        </Routes>
    );
}