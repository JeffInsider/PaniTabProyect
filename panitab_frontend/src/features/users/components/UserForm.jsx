import { useFormik } from "formik"
import { userValidationSchema } from "../forms/users_data";
import { rolesListConstant } from "../../../shared/constants/roles-list.constants";
import { FaSpinner } from "react-icons/fa";
import { isObjectEmpty } from "../../../shared/utils/is-object-empty";

export const UserForm = ({user, onSubmit, loading, isCreate}) => {
    //console.log("crear?", isCreate);
    const formik = useFormik({
        initialValues: {
            firstName: user.firstName || "",
            lastName: user.lastName || "",
            email: user.email || "",
            password: isCreate ? "" : undefined, // Inicializar vacío si es nuevo usuario y el undefined si no es nuevo usuario y no se podra editar
            confirmPassword: isCreate ? "" : undefined, // Inicializar vacío si es nuevo usuario
            roles: Array.isArray(user.roles) ? user.roles[0] : user.roles || "", // Asegurar que sea string  
        },
        validationSchema: userValidationSchema(isCreate),
        onSubmit: (values) => {
            onSubmit({
                ...values,
                password: isCreate && values.password ? values.password : undefined, // Enviar contraseña solo si es un nuevo usuario
                confirmPassword: isCreate && values.confirmPassword ? values.confirmPassword : undefined,  // Enviar confirmación de contraseña solo si es un nuevo usuario
                roles: values.roles, // Enviar roles como un array al backend
                
            });
        },
    });

    return (
        <form onSubmit={formik.handleSubmit} className="space-y-4 bg-white p-6 rounded-lg shadow-md">
            <div>
                <label className="block text-sm font-medium">Nombre:</label>
                <input
                    type="text"
                    name="firstName"
                    className="w-full p-2 border rounded"
                    value={formik.values.firstName}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                {formik.touched.firstName && formik.errors.firstName && (
                    <div className="text-red-500 text-sm">{formik.errors.firstName}</div>
                )}
            </div>

            <div>
                <label className="block text-sm font-medium">Apellido:</label>
                <input
                    type="text"
                    name="lastName"
                    className="w-full p-2 border rounded"
                    value={formik.values.lastName}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                {formik.touched.lastName && formik.errors.lastName && (
                    <div className="text-red-500 text-sm">{formik.errors.lastName}</div>
                )}
            </div>

            <div>
                <label className="block text-sm font-medium">Correo:</label>
                <input
                    type="email"
                    name="email"
                    className="w-full p-2 border rounded"
                    value={formik.values.email}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                />
                {formik.touched.email && formik.errors.email && (
                    <div className="text-red-500 text-sm">{formik.errors.email}</div>
                )}
            </div>

            {isCreate && (
                <div>
                    <label className="block text-sm font-medium">Contraseña:</label>
                    <input
                        type="password"
                        name="password"
                        className="w-full p-2 border rounded"
                        value={formik.values.password}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                    />
                    {formik.touched.password && formik.errors.password && (
                        <div className="text-red-500 text-sm">{formik.errors.password}</div>
                    )}
                </div>
            )}

            {/* Mostrar el campo de confirmación de contraseña solo en la creación */}
            {isCreate && (
                <div>
                    <label className="block text-sm font-medium">Confirmar contraseña:</label>
                    <input
                        type="password"
                        name="confirmPassword"
                        className="w-full p-2 border rounded"
                        value={formik.values.confirmPassword}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                    />
                    {formik.touched.confirmPassword && formik.errors.confirmPassword && (
                        <div className="text-red-500 text-sm">{formik.errors.confirmPassword}</div>
                    )}
                </div>
            )}

            <div>
                <label className="block text-sm font-medium">Rol:</label>
                <select
                    name="roles"
                    className="w-full p-2 border rounded"
                    value={formik.values.roles} // Asegurar que siempre sea string
                    onChange={(e) => formik.setFieldValue("roles", e.target.value)} // Convertir a string
                    onBlur={formik.handleBlur}
                >
                    <option value="" disabled>Selecciona un rol</option>
                    {Object.values(rolesListConstant).map((role) => (
                        <option key={role} value={role}>{role}</option>
                    ))}
                </select>
                    {formik.touched.roles && formik.errors.roles && (
                        <div className="text-red-500 text-sm">{formik.errors.roles}</div>
                    )}
            </div>

            <button
                type="submit"
                className={`
                    w-full py-2.5 rounded-lg text-sm font-semibold text-center inline-block 
                    transition-all duration-200
                    ${loading || !isObjectEmpty(formik.errors) 
                        ? "bg-gray-300 text-black cursor-not-allowed"
                        : "bg-blue-600 text-white hover:bg-blue-700 focus:bg-blue-700"
                    }`}
                disabled={loading || !isObjectEmpty(formik.errors)}
            >
                <span className="inline-block mr-2">
                    {loading ? <FaSpinner className="w-4 h-4 animate-spin" /> : "Guardar"}
                </span>
            </button>
        </form>
    );
};