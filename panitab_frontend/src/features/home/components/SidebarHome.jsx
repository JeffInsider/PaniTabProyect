import { useEffect, useState } from "react";
import StatsWidget from "./StatsWidget";
import { FaBars, FaUsers } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { SideBar } from "../../../shared/components/SideBar";


export const SidebarHome = ({showSidebar}) => {

    return(
        <SideBar 
            title="Inicio" 
            showSidebar={showSidebar}
            isHome={true} 
        >
            <StatsWidget />
        </SideBar>
    );
};
