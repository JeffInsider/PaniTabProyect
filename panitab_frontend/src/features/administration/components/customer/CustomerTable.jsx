import { CustomerRow } from "./CustomerRow";

export const CustomerTable = ({ customers }) => {
    return (
        <div className="overflow-x-auto bg-white shadow-lg rounded-xl">
            <table className="min-w-full table-auto">
                <thead className="bg-[#f7f4f0] border-b">
                    <tr>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Numero de Identidad</th>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Nombre</th>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Apellido</th>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Teléfono</th>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Balance</th>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Activo</th>
                        <th className="px-6 py-3 text-left text-sm font-medium text-gray-600 uppercase tracking-wider">Acciones</th>
                    </tr>
                </thead>
                <tbody className="divide-y">
                    {customers.length === 0 ? (
                        <tr>
                            <td colSpan="7" className="text-center py-4 text-gray-500">
                                 No hay clientes registrados todavía.
                            </td>
                        </tr>
                    ) : (
                        customers.map((customer) => (
                            <CustomerRow key={customer.id} customer={customer} />
                        ))
                    )}
                </tbody>
            </table>
        </div>
    );
}