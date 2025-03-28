export const QuickAccessCard = ({ title, description, icon: Icon }) => {
    return (
        <div className="p-4 bg-gray-100 shadow-md rounded-lg flex items-center space-x-4 cursor-pointer hover:bg-gray-200 transition">
            <Icon className="text-3xl text-blue-500" />
            <div>
                <h3 className="text-lg font-semibold">{title}</h3>
                <p className="text-sm text-gray-600">{description}</p>
            </div>
        </div>
    );
};
