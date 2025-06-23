using Applications.DTOs.ServiceApplication;
using Applications.Interfaces.Repositories;

namespace Applications.Helpers;

public static class ServiceApplicationValidator
{
    public static async Task<bool> ValidateNewApplication(this ServiceApplicationCreateRequest? request, IServiceApplicationRepository repository)
    {
        return await repository.DoesPersonHaveActiveApplicationsAsync(request.Value.PersonId, request.Value.ServiceOfferId);
    }
}