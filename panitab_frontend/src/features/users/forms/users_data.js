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
        ...(isCreate && {  // Solo valida la contraseña en creación
            password: Yup.string()
                .required("La contraseña es requerida")
                .min(6, "La contraseña debe tener al menos 6 caracteres")
                .matches(/[A-Z]/, "La contraseña debe contener al menos una mayúscula")
                .matches(/\d/, "La contraseña debe contener al menos un número")
                .matches(/[@$!%*?&]/, "La contraseña debe contener al menos un signo especial (@, $, !, %, *, ?, &)"),
            confirmPassword: Yup.string()
                .required("La confirmación de la contraseña es requerida")
                .oneOf([Yup.ref("password"), null], "Las contraseñas no coinciden"),
        }),
    });
};
