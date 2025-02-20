import { UserForm } from "../components/UserForm";
import { Loading } from "../../../shared/components/Loading";
import { SideBarUser } from "../components/SideBarUser";
import Header from "../../../shared/components/Header";
import { FaCheckCircle } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { useLayout } from "../../../shared/hooks/useLayout";
import { useCreateUser } from "../hooks/useCreateUser";
import { NotificationCard } from "../../../shared/components/NotificationCard";

export const UserCreatePage = () => {
    const { showSidebar, setShowSidebar, showUserMenu, setShowUserMenu } = useLayout();
    const navigate = useNavigate();
    const { handleCreateUser, loading, message, showConfirmation, notification } = useCreateUser();

    const handleCancel = () => {
        navigate("/users");
    };

    if (loading) {
        return <Loading />;
    }
    return (
        <div className="flex h-screen">
            {notification && <NotificationCard message={notification.message} type={notification.type} />}
            <SideBarUser showSidebar={showSidebar} />
            <div className="flex-1 flex flex-col">
                <Header
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                />
                <div className="p-6">
                    <h2 className="text-2xl font-bold mb-4">Actualizar Usuario</h2>
                    <UserForm user={[]} onSubmit={handleCreateUser} loading={loading} isCreate={true}/>
                </div>
                <div className="p-6 flex justify-end">
                    <button
                        onClick={handleCancel}
                        className="bg-gray-500 text-white font-semibold px-6 py-2 rounded-lg shadow-md hover:bg-gray-600"
                    >
                        Volver
                    </button>
                </div>
            </div>
        </div>
    );
}