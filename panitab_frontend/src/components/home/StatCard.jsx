const StatCard = ({ title, value }) => {
    return (
        <div className="bg-gradient-to-r from-[#f7f4f0] via-[#f0eae2] to-[#e0d6cc] shadow-md p-6 rounded-lg text-center border border-gray-200">
            <h4 className="text-gray-600 text-sm font-medium mb-2">{title}</h4>
            <p className="text-2xl font-bold text-gray-800">{value}</p>
        </div>
    );
};

export default StatCard;