import React, { useState } from "react";
import { useAuthStore } from "../../features/security/store"

export const ProtectedComponent = ({requiredRoles, children}) => {
    const [showMessage, setShowMessage] = useState(false);
    const roles = useAuthStore((state) => state.roles) ?? [];

    // Verificar si el usuario tiene algún rol requerido
    const hasPermission = roles.some((role) => requiredRoles.includes(role));

    const handleClick = (e) => {
        if (!hasPermission) {
            e.preventDefault();
            // Mostrar mensaje durante 3 segundos
            setShowMessage(true);
            setTimeout(() => {
                setShowMessage(false);
            }, 3000);
        }
    };

    return (
        <div>
            {showMessage && (
                <div className="fixed inset-0 flex items-center justify-center z-50">
                    <div className="bg-red-500 text-white text-center p-4 rounded-lg shadow-lg">
                        <p className="font-semibold">Acceso no autorizado</p>
                    </div>
                </div>
            )}
            {children && React.cloneElement(children, { onClick: handleClick })}
        </div>
    );
};