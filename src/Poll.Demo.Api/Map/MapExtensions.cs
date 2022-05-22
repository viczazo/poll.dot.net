using AutoMapper;

namespace Poll.Demo.Api.Map
{
    public static class MapExtensions
    {
        public static void AddWebApiMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiMappingProfile());
            });

            mappingConfig.CreateMapper();
        }
    }
}