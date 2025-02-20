import { useNavigate, useParams } from "react-router-dom";
import { useUserStore } from "../store/useUserStore";
import { useEffect, useState } from "react";
import { Loading } from "../../../shared/components/Loading";
import { useFormik } from "formik";
import { userInitValues, userValidationSchema } from "../forms/users_data";
import { disableUser, enableUser, getUserById, updateUser } from "../../../shared/actions/users/users.action";
import { UserForm } from "../components/UserForm";
import { SideBarUser } from "../components/SideBarUser";
import Header from "../../../shared/components/Header";
import { UserActiveStatus } from "../components/UserActiveStatus";
import { FaCheckCircle } from "react-icons/fa";
import { useLayout } from "../../../shared/hooks/useLayout";
import { useUserDetail } from "../hooks/useUserDetail";
import { NotificationCard } from "../../../shared/components/NotificationCard";

export const UserDetailPage = () => {
    const { showSidebar, setShowSidebar, showUserMenu, setShowUserMenu } = useLayout();
    const {user, loading, handleUpdateUser, handleUpdateStatus, notification} = useUserDetail();
    const navigate = useNavigate();


    const handleCancel = () => {
        navigate("/users");
    };

    if (!user) {
        return <Loading />;
    };

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
                    <UserForm user={user} onSubmit={handleUpdateUser} loading={loading} isCreate={false}/>
                    <UserActiveStatus
                        isActive={user.isActive}
                        onToggleStatus={() => handleUpdateStatus(user.id, user.isActive)}
                    />
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