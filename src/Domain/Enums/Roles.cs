using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Roles :ushort
    {
        [Description("Has full access to all system features and administrative capabilities.")]
        SystemAdministrator = 1,

        [Description("A student who is authorized to log in and participate in quizzes.")]
        QuizParticipant = 2,

        [Description("Responsible for creating and updating tasks within the system.")]
        TaskManager = 3,

        [Description("Can view tasks but is not permitted to make modifications.")]
        TaskReader = 4,

        [Description("Has permission to update student information.")]
        StudentOperator = 5,

        [Description("Can view student data without making any changes.")]
        StudentReader = 6,

        [Description("Granted full access to perform all CRUD operations on student records.")]
        StudentAdministrator = 7,

        [Description("Granted full access to perform all CRUD operations on tasks.")]
        TaskAdministrator = 8
    }


}
