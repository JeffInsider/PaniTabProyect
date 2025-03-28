using AutoMapper;
using panitab_backend.Database.Entities;
using panitab_backend.Database.Entities.Administration;
using panitab_backend.Dtos.Auth;
using panitab_backend.Dtos.Customer;
using panitab_backend.Dtos.Users;

namespace panitab_backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForUsers();
        }

        public void MapsForUsers() 
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<CreateUserDto, UserEntity>();
            CreateMap<UpdateUserDto, UserEntity>();
        }

        public void MapsForCustomers()
        {
            CreateMap<CustomerEntity, CustomerDto>();
            CreateMap<CreateCustomerDto, CustomerEntity>();
            CreateMap<UpdateCustomerDto, CustomerEntity>();
        }

        //public void MapsForWarehouse()
        //{
        //    CreateMap<WarehouseControlEntity, WarehouseControlDto>();
        //    CreateMap<WarehouseDetailEntity, WarehouseDetailDto>();
        //}
    }
}
