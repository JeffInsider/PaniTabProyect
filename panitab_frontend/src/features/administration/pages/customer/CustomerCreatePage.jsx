import { useNavigate } from "react-router-dom";
import { useLayout } from "../../../../shared/hooks/useLayout";
import { Loading } from "../../../../shared/components/Loading";
import { NotificationCard } from "../../../../shared/components/NotificationCard";
import { SideBarAdministration } from "../../components/SideBarAdministration";
import Header from "../../../../shared/components/Header";
import { useCreateCustomer } from "../../hooks/customer/useCreateCustomer";
import { CustomerForm } from "../../components/customer/CustomerForm";

export const CustomerCreatePage = () => {
    const { showSidebar, setShowSidebar, showUserMenu, setShowUserMenu } = useLayout();
    const navigate = useNavigate();
    const {handleCreateCustomer, loading, message, showConfirmation, notification} = useCreateCustomer();

    const handleCancel = () => {
        navigate("/administration/customers");
    }

    if (loading) {
        return <Loading />;
    }
    return (
        <div className="flex h-screen">
            {/*si notification es verdadero, muestra el mensaje de notificación*/}
            {notification && <NotificationCard message={notification.message} type={notification.type} />}
            <SideBarAdministration showSidebar={showSidebar} />
            <div className="flex-1 flex flex-col">
                <Header
                    showSidebar={showSidebar}
                    setShowSidebar={setShowSidebar}
                    showUserMenu={showUserMenu}
                    setShowUserMenu={setShowUserMenu}
                />
                <div className="p-6">
                    <h2 className="text-2xl font-bold mb-4">Crear un vendedor</h2>
                    <CustomerForm customer={[]} onSubmit={handleCreateCustomer} loading={loading} isCreate={true}/>
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
};