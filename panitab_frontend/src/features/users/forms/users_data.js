import * as Yup from "yup";

export const userInitValues = {
    firstName: "",
    lastName: "",
    email: "",
    roles: "",
    isActive: false,
};

export const userValidationSchema = Yup.object({
    firstName: Yup.string().required("El nombre es requerido"),
    lastName: Yup.string().required("El apellido es requerido"),
    email: Yup.string()
        .required("El correo electrónico es requerido")
        .email("Email inválido"),
    roles: Yup.string().required("El rol es requerido"),
});
