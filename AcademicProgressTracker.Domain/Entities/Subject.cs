﻿namespace AcademicProgressTracker.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Semester { get; set; }

        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
    }
}