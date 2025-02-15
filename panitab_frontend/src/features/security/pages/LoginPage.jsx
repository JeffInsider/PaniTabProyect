import { useEffect, useState } from "react"
import { FiEyeOff } from "react-icons/fi";
import { LuEye } from "react-icons/lu";
import styled, { keyframes } from "styled-components";
import { useAuthStore } from "../store";
import { useFormik } from "formik";
import { loginInitValues, loginValidationSchema} from "../forms/login_data";
//import { Loading } from "../../../shared/components/Loading";
import { isObjectEmpty } from "../../../shared/utils/is-object-empty";
import { FaArrowRight } from "react-icons/fa";
import { useNavigate } from "react-router-dom";

import { FaSpinner } from "react-icons/fa";

export const LoginPage = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    
    //
    const [showPassword, setShowPassword] = useState(false);
    const [showErrorMessage, setShowErrorMessage] = useState(false);
    //
    
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
    const login = useAuthStore((state) => state.login);
    const error = useAuthStore((state) => state.error);
    const validateAuthentication = useAuthStore((state) => state.validateAuthentication);

    const message = useAuthStore((state) => state.message);

    useEffect(() => {
        if (isAuthenticated) {
          navigate("/home");
        }
    }, [isAuthenticated]);

    // mostrar el mensaje de error despues de 3 segundos
    useEffect(() => {
        if (error) {
            setShowErrorMessage(true);
            const timer = setTimeout(() => {
                setShowErrorMessage(false);
                useAuthStore.setState({ message:"", error: false });
            }, 3000);
            return () => clearTimeout(timer);
        }
    }, [error]);

      
    const formik = useFormik({
        initialValues: loginInitValues,
        validationSchema: loginValidationSchema,
        validateOnChange: true,
        onSubmit: async (formValues) => {
            console.log("Los valores del formulario son:", formValues); // Agregar un log aquí para asegurarte de que se esté llamando
            setLoading(true);
            await login(formValues);
            validateAuthentication();
            setLoading(false);
        },
    });

    //mostrar una pagina de carga mientras se valida la autenticacion
    //if (loading) {
    //    return <Loading />;
    //}


    //esto es para mostrar o no la contraseña
    const handleShowPassword = () => {
        setShowPassword(!showPassword);
    };
    

    return (
        <WavesContainer>
            {showErrorMessage && error ? ( // Mostrar el mensaje de error si showErrorMessage es true y error es true
                <div className="absolute top-10 left-1/2 transform -translate-x-1/2 z-50">
                    <div className="bg-red-500 text-white text-center py-2 px-4 rounded-lg shadow-md animate-fade">
                    {message}
                    </div>
                </div>
            ) : (
                ""
            )}

            <div className="bg-white bg-opacity-30 backdrop-blur-md rounded-lg shadow-lg p-8 z-10 flex flex-col items-center">
                <div className="text-center mb-1">
                    <h1 className="text-4xl font-bold mb-4">PaniTab Beta</h1>
                    <img src="/src/images/logo.png" alt="Logo" className="w-32 h-32 mx-auto" />
                </div>
                <div className="w-full ">
                    <h2 className="text-2xl font-bold mb-2 text-center">Inicie Sesion</h2>
                    <form onSubmit={formik.handleSubmit} className="flex flex-col items-center">
                        <div className="w-full flex flex-col items-center">
                            <div className="mb-6 relative">
                                <label 
                                    htmlFor="email"
                                    className="block text-sm font-semibold mb-2"
                                >
                                    Correo Electrónico:
                                </label>
                                <div className="relative">
                                    <input 
                                        className="w-80 rounded-md bg-transparent border-b-2 
                                            border-blue-500 focus:outline-none focus:border-blue-700"
                                        type="email"
                                        name="email"
                                        id="email"
                                        value={formik.values.email}
                                        onChange={formik.handleChange}
                                        onBlur={formik.handleBlur}
                                    />
                                    
                                    {formik.touched.email && formik.errors.email && (
                                        <div className="text-red-500 text-xs mb-2">
                                        {formik.errors.email}
                                      </div>
                                    )}

                                </div>
                            </div>
                            <div className="mb-6 relative">
                                <label 
                                    htmlFor="password"
                                    className="block text-sm font-semibold mb-2"
                                >
                                    Contraseña:
                                </label>
                                <div className="relative">
                                    <input 
                                        className="w-80 rounded-md bg-transparent border-b-2 
                                        border-blue-500 focus:outline-none focus:border-blue-700" 
                                        type={showPassword ? "text" : "password"}
                                        id="password"
                                        name="password"
                                        value={formik.values.password}
                                        onChange={formik.handleChange}
                                        onBlur={formik.handleBlur}
                                    />
                                    {formik.touched.password && formik.errors.password && (
                                        <div className="text-red-500 text-xs mb-2">
                                            {formik.errors.password}
                                        </div>
                                    )}

                                </div>
                                <span
                                        onClick={handleShowPassword}
                                        className="absolute inset-y-0 right-0 px-3 flex items-center cursor-pointer"
                                    >
                                        {showPassword ? (
                                            <LuEye className="h-6 w-6 text-gray-600" />
                                        ) : (
                                            <FiEyeOff className="h-6 w-6 text-gray-600" />
                                        )}
                                    </span>
                            </div>
                            <button
                                className={`
                                    w-full py-2.5 rounded-lg text-sm font-semibold text-center inline-block 
                                    transition-all duration-200 
                                ${!isObjectEmpty(formik.errors) || loading // Cambiar el color del botón si hay errores o si loading es true
                                ? "bg-gray-300 text-black cursor-not-allowed"
                                : "bg-blue-600 text-white hover:bg-blue-700 focus:bg-blue-700"
                                }
                            `}
                            type="submit"
                            disabled={!isObjectEmpty(formik.errors) || loading} // Deshabilitar el botón cuando hay errores o cuando loading es true
                            
                            >
                                <span className="inline-block mr-2">
                                    {loading ? <FaSpinner className="w-4 h-4 animate-spin" /> : "Ingresar"}
                                </span>
                                {!loading && <FaArrowRight className="w-4 h-4 inline-block" />}
                            </button>
                        </div>
                    </form>                
                    <p className="m-4">
                        <button
                            onClick={() => navigate("/forgot-password")}
                            className="text-blue-500 p-4 underline"
                        >
                            ¿Olvidaste tu contraseña?
                        </button>
                    </p>
                </div>
            </div>
            <div className="absolute bottom-4 text-center text-gray-500 text-sm">
                <p>© 2025 PaniTab - Todos los derechos reservados</p>
            </div>
        </WavesContainer>
    );
};

// Animación para la card flotante
const fadeIn = keyframes`
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
`;

const wave = keyframes`
  0% {
    background-position: 0% 50%;
  }
  50% {
    background-position: 100% 50%;
  }
  100% {
    background-position: 0% 50%;
  }
`;

const WavesContainer = styled.div`
  position: relative;
  overflow: hidden;
  background: radial-gradient(circle, #f9e6cf, #f4d7b3, #e6b985, #d6a66e);
  background-size: 300% 300%;
  animation: ${wave} 12s ease infinite;
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;

  & .animate-fade {
    animation: ${fadeIn} 0.5s ease forwards;
    }

  &::before {
    content: "";
    position: absolute;
    top: 50%;
    left: 50%;
    width: 500px;
    height: 500px;
    background: rgba(255, 229, 204, 0.4);
    filter: blur(150px);
    transform: translate(-50%, -50%);
    border-radius: 50%;
    z-index: 1;
  }

  &::after {
    content: "";
    position: absolute;
    top: 20%;
    left: 20%;
    width: 300px;
    height: 300px;
    /*background: url("/src/images/logo.png") no-repeat center;*/
    background-size: contain;
    opacity: 0.2;
    transform: translate(-50%, -50%);
    z-index: 0;
  }
`;







