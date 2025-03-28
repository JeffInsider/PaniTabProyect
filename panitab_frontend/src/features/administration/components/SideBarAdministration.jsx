import { label, path } from "framer-motion/client";
import { SideBar } from "../../../shared/components/SideBar";
import { FaTruck } from "react-icons/fa";
import { FaHouseChimney } from "react-icons/fa6";

export const SideBarAdministration = ({ showSidebar }) => {
    const options = [
        {
            label: "Dashboard",
            icon: FaHouseChimney,
            path: "/administration"
        },
        {
            label: "Vendedores",
            icon: FaTruck,
            path: "/administration/customer"
        }
    ];
    return (
        <SideBar
            title="Administración"
            options={options}
            showSidebar={showSidebar}
            isHome={false}
        />
    );
}