import { useState } from "react";

export const useLayout = () => {
    const [showSidebar, setShowSidebar] = useState(true);
    const [showUserMenu, setShowUserMenu] = useState(false);

    return {
        showSidebar,
        setShowSidebar,
        showUserMenu,
        setShowUserMenu
    };
};