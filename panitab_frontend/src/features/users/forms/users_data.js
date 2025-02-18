import * as Yup from "yup";

export const userInitValues = {
    firstName: "",
    lastName: "",
    email: "",
    roles: "",
    password: "",
    confirmPassword: "",
    isActive: false,
};

export const userValidationSchema = (isCreate) => {
    return Yup.object({
        firstName: Yup.string().required("El nombre es requerido"),
        lastName: Yup.string().required("El apellido es requerido"),
        email: Yup.string()
            .required("El correo electrónico es requerido")
            .email("Email inválido"),
        roles: Yup.string().required("El rol es requerido"),
        ...(isCreate && {  // Solo incluye estas validaciones si es crear
            password: Yup.string()
                .required("La contraseña es requerida")
                .min(6, "La contraseña debe tener al menos 6 caracteres"),
            confirmPassword: Yup.string()
                .required("La confirmación de la contraseña es requerida")
                .oneOf([Yup.ref("password"), null], "Las contraseñas no coinciden"),
        }),
    });
};
