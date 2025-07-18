import { Route, Routes } from "react-router-dom";
import { DashboardPage } from "../pages/DashboardPage";
import { CustomerListPage } from "../pages/customer/CustomerListPage";
import { CustomerCreatePage } from "../pages/customer/CustomerCreatePage";

export const AdministrationRoute = () => {
    return (
        <Routes>
            {/* Ruta principal de Administración */}
            <Route path="/" element={<DashboardPage />} />
            {/* Otras Rutas de Administración */}
            <Route path="/customers" element={<CustomerListPage/>}/>
            <Route path="/customers/create" element={<CustomerCreatePage/>}/>
        </Routes>
    );
}