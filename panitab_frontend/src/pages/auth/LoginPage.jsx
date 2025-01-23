import axios from "axios";
import { useContext, useState } from "react"
import { FiEyeOff } from "react-icons/fi";
import { LuEye } from "react-icons/lu";
import { useNavigate } from "react-router-dom";
import styled, { keyframes } from "styled-components";
import { AuthContext } from "../../context";
import logo from "../../images/logo.png";


export const LoginPage = () => {

    //se inicia el estado del formulario en vacio
    const [loginForm, setLoginForm] = useState({
        user: "",
        password: "",
    });

    //esto es para mostrar o no la contraseña
    const [showPassword, setShowPassword] = useState(false);

    const handleShowPassword = () => {
        setShowPassword(!showPassword);
    };

    //esto es para el envio del formulario y la logica de la autenticacion
    const {login} = useContext(AuthContext);
    const navigate = useNavigate();

    //se ejecuta cuando el usuario escribe en los inputs e inicia sesion
    const handleSubmit = async (e) => {
        e.preventDefault();
        const {user, password} = loginForm;
        try{
            //aqui iba el fetch pero se implemento con axios
            const response = await axios.post(
                "/auth/login",
                {
                    username: user,
                    password: password,
                },
                {
                    headers: {
                        "Content-Type": "application/json",
                    },
                }
            );

            console.log("exito", response.data);
            if (response.data && response.data.data && response.data.data.token) {
                //va a guardar el token en el localstorage y el contexto
                console.log(
                    "Token guardado con exito en el localstorage",
                    response.data.data.token
                );
                login({
                    username: user,
                    token: response.data.data.token,
                    refreshToken: response.data.data.refreshToken,
                    tokenExpiration: response.data.data.tokenExpiration,
                });

                navigate("/home");
            } else {
                console.log("No se recibio el token en la respuesta");
            }
        } catch (error) {
            console.error("Error al iniciar sesion", error);
        }
    };

    return (
        <WavesContainer>
            <div className="bg-white bg-opacity-30 backdrop-blur-md rounded-lg shadow-lg p-8 z-10 flex flex-col items-center">
                <div className="text-center mb-1">
                    <h1 className="text-4xl font-bold mb-4">Bienvenido a PaniTab Beta</h1>
                </div>
                <div className="w-full ">
                    <h2 className="text-2xl font-bold mb-2 text-center">Inicie Sesion</h2>
                    <form className="flex flex-col items-center" onSubmit={handleSubmit} >
                        <div className="w-full flex flex-col items-center">
                            <div className="mb-4 w-80">
                                <label 
                                    htmlFor="username"
                                    className="block text-sm font-semibold mb-2"
                                >
                                    Usuario:
                                </label>
                                <input 
                                    type="text"
                                    id="username"
                                    className="w-80 rounded-md bg-transparent border-b-2 
                                        border-blue-500 focus:outline-none focus:border-blue-700"
                                    value={loginForm.user}
                                    onChange={(e) =>
                                        setLoginForm({
                                            ...loginForm,
                                            user: e.target.value,
                                        })
                                    }
                                />
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
                                        type={showPassword ? "text" : "password"}
                                        id="password"
                                        className="w-80 rounded-md bg-transparent border-b-2 
                                        border-blue-500 focus:outline-none focus:border-blue-700" 
                                        value={loginForm.password}
                                        onChange={(e) =>
                                            setLoginForm({
                                                ...loginForm,
                                                password: e.target.value,
                                            })
                                        }
                                    />
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
                                type="submit" 
                                className="w-80 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 
                                            rounded focus:outline-none focus:shadow-outline">
                                Iniciar Sesion
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
  background: radial-gradient(circle, #b2f0e6, #9ad9db, #79b8d1, #5b9ac1);
  background-size: 300% 300%;
  animation: ${wave} 12s ease infinite;
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;

  &::before {
    content: "";
    position: absolute;
    top: 50%;
    left: 50%;
    width: 500px;
    height: 500px;
    background: rgba(0, 255, 255, 0.3);
    filter: blur(150px);
    transform: translate(-50%, -50%);
    border-radius: 50%;
    z-index: 1;
  }

  &::after {
    content: "";
    position: absolute;
    top: 80%;
    left: 0%;
    right: 0;
    bottom: 0;
    background: url("/src/images/logo.png") no-repeat ;
    background-size: 10%; 
    opacity: 0.9; 
    z-index: 0; 
  }
`;
// Inputs con efecto
export const Input = styled.input`
  width: 100%;
  padding: 10px;
  margin: 10px 0;
  border: 2px solid transparent;
  border-radius: 5px;
  background: transparent;
  color: #fff;
  font-size: 1rem;
  outline: none;
  transition: all 0.3s ease-in-out;

  &:focus {
    border-color: #00ffff;
    box-shadow: 0 0 10px #00ffff;
  }
`;







