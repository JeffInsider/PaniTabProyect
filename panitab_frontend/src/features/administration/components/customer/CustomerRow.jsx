import { Link } from "react-router-dom"
import { ProtectedComponent } from "../../../../shared/components/ProtectedComponent"
import { FiEdit } from "react-icons/fi"
import { rolesListConstant } from "../../../../shared/constants/roles-list.constants"

export const CustomerRow = ({ customer }) => {
    return (
        <tr className="hover:bg-[#f4f0eb] transition-colors duration-300 ease-in-out">
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{customer.identityNumber}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.firstName}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.lastName}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.phoneNumber}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.balance}</td>
            <td className="px-10 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                <div
                    className={`flex items-center justify-center w-5 h-5 rounded-full ${
                        customer.isActive
                            ? "bg-green-500"
                            : "bg-red-500 text-white"
                    }`}
                >
                    {!customer.isActive && (
                        <span className="text-xs font-semibold">X</span>
                    )}
                </div>
            </td>
            
            <td className="px-10 py-4 whitespace-nowrap text-sm font-medium text-blue-600 hover:text-blue-800 transition duration-200 ease-in-out">
                {/* solo podra ingresar en editar solo los roles */}
                <ProtectedComponent requiredRoles={[rolesListConstant.ADMIN, rolesListConstant.OFFICE]}>
                    <Link to={`/customers/${customer.id}`}>
                        <FiEdit className="w-5 h-5" />
                    </Link>
                </ProtectedComponent>
            </td>

        </tr>
    );
}; 