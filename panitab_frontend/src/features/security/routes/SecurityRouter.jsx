import { Navigate, Route, Routes } from "react-router-dom";
import { LoginPage } from "../pages";

export const SecurityRouter = () => {
    return (
    <div >    
      {/* Contenedor principal de contenido */}
        <Routes>
            {/* Redirigir a /login */}
            <Route path='/login' element={<LoginPage />} />
            {/* Redirigir a /login */}
            <Route path='/*' element={<Navigate to="/security/login" />} />
        </Routes>
    </div>
    );
};
        