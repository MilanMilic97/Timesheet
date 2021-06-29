using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities.Enum;

namespace Timesheet.Web.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Description { get; }
        public string Name { get; }
        public int TeamMemberId { get; }
        public int ClientId { get; }
        public ProjectStatus Status { get; }

        public ProjectDto(int id, string description, string name, int teamMemberId, int clientId, ProjectStatus status)
        {
            Id = id;
            Description = description;
            Name = name;
            TeamMemberId = teamMemberId;
            ClientId = clientId;
            Status = status;
        }

        public ProjectDto()
        {
        }
    }
}
