using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Enum
{
    public enum ErrorCode
    {
        // General
        NoError = 0,
        UnknownError = 1,
        ValidationError = 2,
        Unauthorized = 3,
        Forbidden = 4,
        DatabaseError = 5,
        OperationFailed = 6,
        BadRequest = 7,
        NotFound = 8,
        // Course-related errors (100–199)
        InvalidCourseId = 100,
        CourseNotFound = 101,
        CourseAlreadyExists = 102,

        // Instructor-related errors (200–299)
        InstructorNotFound = 200,
        InvalidInstructorId = 201,
        InstructorAlreadyAssigned = 202,

        // Student-related errors (300–399)
        StudentNotFound = 300,
        InvalidStudentId = 301,
        StudentAlreadyRegistered = 302,

        // Question-related errors (400–499)
        QuestionNotFound = 400,
        InvalidQuestionFormat = 401,

        // Choice-related errors (500–599)
        ChoiceNotFound = 500,
        InvalidChoice = 501,

        // Quiz-related errors (600–699)
        QuizNotFound = 600,
        InvalidQuizId = 601,

        // Result-related errors (700–799)
        ResultNotFound = 700,
        InvalidResultData = 701,

        // StudentAnswer-related errors (800–899)
        StudentAnswerNotFound = 800,
        InvalidStudentAnswer = 801,

        // StudentCourse-related errors (900–999)
        StudentCourseNotFound = 900,
        StudentAlreadyEnrolled = 901,

        // StudentExam-related errors (1000–1099)
        StudentExamNotFound = 1000,
        ExamAlreadySubmitted = 1001,

        // StudentInstructor-related errors (1100–1199)
        StudentInstructorNotFound = 1100,
        InvalidAssignment = 1101,
        InternalServerError=1102,
        UnauthorizedAccess=1103,
        ResourceUnavailable
    }
}
