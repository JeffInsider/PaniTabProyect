import { Navigate, Outlet } from "react-router-dom";
import { useAuthStore } from "../../features/security/store";
import {rolesListConstant} from "../constants/roles-list.constants";

export const ProtectedLayout = () => {

    const isAuthenicated = useAuthStore((state) => state.isAuthenticated);
    const roles = useAuthStore((state) => state.roles);

  console.log("ProtectedLayout",{isAuthenicated, roles})

  // Si el usuario no está autenticado o no tiene un rol válido, redirige al login
    if(!isAuthenicated || !roles.some(role => 
      [
        rolesListConstant.ADMIN, 
        rolesListConstant.OFFICE, 
        rolesListConstant.CHECKER, 
        rolesListConstant.STORE,
        rolesListConstant.REPORTS,
      ].includes(role)
    )){
      return <Navigate to="/security/login" />
    }
    
    // Si está autenticado y tiene un rol válido, muestra el contenido de la página protegida
  return (
    <div >      
    <Outlet />{/* Renderiza el componente hijo */}
  </div>
  )
}