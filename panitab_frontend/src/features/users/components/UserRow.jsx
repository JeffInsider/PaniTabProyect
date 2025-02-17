import { FiEdit } from "react-icons/fi";
import { Link } from "react-router-dom";

export const UserRow = ({ user }) => {
    return (
        <tr className="hover:bg-[#f4f0eb] transition-colors duration-300 ease-in-out">
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.firstName}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.lastName}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.email}</td>
            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.roles}</td>
            <td className="px-10 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                <div
                    className={`flex items-center justify-center w-5 h-5 rounded-full ${
                        user.isActive
                            ? "bg-green-500"
                            : "bg-red-500 text-white"
                    }`}
                >
                    {!user.isActive && (
                        <span className="text-xs font-semibold">X</span>
                    )}
                </div>
            </td>
            <td className="px-10 py-4 whitespace-nowrap text-sm font-medium text-blue-600 hover:text-blue-800 transition duration-200 ease-in-out">
                <Link to={`/users/${user.id}`}>
                    <FiEdit className="text-xl" />
                </Link>
            </td>
        </tr>
    ); 
};