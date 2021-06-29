using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.Enum;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
   public class Project
    {
        public int Id { get;  }
        public ProjectDescription Description { get; }
        public ProjectName Name { get; }
        public TeamMember TeamMember { get; }
        public Client Client { get; }
        public ProjectStatus Status { get; }

        private Project(int id, ProjectName name, ProjectDescription description, ProjectStatus status, TeamMember teamMember, Client client)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            TeamMember = teamMember;
            Client = client;
        }

        public static Result<Project> Create(int id, string name, string description, ProjectStatus status, TeamMember teamMember, Client client)
        {

            Result<ProjectName> projectNameResult = ProjectName.Create(name);
            Result<ProjectDescription> descriptionResult = ProjectDescription.Create(description);
            Result result = Result.Combine(projectNameResult, descriptionResult);

            if (result.IsFailure)
            {
                return Result.Failure<Project>("Project creating failed");
            }
            return Result.Success(new Project(id, projectNameResult.Value, descriptionResult.Value, status, teamMember, client));
        }
    }
}
