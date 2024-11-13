namespace StudyPlatform.ViewModels
{
    public class QuestionareViewModel
    {
        public string KnownlegeRating { get; set; } = string.Empty;
        public string Motivation { get; set; } = string.Empty;
        public string LearningFormat { get; set; } = string.Empty;
        public string LearningTarget { get; set; } = string.Empty;
        public string StudyModules { get; set; } = string.Empty;
        public string HoursPerWeek { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public override string ToString()
        {
            return $@"
            <h2>Survey Details</h2>
            <p><strong>Email:</strong> {Email}</p>
            <p><strong>Knowledge Rating:</strong> {KnownlegeRating}</p>
            <p><strong>Motivation Level:</strong> {Motivation}</p>
            <p><strong>Learning Format:</strong> {LearningFormat}</p>
            <p><strong>Learning Targets:</strong> {LearningTarget}</p>
            <p><strong>Learning Modules:</strong> {StudyModules}</p>
            <p><strong>Hours per Week:</strong> {HoursPerWeek}</p>
            <p><strong>Class:</strong> {Class}</p>";
        }

    }
}
