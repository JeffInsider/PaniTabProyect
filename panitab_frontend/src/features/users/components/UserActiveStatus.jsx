import { disableUser, enableUser } from "../../../shared/actions/users/users.action";

export const UserActiveStatus = ({ isActive, onToggleStatus }) => {

    return (
        <div className="mt-6 bg-white p-6 rounded-lg shadow-md">
            <h3 className="text-xl font-semibold mb-4">Estado del Usuario</h3>
            <div className="flex items-center gap-4">
                <div className="flex items-center gap-2">
                    <span className="text-sm font-medium">Estado:</span>
                    <button
                        type="button"
                        onClick={onToggleStatus} // Llamamos a la función que viene de la página
                        className={`px-4 py-2 rounded-full text-white font-semibold transition-all duration-300 
                            ${isActive ? "bg-green-500 hover:bg-green-600" : "bg-red-500 hover:bg-red-600"}`}
                    >
                        {isActive ? "Activo" : "Inactivo"}
                    </button>
                </div>
                <p className="text-sm text-gray-600">
                    {isActive
                        ? "El usuario tiene acceso completo al sistema."
                        : "El usuario no tiene acceso al sistema."}
                </p>
            </div>
        </div>
    );
};