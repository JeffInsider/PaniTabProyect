import { FaSpinner } from "react-icons/fa";
import { isObjectEmpty } from "../../../../shared/utils/is-object-empty";
import {customerValidationSchema} from "../../forms/customer/customers_data";
import { useFormik } from "formik";

export const CustomerForm = ({ customer, onSubmit, loading, isCreate }) => {
  //esto solo es para cargar los datos del cliente si es que existe, si no existe se inicializa con valores vacios
  const formik = useFormik({
    initialValues: {
      identityNumber: customer.identityNumber || "",
      firstName: customer.firstName || "",
      lastName: customer.lastName || "",
      phone: customer.phone || "",
      balance: !isCreate ? 0 : undefined,
    },
    validationSchema: customerValidationSchema(isCreate),
    // Validación del formulario
    onSubmit: (values) => {
      onSubmit({
        ...values,
      });
    },
  });

  return (
    <form
      onSubmit={formik.handleSubmit}
      className="space-y-4 bg-white p-6 rounded-lg shadow-md"
    >
      <div>
        <label className="block text-sm font-medium">
          Número de Identidad:
        </label>
        <input
          type="text"
          name="identityNumber"
          className="w-full p-2 border rounded"
          value={formik.values.identityNumber}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
        />
        {formik.touched.identityNumber && formik.errors.identityNumber && (
          <div className="text-red-500 text-sm">
            {formik.errors.identityNumber}
          </div>
        )}
      </div>

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
        <label className="block text-sm font-medium">Teléfono:</label>
        <input
          type="text"
          name="phone"
          className="w-full p-2 border rounded"
          value={formik.values.phone}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
        />
        {formik.touched.phone && formik.errors.phone && (
          <div className="text-red-500 text-sm">{formik.errors.phone}</div>
        )}
      </div>
      <div>
        <label className="block text-sm font-medium">Saldo:</label>
        <input
          type="number"
          name="balance"
          className="w-full p-2 border rounded"
          value={formik.values.balance}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          disabled={isCreate} // Desactiva el campo si se está creando
        />
        {formik.touched.balance && formik.errors.balance && (
          <div className="text-red-500 text-sm">{formik.errors.balance}</div>
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
