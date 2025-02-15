import { useNavigate } from "react-router-dom";
import logo from "../../../images/logo.png";
import { FaHome } from "react-icons/fa";
import { SideBar } from "../../../shared/components/SideBar";

export const SideBarUser = ({showSidebar}) => {
    
    const options = [
            {
                label: "Usuarios",
                icon: FaHome,
                path: "/users"
            },
            
        ];
    
        return(
                
            <SideBar 
                title="Usuarios" 
                options={options} 
                showSidebar={showSidebar}
                isHome={false} 
            />
    
        );
}