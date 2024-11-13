using StreamServer.Database.Models;

namespace StreamServer.Database.Services
{
    public interface IStreamService
    {
        Task StreamStarted(StreamInformation stream);
        Task StreamEnded(StreamInformation stream);

        Task<StreamInformation?> GetStream(int subjectId);

        Task<bool> ValidateTeacherCreditials(int subjectId, string password);
    }
}
